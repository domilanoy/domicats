import Vue from 'vue';
import Vuex from 'vuex';
import Cat from '../../common/types/uiTypes';

import Communication from '../../common/services/communicationService';

Vue.use(Vuex);

export const store = new Vuex.Store({
	state: {
        voteCount: -1,
        cats: null,
        modifiedCat: null

    },
    mutations: {
        changeVoteCount(state, voteCount) {
            state.voteCount = voteCount;
        },
        changeCats(state, cats) {
            state.cats = cats;
        },
        modifyCat(state, cat) {
            state.modifiedCat = cat;
        }
    },
    getters: {
        voteCount(state) {
            if (state.voteCount === -1) {
                Communication.callService('voteCount', {}, function (result: string) {
                    try {
                        state.voteCount = parseInt(result);
                    }
                    catch (e) {
                        alert('Error voteCount ' + e.message);
                    }
                }.bind(this));
            }
            return state.voteCount;
        },
        cats(state) {
            if (state.cats === null) {
                Communication.callService('cats', {}, function (result: string) {
                    try {
                        state.cats = JSON.parse(result);
                    }
                    catch (e) {
                        alert('Error cats ' + e.message);
                    }
                }.bind(this));
            }
            return state.cats;
        },
        modifiedCat(state) {
            return state.modifiedCat;
        }
    }
});

