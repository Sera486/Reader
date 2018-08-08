import * as types from './mutation-types'

export default {
    [types.SET_USER](state, user) {
        state.user = user;
    },

    [types.SET_TOKEN](state, token) {
        state.token = token;
    },

    [types.SET_BOOK_POSINTION](state, obj) {
        if (state.books == undefined) {
            state.books = [];
        }
        if (state.books[obj.id] == undefined) {
            state.books[obj.id] = {};
        }
        state.books[obj.id].position = obj.position;
    },

    [types.INCREASE_FONT_SIZE](state) {
        state.bookSettings.fontSize = 2 + state.bookSettings.fontSize ;
    },

    [types.DECREASE_FONT_SIZE](state) {
        state.bookSettings.fontSize = state.bookSettings.fontSize - 2;
    },

    [types.SET_FONT_FAMILY](state, fontFamily) {
        state.bookSettings.fontFamily = fontFamily;
    },

    [types.SET_ALIGNMENT](state, alignment) {
        state.bookSettings.textAlign = alignment;
    },

    [types.SET_LINE_HEGHT](state, lineHeight) {
        state.bookSettings.lineHeight = lineHeight;
    },

    [types.SET_THEME](state, theme) {
        state.bookSettings.theme = theme;
    },
}
