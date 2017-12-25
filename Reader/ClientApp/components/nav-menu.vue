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
                        <li v-for="route in routes">
                            <!-- TODO: highlight active link -->
                            <router-link :to="route.path">
                                <span :class="route.style"></span> {{ route.display }}
                            </router-link>
                        </li>
                    </ul>
                    <ul class="nav navbar-nav navbar-right">
                        <li><a><span class="glyphicon glyphicon-user"></span> Register</a></li>
                        <li><a @click="showLoginModal"><span class="glyphicon glyphicon-log-in"></span> Login</a></li>
                        
                    </ul>
                </div>
            </transition>
        </div>
        
    </div>
</template>

<script>
    import { routes } from '../routes'

export default {
    data() {
        return {
            routes,
            collapsed : true
        }
    },
    methods: {
        toggleCollapsed: function(event){
            this.collapsed = !this.collapsed;
        },
        showLoginModal: function (event) {
            console.log("show login emmited");
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
