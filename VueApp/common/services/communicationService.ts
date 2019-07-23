import EventUiService from './eventUiService';

export default class Communication {
    private static isOpen: boolean = false;
    private static ws: any = null;
    private static callbacks: Map<string, Function> = new Map();

    public static init(vueInstance: any, isDone: Function | any): void {

        if (Communication.isOpen) { isDone(true); return; }
        const doc: any = document;
        const protocol = doc.location.protocol === "https:" ? "wss" : "ws";
        const url = protocol + "://" + window.location.host + "/test";
        Communication.ws = new WebSocket(url);
        Communication.ws.onopen = function () {
            console.log("Web Socket is opened");
            Communication.ws.onmessage = (message: any) => {
                // analyze returned message 
                //alert(message.data);
                try {
                    const resp = JSON.parse(message.data);
                    const error = resp.error;
                    if (error !== '') {
                        alert('error onmessage=' + error); return;
                    }
                    const wsid = '' + resp.wsid;
                    if (Communication.callbacks.has(wsid)) {
                        Communication.callbacks.get(wsid)(resp.result);
                        Communication.callbacks.delete(wsid);
                    }
                    //
                    if (resp.action === 'ChangtEvent') {
                        EventUiService.manageEvent(vueInstance, resp.result);
                    }
                }
                catch (e) {
                    alert('erreur ws.onmessage ' + e.message);
                }
            };
            Communication.isOpen = true;
            isDone(true);
        }.bind(this);
        Communication.ws.onclose = function () {   // websocket is closed.
            Communication.isOpen = false;
            console.log("Connection is closed...");
        }.bind(this);
    }

    static callService(action: string, param: any, callback: Function): void {
        const wsid = '' + Math.random();
        if (callback) this.callbacks.set(wsid, callback);
        let formData = (param === null) ? {} : param;
        formData['wsid'] = '' + wsid;
        formData['action'] = action;
        //alert(JSON.stringify(formData));    //
        Communication.ws.send(JSON.stringify(formData));
    }
}
