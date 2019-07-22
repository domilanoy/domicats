<template>
    <div id="infov">
        <div class="container">
            <h1 class="title">
                {{ pageTitle }}
            </h1>
            <h2 class="subtitle">
                Template is created using Vue, Vue router, Vuex, TypeScript and Bulma.
            </h2>
            <info-about></info-about>
            <info-stack></info-stack>
        </div>
    </div>
</template>

<script lang="ts">
import { Vue, Component } from 'vue-property-decorator'
import InfoAbout from '@/catmash/components/Info/InfoAbout.vue'
import InfoStack from '@/catmash/components/Info/InfoStack.vue'

@Component({
    name: 'Info',
    components: {
        InfoAbout,
        InfoStack
    }
})
export default class Info extends Vue {
    pageTitle: string = '.NET with Vue and TypeScript'
}
</script>
<style scoped>
</style>
