<template>
    <div id="appv">
        <div class="container header">
            <section class="section">
                <div class="columns">
                    <div class="column">
                        <router-link :to="{ name: 'Cats' }">Les chats</router-link>
                    </div>
                    <div class="column">
                        <router-link :to="{ name: 'vuex' }">Vuex</router-link>
                    </div>
                    <div class="column">
                        <router-link :to="{ name: 'thirdpartylibraries' }">Third party libraries</router-link>
                    </div>
                    <div class="column">
                        <router-link :to="{ name: 'Info' }">Info</router-link>
                    </div>
                    <div class="column">
                        Nombre de votes : {{ voteCount }}
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
import Communication from '../common/services/communicationService';

@Component({
    name: 'App'
})
export default {
    data() {
        return {
            voteCount: 0
        }
    },
    mounted: function () {
        setTimeout(() => {
            Communication.callService('voteCount', {}, function (result: string) {
                try {
                    this.voteCount = parseInt(result);
                }
                catch (e) {
                    alert('App - voteCount ' + e.message);
                }
            }.bind(this));
        }, 2000);
    }
}
</script>
<style scoped lang="scss">
</style>
