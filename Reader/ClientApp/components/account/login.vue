<template >
    <div>
        <div class="modal-header">
            <button type="button" class="close" @click="close">&times;</button>
        </div>

        <div class="modal-body">
            <form role="form" method="post">
                <fieldset>
                    <h3>Enter login and password</h3>
                    <div class="form-group">
                        <input v-model="userName" class="form-control input-lg" placeholder="Login">
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
        </div>

        <div class="modal-footer">
            <div class="row">
                <input @click="handleLogin" type="submit" class="btn btn-lg btn-primary btn-block" value="Login">
                <a @click="close" class="btn btn-link btn-lg  pull-left">Register</a>
            </div>
        </div>
    </div>
</template>

<script>
    import { mapActions } from 'vuex'
    import * as types from '../../store/mutation-types'
    import {login} from '../../store/auth'

    export default {
        data() {
            return {
                userName: "",
                password: "",
                remeberMe: true
            }
        },
        methods: {
            close(event) {
                this.$emit('close');
            },
            async handleLogin() {
                let response = await login({ userName: this.userName, password: this.password, remeberMe: this.remeberMe });
                
                let user = response.user;
                let token = response.token;
                this.$store.commit(types.SET_USER, user);
                this.$store.commit(types.SET_TOKEN, token);
                this.close();
            }
        }
    }
</script>

<style lang="less" scoped>
</style>
