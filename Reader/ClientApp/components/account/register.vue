<template>
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
                        <input v-model="email" class="form-control input-lg" placeholder="Email">
                        <span class="text-danger"></span>
                    </div>
                    <div class="form-group">
                        <input v-model="password" class="form-control input-lg" placeholder="Password">
                        <span class="text-danger"></span>
                    </div>
                    <div class="form-group">
                        <input v-model="confirmPassword" class="form-control input-lg" placeholder="Password confirmation">
                        <span class="text-danger"></span>
                    </div>
                </fieldset>
            </form>
        </div>

        <div class="modal-footer">
            <div class="row">
                <input @click="handleRegister" type="submit" class="btn btn-lg btn-primary btn-block" value="Register">
            </div>
        </div>
    </div>
</template>

<script>
    import { mapActions } from 'vuex'
    import * as types from '../../store/mutation-types'
    import { register } from '../../store/auth'

    export default {
        data() {
            return {
                userName: "",
                email: "",
                password: "",
                confirmPassword: "",
            }
        },
        methods: {
            close(event) {
                this.$emit('close');
            },
            async handleRegister() {
                let response = await register({ userName: this.userName, email: this.email, password: this.password, confirmPassword: this.confirmPassword, });

                localStorage.setItem("token", response.token);
                localStorage.setItem("user", JSON.stringify(response.user));

                let user = response.user;
                user.isAuthentificated = true;
                this.$store.commit(types.SET_USER, user);
                this.close();
            }
        }
    }
</script>

<style lang="less" scoped>
</style>
