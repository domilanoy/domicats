<template>
    <div id="appv">
        <loader v-if="init"></loader>
        <div v-else>
            <div class="container header">
                <section class="section">
                    <div class="columns">
                        <div class="column">
                            <router-link :to="{ name: 'Cats' }">Les chats</router-link>
                        </div>
                        <!--<div class="column">
                            <router-link :to="{ name: 'vuex' }">Vuex</router-link>
                        </div>
                        <div class="column">
                            <router-link :to="{ name: 'thirdpartylibraries' }">Third party libraries</router-link>
                        </div>
                        <div class="column">
                            <router-link :to="{ name: 'Info' }">Info</router-link>
                        </div>-->
                        <div class="column">
                            Nombre de votes : <span v-if="voteCount > -1">{{ voteCount }}</span>
                        </div>
                    </div>
                </section>
            </div>
            <div class="container">
                <notifications group="all" />
                <div class="columns">
                    <section class="section">
                        <transition name="component-fade" mode="out-in">
                            <router-view></router-view>
                        </transition>
                    </section>
                </div>
            </div>
        </div>
        <div class="container">
            <footer>
                <hr />
                <p>
                    &copy; 2018 - domicats
                </p>
            </footer>
        </div>
    </div>
</template>
<script lang="ts">
import { Vue, Component } from 'vue-property-decorator';
import Loader from '../common/components/Loader.vue';
import Communication from '../common/services/communicationService';

@Component({
    name: 'App',
    components: {
        Loader
    }
})
//export default {
export default class App extends Vue {
    data() {
        return {
            init: true,
            voteCount: -1
        }
    };
    mounted() {
        // initialization Communication
        const self: any = this;
        Communication.init(this, (isOpen) => {
            if (isOpen) {
                self.init = false;
                self.$store.watch(      // when modification
                    function (state) {
                        return state.voteCount;
                    },
                    function (val) {
                        self.voteCount = val;
                    },
                    {
                        deep: true //add this if u need to watch object properties change etc.
                    }
                );
                self.voteCount = self.$store.getters.voteCount;
            }
        });
    }
}
</script>
<style scoped lang="scss">
</style>
