import Vue from 'vue'
import VueRouter from 'vue-router'
import {isAuthenticated} from './store/auth'

import { routes } from './routes'

Vue.use(VueRouter);

let router = new VueRouter({
    mode: 'history',
    routes
});

router.beforeEach((to, from, next) => {
    if (to.matched.some(record => record.name != 'book') ) {
        let elems = document.getElementsByClassName('row');
        for (var i = 0; i < elems.length; i++) {
            elems[i].style.backgroundColor = 'white';
        }
    }

    document.title = to.meta.title;
    if (to.matched.some(record => record.meta.requiresAuth) && !isAuthenticated()) {
        next({ path: '/', query: { redirect: to.fullPath } });
    } else {
        next();
    }
});

export default router
