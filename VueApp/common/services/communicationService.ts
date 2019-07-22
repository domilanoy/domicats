export default class Communication {
    private static isOpen: boolean = false;
    private static ws: any = null;
    private static callbacks: Map = new Map();

    private static init(isDone: Function): void {
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
                        alert(error); return;
                    }
                    const wsid = '' + resp.wsid;
                    if (Communication.callbacks.has(wsid)) {
                        Communication.callbacks.get(wsid)(resp.result);
                        Communication.callbacks.delete(wsid);
                    }
                    //
                    if (resp.action === 'ChangtEvent') {
                        alert('changement');
                    }
                }
                catch (e) {
                    alert('erreur ws.onmessage');
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

    static callService(action: string, param: any, callback: any): void {
        Communication.init((isDone: boolean) => {
            if (!isDone) return;
            const wsid = '' + Math.random();
            if (callback) this.callbacks.set(wsid, callback);
            let formData = { wsid: '' + wsid, action: action, param: JSON.stringify(param) };
            Communication.ws.send(JSON.stringify(formData));
        });
    }


    /**
     * Show success notification
     *
     * @param {Object} vueInstance - Vue instance
     * @param {String} message - Message which to display on Notification
     */
    static callComm(vueInstance: any, message: string) {
        /*vueInstance.$notify({
            group: 'global',
            type: 'success',
            title: 'Success',
            text: message,
            duration: 3500
        })*/
    }

    
}
