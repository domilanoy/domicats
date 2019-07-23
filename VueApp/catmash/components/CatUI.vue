<template>
    <div class="catcont">
        <div>
            <img :data-id="id" :src="url" />
        </div>
        <p>
            <span ref="scoreElt">Score {{ _score }}</span>
            <span @click="vote" title="Voter pour ce chat">Voter</span>
            <span @click="prefer" title="Placer ce chat dans mes pr&eacute;f&eacute;r&eacute;s">++</span>
        </p>
    </div>
</template>
<script lang="ts">
import { Vue, Component } from 'vue-property-decorator';
import Communication from '../../common/services/communicationService';
import Notifications from 'vue-notification'

Vue.use(Notifications)

@Component({
    name: 'CatUi'
    })
export default {
    props: {
        id: String,
        url: String,
        score: Number
    },
    data() {
        return {
            _score: 0
        }
    },
    created() {
        const self: any = this;
        self._score = self.score;
        self.$eventBus.$on('updateScore', (id: string, score: number) => {
            if (id === self.id) {
                self._score = score;
                self.$refs.scoreElt.innerText = 'Score ' + score;
            }
        });
    },
    methods: {
        vote: function () {
            Communication.callService('vote', { id: this.id }, function (result: boolean) {
                // ['Success', 'Warn', 'Error']
                if (result) {
                    this.$notify({
                        group: 'all',
                        title: 'Vote',
                        type: 'Success',
                        text: 'Vote accompli'
                    });
                } else {
                    this.$notify({
                        group: 'all',
                        title: 'Vote',
                        type: 'Error',
                        text: 'Vote non accompli'
                    });
                }
            }.bind(this));
        },
        prefer: function () {
            let prefer: string | null = localStorage.getItem("catmash_prefer");
            if (prefer) {
                if (! prefer.includes(',' + this.id + ','))  prefer += this.id + ',';

            } else {
                prefer = ',' + this.id + ',';
            }
            localStorage.setItem("catmash_prefer", prefer);
        }
    }
}
</script>
<style scoped>
</style>
