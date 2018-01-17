import Vue from 'vue'
import VueRouter from 'vue-router'

import { routes } from './routes'

Vue.use(VueRouter);

let router = new VueRouter({
    mode: 'history',
    routes
});

router.beforeEach((to, from, next) => {
    document.title = to.meta.title;
    next();
    //if (to.matched.some(record => record.meta.requiresAuth) && !Auth.loggedIn) {
    //    next({ path: '/login', query: { redirect: to.fullPath } });
    //} else {
    //    next();
    //}
});

export default router
