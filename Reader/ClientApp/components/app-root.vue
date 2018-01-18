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
    import * as types from '../store/mutation-types'

    import Vue from 'vue'
    import NavMenu from './nav-menu'
    import HomePage from './home-page'
    import LibraryPage from './library-page'
    import Login from './account/login'
    import BookPreview from './book-preview'
    import BookPage from './book-page'
    import BookLoader from './book-loader'
    import UserPreview from './account/user-preview'

    Vue.component('home-page', HomePage);
    Vue.component('library-page', LibraryPage);
    Vue.component('nav-menu', NavMenu);
    Vue.component('login', Login);
    Vue.component('book-preview', BookPreview);
    Vue.component('book-page', BookPage);
    Vue.component('book-loader', BookLoader);
    Vue.component('user-preview', UserPreview);


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
        },

        beforeMount() {
            //restoring user after page refresh
            let user = JSON.parse(localStorage.getItem("user"));
            console.log(user);
            if (user) {
                user.isAuthentificated = true;
                this.$store.commit(types.SET_USER, user);
            }
        }

    }

</script>

<style>
    .router-view {
        margin-top: 80px;
        margin-left: 60px;
        margin-right: 60px;
    }
</style>
