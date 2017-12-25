import { router } from "../router"
import axios from "axios";

const API_URL = "http://localhost:3001/";
const LOGIN_URL = API_URL + "sessions/create/";
const SIGNUP_URL = API_URL + "users/";

const getAuthToken =()=> {
    try {
        return localStorage.getItem("token");
    } catch (err) {
        return null;
    }
}

axios.defaults.headers.common.Authorization = getAuthToken();


export default {
    user: {
        authenticated: false
    },

    async login(user) {
        try {
            let response = await axios.post("/api/account/login",
                {
                    userName: user.userName,
                    password: user.password,
                    remeberMe: user.remeberMe
                });

            localStorage.setItem("token", response.data.token);
            this.user.authenticated = true;
        } catch (err) {
            console.log("Failed to login"+err);
        }
        return response.data;
    },

    //login(context, creds, redirect) {
    //    context.$http.post(LOGIN_URL,
    //        creds,
    //        (data) => {
    //            localStorage.setItem("token", data.token);

    //            this.user.authenticated = true;

    //            if (redirect) {
    //                router.go(redirect);
    //            }

    //        }).error((err) => {
    //        context.error = err;
    //    });
    //},

    signup(context, creds, redirect) {
        context.$http.post(SIGNUP_URL,
            creds,
            (data) => {
                localStorage.setItem("token", data.id_token);

                this.user.authenticated = true;

                if (redirect) {
                    router.go(redirect);
                }

            }).error((err) => {
            context.error = err;
        });
    },

    logout() {
        localStorage.removeItem("token");
        this.user.authenticated = false;
    },

    checkAuth() {
        let jwt = localStorage.getItem("token");
        this.user.authenticated = !!jwt;
    }
}