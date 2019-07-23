<template>
    <div>
        <div class="container">
            <section class="section">
                <div class="columns">
                    <div class="column">
                        <h2>Bonjour ...</h2>
                    </div>
                </div>
            </section>
       </div>
    </div>
</template>

<script lang="ts">
import { Vue, Component } from 'vue-property-decorator'

@Component({
    name: 'Loader'
})
export default class Loader extends Vue { }
</script>

<style scoped scss>
    h2 {
        font-size: 1.2em;
    }
</style>
