import Vue from 'vue'
import Vuex from 'vuex'
import createPersistedState from 'vuex-persistedstate'
import api from 'api'
import auth from './auth'
import mutations from './mutations'
import * as actions from './actions'
import { FontFamily } from '../store/enums'
//import * as getters from './getters'

Vue.use(Vuex);

// STATE
const state = {
    //default state; if user data is actualy stored in local storage we will load it
    user: null,
    bookSettings: {
        fontSize: 18,
        fontFamily: FontFamily.Verdana,
        lineHeight: "1",
        theme: "white",
        textAlign: "justify"
    }
}


export default new Vuex.Store({
    plugins: [createPersistedState()],
    state,
    mutations,
    actions,
//    getters
});
