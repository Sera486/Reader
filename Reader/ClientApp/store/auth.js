import { router } from "../router"
import axios from "axios";

const API_URL = "/api/account/";
const LOGIN_URL = API_URL + "login";
const REGISTER_URL = API_URL + "register";

const getAuthToken =()=> {
    try {
        let state = JSON.parse(localStorage.getItem("vuex"));
        return state['token'];
    } catch (err) {
        return null;
    }
}

axios.defaults.headers.common.Authorization = getAuthToken();


export const login = async (user, redirect) => {
    try {
        let response = await axios.post(LOGIN_URL,
            {
                userName: user.userName,
                password: user.password,
                remeberMe: user.remeberMe
            });

        if (redirect) {
            router.go(redirect);
        }

        return response.data;
    } catch (err) {
        console.log("Failed to login" + err);
    }
}

export const register= async (user, redirect) => {
    try {
        let response = await axios.post(REGISTER_URL,user);

        return response.data;
    } catch (err) {
        console.log("Failed to login" + err);
    }
}

export const logout = () => {
    localStorage.clear();
    console.log("user logged out");
}

export function isAuthenticated() {
    let jwt = getAuthToken();
    return !!jwt;
}
