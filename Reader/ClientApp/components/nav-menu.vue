<template>
    <div class="main-nav">
        <div class="navbar navbar-inverse">
            <div class="navbar-header">
                <button type="button" class="navbar-toggle" v-on:click="toggleCollapsed">
                    <span class="sr-only">Toggle navigation</span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                </button>
                <a class="navbar-brand" href="/">Reader</a>
            </div>
            <transition name="slide">
                <div class="navbar-collapse collapse in" v-show="!collapsed">
                    <ul class="nav navbar-nav">
                        <li v-for="route in routes.filter(f=>f.display!=null)">
                            <router-link :to="route.path">
                                <span :class="route.style"></span> {{ route.display }}
                            </router-link>
                        </li>
                    </ul>
                    <ul class="nav navbar-nav navbar-right">
                        <li v-if="user.isAuthenticated"></li>
                        <li v-if="!user.isAuthenticated"><a><span class="glyphicon glyphicon-user"></span> Register</a></li>
                        <li v-if="!user.isAuthenticated"><a @click="showLoginModal"><span class="glyphicon glyphicon-log-in"></span> Login</a></li>
                    </ul>
                </div>
            </transition>
        </div>
        
    </div>
</template>

<script>
    import UserPreview from './account/user-preview'
    import { routes } from '../routes'
    import { mapState } from 'vuex'

export default {
        data() {
            return {
                routes,
                collapsed: true
            }
        },

        computed: {
            ...mapState({
                user: state => state.user
            })
        },

        methods: {
        toggleCollapsed: function(event){
            this.collapsed = !this.collapsed;
        },
        showLoginModal: function (event) {
            this.$emit("showlogin");
        }
    }
}
</script>

<style>
    .navbar-right{
        margin-right:30px;
    }

.slide-enter-active, .slide-leave-active {
  transition: max-height .35s
}
.slide-enter, .slide-leave-to {
  max-height: 0px;
}

.slide-enter-to, .slide-leave {
  max-height: 20em;
}
</style>
