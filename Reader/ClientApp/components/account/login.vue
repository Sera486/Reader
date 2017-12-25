<template >
    <transition name="modal">
        <div class="modal-mask">
            <div class="modal-wrapper" @click="close" >
                <div class="modal-container" v-on:click.stop=""> <!-- дабы модалка не закрывалась при клике на неё саму-->

                    <div class="modal-header">
                        <slot name="header">
                            <button type="button" class="close" @click="close">&times;</button>
                        </slot>
                    </div>

                    <div class="modal-body">
                        <slot name="body">
                            <form role="form" method="post">
                                <fieldset>
                                    <h3>Enter login and password</h3>
                                    <div class="form-group">
                                        <input v-model="userName"  class="form-control input-lg" placeholder="Login">
                                        <span class="text-danger"></span>
                                    </div>
                                    <div class="form-group">
                                        <input v-model="password" class="form-control input-lg" placeholder="Password">
                                        <span class="text-danger"></span>
                                    </div>
                                    <span class="button-checkbox">
                                        <button type="button" class="btn" data-color="info">Remeber me</button>
                                        <input v-model="remeberMe" type="checkbox" checked="checked" class="hidden">
                                        <a class="btn btn-lg btn-link pull-right">Forgot password?</a>
                                    </span>
                                </fieldset>
                            </form>
                        </slot>
                    </div>

                    <div class="modal-footer">
                        <slot name="footer">
                            <div class="row">
                                <input @click="login" type="submit" class="btn btn-lg btn-primary btn-block" value="Login">
                                <a @click="close" class="btn btn-link btn-lg  pull-left">Register</a>
                            </div>
                        </slot>
                    </div>

                </div>
            </div>
        </div>
    </transition>
</template>

<script>
    import { mapActions } from 'vuex'
    //TODO: Сейчас модалка только для логина, хорошо бы её переделать под всё на свете
    export default {
        data() {
            return {
                userName: "",
                password: "",
                remeberMe:true
            }
        },
        methods: {
            close: function (event) {
                this.$emit('close');
            },
            login: function () {
                this.$store.dispatch('login', { userName: this.userName, password: this.password, remeberMe: this.remeberMe });
            }
        }
    }
</script>

<style lang="less">
    .modal-mask {
        position: fixed;
        z-index: 9998;
        top: 0;
        left: 0;
        width: 100%;
        height: 100%;
        background-color: rgba(0, 0, 0, .5);
        display: table;
        transition: opacity .3s ease;
    }

    .modal-wrapper {
        display: table-cell;
        vertical-align: middle;
    }

    .modal-container {
        width: 500px;
        margin: 0px auto;
        padding: 10px 15px;
        background-color: #fff;
        border-radius: 5px;
        box-shadow: 0 2px 8px rgba(0, 0, 0, .33);
        transition: all .3s ease;
        font-family: Helvetica, Arial, sans-serif;
    }

    .modal-header h3 {
        margin-top: 0;
        color: #42b983;
    }


    /*
 * The following styles are auto-applied to elements with
 * transition="modal" when their visibility is toggled
 * by Vue.js.
 *
 * You can easily play with the modal transition by editing
 * these styles.
 */

    .modal-enter {
        opacity: 0;
    }

    .modal-leave-active {
        opacity: 0;
    }

    .modal-enter .modal-container,
    .modal-leave-active .modal-container {
        -webkit-transform: scale(1.1);
        transform: scale(1.1);
    }
</style>
