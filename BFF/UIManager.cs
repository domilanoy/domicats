using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Threading;
using CatmashAPI;
using Newtonsoft.Json;

namespace domicats
{
    public class UIManager
    {
        static CatmashService _catmashService = new CatmashService();
        WebSocketManager _webSocketManager;

        public UIManager(WebSocketManager webSocketManager)
        {
            _webSocketManager = webSocketManager;
        }

        public void AnswerTheDemand(WebSocket webSocket, string msg)
        {
            Dictionary<string, object> dict;
            string wsid = "", action = "";
            // analyze message
            try
            {
                dict = JsonConvert.DeserializeObject<Dictionary<string, object>>(msg);
                wsid = dict["wsid"].ToString();
                action = dict["action"].ToString();
            }
            catch (Exception ex)
            {
                SendMessage(webSocket, wsid, action, "", ex.Message);
                return;
            }
            // execute
            var (result, error) = Execute(action, dict);
            SendMessage(webSocket, wsid, action, result, error);
        }


        // private
        private (string result, string error) Execute(string action, Dictionary<string, object> param)
        {
            // execute
            string result = "", error = "";
            try
            {
                switch (action)
                {
                    case "cats":
                        result = JsonConvert.SerializeObject(_catmashService.Cats);
                        break;
                    case "voteCount":
                        result = _catmashService.VoteCount.ToString();
                        break;
                    case "vote":
                        string id = param["id"].ToString();
                        bool res = _catmashService.SetVote(id);
                        result = res.ToString();
                        if (res)
                        {
                            // changement
                            new Thread((delegate ()
                            {
                                Thread.Sleep(1000);
                                var obj = new { id, voteCount =_catmashService.VoteCount, score = _catmashService.GetScore(id) };
                                SendMessageToAll("ChangtEvent", JsonConvert.SerializeObject(obj), "");
                            })).Start();
                        }
                        //
                        break;
                    default:
                        error = $"Action '{action}' inconnue";
                        break;
                }
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
            return (result, error);
        }

        private async void SendMessageToAll(string action, string result, string error)
        {
            // event to ui apps
            string msg = JsonConvert.SerializeObject(new { wsid = "", action, result, error });
            await _webSocketManager.SendMessageToAllAsync(msg);
        }

        private async void SendMessage(WebSocket webSocket, string wsid, string action, string result, string error)
        {
            string msg = JsonConvert.SerializeObject(new { wsid, action, result, error });
            if (webSocket == null)
                await _webSocketManager.SendMessageToAllAsync(msg);
            else
                await _webSocketManager.SendMessageAsync(webSocket, msg);
        }
    }
}
