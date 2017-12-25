<template>
    <div id="app" class="container-fluid">
        <div class="row">
            <nav-menu params="route: route" v-on:showlogin="showModal"></nav-menu>
            <router-view class="router-view"></router-view>
        </div>
        <login v-if="modalVisible" v-on:close="closeModal"></login>
    </div>
</template>

<script>
    import Vue from 'vue'
    import CounterExample from './counter-example'
    import FetchData from './fetch-data'
    import HomePage from './home-page'
    import NavMenu from './nav-menu'
    import Login from './account/login'

    Vue.component('counter-example', CounterExample);
    Vue.component('fetch-data', FetchData);
    Vue.component('home-page', HomePage);
    Vue.component('nav-menu', NavMenu);
    Vue.component('login', Login);


export default {
    data() {
        return {
            modalVisible: false
        }
    },
    methods: {
        showModal: function (event) {
            this.modalVisible = true;
        },
        closeModal: function (event) {
            this.modalVisible = false;
        }
    }

}
</script>

<style>
    .router-view{
        margin-top:80px;
        margin-left:100px;
    }
</style>
