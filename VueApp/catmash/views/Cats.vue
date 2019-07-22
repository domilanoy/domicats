<template>
    <div id="catsv">
        <div class="container">
            <template v-for="(cat, index) in cats" v-if="cats">
                <!--<br v-if="index > 0 && index % 5 == 0" />-->
                <cat-ui :id="cat.Id" :url="cat.Url" :score="cat.Score"></cat-ui>
            </template>
        </div>
    </div>
</template>
<script lang="ts">
import { Vue, Component } from 'vue-property-decorator';
import CatUi from '@/catmash/components/CatUi.vue';
import Cat from '../../common/types/cat';
import Communication from '../../common/services/communicationService';


@Component({
    name: 'Cats',
    components: {
        CatUi
    }
})
export default class Cats extends Vue {
    cats = null;

    created(): void {
        Communication.callService('cats', {}, function (result: string) {
            try {
                this.cats = JSON.parse(result);
            }
            catch (e) {
                alert(e.message);
            }
        }.bind(this));
    }
}
</script>
<style scoped>
</style>
