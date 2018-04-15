import Vue from 'vue'
import Vuex from 'vuex'
import createPersistedState from 'vuex-persistedstate'
import api from 'api'
import auth from './auth'
import mutations from './mutations'
import * as actions from './actions'
//import * as getters from './getters'

Vue.use(Vuex);

// STATE
const state = {
    //default state; if user data is actualy stored in local storage we will load it
    user: null
}


export default new Vuex.Store({
    plugins: [createPersistedState()],
    state,
    mutations,
    actions,
//    getters
});
