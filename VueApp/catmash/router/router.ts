import VueRouter from 'vue-router'

// Pages
import Cats from '@/catmash/views/Cats.vue'
import Vuex from '@/catmash/views/Vuex.vue'
import Info from '@/catmash/views/Info.vue'
import ThirdPartyLibraries from '@/catmash/views/ThirdPartyLibraries.vue'

const routePrefix = 'catmash'

const routes = [
	{
		path: '*',
		redirect: { name: 'Info' }
	},
	{
		name: 'Info',
		path: `/${routePrefix}/info`,
		component: Info
    },
    {
        name: 'Cats',
        path: `/${routePrefix}/cats`,
        component: Cats
    },
	{
		name: 'vuex',
		path: `/${routePrefix}/vuex`,
		component: Vuex
	},
	{
		name: 'thirdpartylibraries',
		path: `/${routePrefix}/thirdpartylibraries`,
		component: ThirdPartyLibraries
	}
]

export const router = new VueRouter({
	mode: 'history',
	routes,
	linkActiveClass: 'is-active'
})
