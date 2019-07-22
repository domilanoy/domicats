import Vuex from "vuex";
import {
    createLocalVue,
    shallowMount
} from "@vue/test-utils";

import TemplateInfo from "@/catmash/views/Info.vue";
import TemplateInfoAbout from "@/catmash/components/Info/InfoAbout.vue";

const localVue = createLocalVue();
localVue.use(Vuex);

describe.only("Info.vue", () => {

    it('Sets the correct default data', () => {

        const wrapper = shallowMount(TemplateInfo, {
            localVue
        });

        expect(wrapper.vm.pageTitle).toBe('.NET with Vue and TypeScript');
    })

    it('Has InfoAbout component', () => {

        const wrapper = shallowMount(TemplateInfo, {
            localVue
        });

        expect(wrapper.contains(TemplateInfoAbout)).toBe(true);
    })
});
