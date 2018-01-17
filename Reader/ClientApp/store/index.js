import Vue from 'vue'
import Vuex from 'vuex'
import Api from 'api'
import Auth from './auth'

Vue.use(Vuex);

// TYPES
const MAIN_SET_COUNTER = 'MAIN_SET_COUNTER';
const SET_USER = 'SET_USER';


// STATE
const state = {
    user: {
        isAuthenticated:false
    },
    counter: 0
}

// MUTATIONS
const mutations = {
    [MAIN_SET_COUNTER](state, obj) {
        state.counter = obj.counter;
    },
    [SET_USER](state, obj) {
        obj.user.isAuthenticated = true;
        console.log(obj.user);
        state.user = obj.user;
        console.log(state.user.userName);
    }
}

// ACTIONS
const actions = ({
    setCounter({ commit }, obj) {
        commit(MAIN_SET_COUNTER, obj);
    },

    async login({ commit }, user) {
        let obj = await Auth.login(user);
        console.log(obj);
        commit(SET_USER, obj);
    }
});

export default new Vuex.Store({
    state,
    mutations,
    actions
});
