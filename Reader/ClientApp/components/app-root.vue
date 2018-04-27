<template>
    <div id="app" class="container-fluid">
        <div class="row">
            <nav-menu params="route: route" v-on:showModal="showModal"></nav-menu>
            <router-view class="router-view"></router-view>
        </div>
        <modal v-if="modalVisible" v-on:close="closeModal">
            <component v-bind:is="currentModal" v-on:close="closeModal"></component>
        </modal>
    </div>
</template>

<script>
    import * as types from '../store/mutation-types'

    import Vue from 'vue'
    import NavMenu from './nav-menu'
    import Modal from './modal'
    import HomePage from './home-page'
    import LibraryPage from './library-page'
    import Login from './account/login'
    import Register from './account/register'
    import BookPreview from './book-preview'
    import BookPage from './book-page'
    import BookLoader from './book-loader'
    import UserPreview from './account/user-preview'
    import LoadingWheel from './loading-wheel'
    import TableOfContent from './table-of-content'
    import UserLibraryPage from './user-library-page'

    Vue.component('home-page', HomePage);
    Vue.component('library-page', LibraryPage);
    Vue.component('nav-menu', NavMenu);
    Vue.component('modal', Modal);
    Vue.component('login', Login);
    Vue.component('book-preview', BookPreview);
    Vue.component('book-page', BookPage);
    Vue.component('book-loader', BookLoader);
    Vue.component('user-preview', UserPreview);
    Vue.component('loading-wheel', LoadingWheel);
    Vue.component('table-of-content', TableOfContent);
    Vue.component('user-library-page', UserLibraryPage);

    export default {
        data() {
            return {
                modalVisible: false,
                currentModal:'login',
            }
        },

        methods: {
            showModal(event) {
                this.modalVisible = true;
                this.currentModal = event;
            },
            closeModal(event) {
                this.modalVisible = false;
            }
        },

        components: {
            login: Login,
            register: Register,
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

<style lang="less">
    .router-view {
        margin-top: 80px;
    }
</style>
