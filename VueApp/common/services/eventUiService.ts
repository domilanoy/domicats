import EventMessage from '../types/uiTypes';
import Cat from '../types/uiTypes';

export default class EventUiService {
    static manageEvent(vueInstance: any, message: string) {
        try {
            const info: EventMessage = JSON.parse(message);
            // update voteCount
            vueInstance.$store.commit('changeVoteCount', info.voteCount);

            // update score of a cat
            vueInstance.$eventBus.$emit('updateScore', info.id, info.score);
        }
        catch (e) {
            alert('erreur EventUiManager ' + e.message);
        }  
    }
}

