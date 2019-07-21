<template>
    <div>
        <div class="container">
            <div class="columns">
                <div class="column is-3">

                    <section class="section">
                        <aside class="menu">
                            <p class="menu-label">
                                Examples
                            </p>
                            <ul class="menu-list">
                                <li>
                                    <router-link :to="{ name: 'vuex' }">Vuex</router-link>
                                </li>
                                <li>
                                    <router-link :to="{ name: 'thirdpartylibraries' }">Third party libraries</router-link>
                                </li>
                            </ul>
                            <p class="menu-label">
                                About
                            </p>
                            <ul class="menu-list">
                                <li>
                                    <router-link :to="{ name: 'templateInfo' }">Template</router-link>
                                </li>
                            </ul>
                        </aside>
                    </section>

                </div>
                <div class="column is-9">

                    <section class="section">
                        <transition name="component-fade"
                                    mode="out-in">
                            <router-view></router-view>
                        </transition>
                    </section>

                </div>
            </div>
        </div>
        <div class="container">
            <footer>
                <hr />
                <p>&copy; 2018 - domicats
                <button @click="test">test</button>
                
                </p>
            </footer>
        </div>
    </div>
</template>
<script lang="ts">
import { Vue, Component } from 'vue-property-decorator'

@Component({
    name: 'App'
})
export default {
    data() {
        return {
            isOpen: false,
            ws: null,
            errConn: true,
            fnc: null
        };
    },
    created: function () {
        const protocol = document.location.protocol === "https:" ? "wss" : "ws";
        const url = protocol + "://" + window.location.host + "/test";
        this.ws = new WebSocket(url);
        this.ws.onopen = function () {
            this.isOpen = true;
            console.log("Web Socket is opened");
        }.bind(this);
        this.ws.onclose = function () {   // websocket is closed.
            this.isOpen = false;
            console.log("Connection is closed...");
        }.bind(this);
    },
    methods: {
        test() {
            alert('hhhh');
            this.ws.send('fffffffffffffffffff');
        }
    } 
}
</script>
<style scoped lang="scss">
</style>
