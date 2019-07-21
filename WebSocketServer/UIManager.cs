using System;
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
            string wsid = "", action = "", param = "";
            // analyze message
            try
            {
                Message message = JsonConvert.DeserializeObject<Message>(msg);
                wsid = message.Wsid;
                action = message.Action;
                param = message.Param;
            }
            catch (Exception ex)
            {
                SendMessage(webSocket, wsid, action, "", ex.Message);
                return;
            }
            // execute
            var (result, error) = Execute(action, param);
            SendMessage(webSocket, wsid, action, result, error);
        }


        // private
        private (string result, string error) Execute(string action, string param)
        {
            // execute
            string result = "", error = "";
            try
            {
                switch (action)
                {
                    case "cats":
                        result = JsonConvert.SerializeObject(_catmashService.Cats);
                        //
                        new Thread((delegate ()
                        {
                            Thread.Sleep(2000);
                            SendMessageToAll("ChangtEvent", "", "");
                        })).Start();
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

        private class Message
        {
            public string Wsid, Action, Param;
        }
    }
}
