<template>
    <div id="catsv">
        <div class="container">
            <template v-for="(cat, index) in cats" v-if="cats">
                <cat-ui :id="cat.Id" :url="cat.Url" :score="cat.Score"></cat-ui>
            </template>
        </div>
    </div>
</template>
<script lang="ts">
import { Vue, Component } from 'vue-property-decorator';
import CatUi from '@/catmash/components/CatUi.vue';

@Component({
    name: 'Cats',
    components: {
        CatUi
    }
})
export default class Cats extends Vue {
    cats = null;

    mounted(): void {
        const self: any = this;
        self.$store.watch(      // when modification
            function (state: any) {
                return state.cats;
            },
            function (val: any) {
                self.cats = val;
            },
            {
                deep: true
            }
        );
        this.cats = this.$store.getters.cats;
    }
}
</script>
<style scoped>
</style>
