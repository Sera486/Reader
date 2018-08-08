<template>
    <div>
        <div class="btn btn-default settings-button" v-on:click="showSettings = !showSettings"><i class="glyphicon  glyphicon-cog"></i></div>
        <transition name="fade">
            <div v-if="showSettings" class="settings-container">
                <div class="dropdown dropdown_hidden dropdown_position-top-right">
                    <div class="dropdown__tail"></div>
                    <div class="settings">
                        <div class="settings__table settings__font-size">
                            <div class="settings__cell settings__smaller" v-on:click="$store.commit(types.DECREASE_FONT_SIZE);">A-</div>
                            <div class="settings__cell settings__bigger" v-on:click="$store.commit(types.INCREASE_FONT_SIZE);">A+</div>
                        </div>
                        <div class="settings__table settings__line-height">
                            <div v-bind:class="[bookSettings.lineHeight=='0.9'?'settings__active':'', 'settings__cell']" v-on:click="$store.commit(types.SET_LINE_HEGHT, '0.9'); lineHeight=1;">
                                <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 104 48" class="settings__icon">
                                    <path d="M32 16h40v2H32zm0 4h40v2H32zm0 4h40v2H32z"></path>
                                </svg>
                            </div>
                            <div v-bind:class="[bookSettings.lineHeight=='1'?'settings__active':'', 'settings__cell']" v-on:click="$store.commit(types.SET_LINE_HEGHT, '1'); lineHeight=2;">
                                <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 104 48" class="settings__icon">
                                    <path d="M32 16h40v2H32zm0 5h40v2H32zm0 5h40v2H32z"></path>
                                </svg>
                            </div>
                            <div v-bind:class="[bookSettings.lineHeight=='1.3'?'settings__active':'', 'settings__cell']" v-on:click="$store.commit(types.SET_LINE_HEGHT, '1.3'); lineHeight=3;">
                                <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 104 48" class="settings__icon">
                                    <path d="M32 16h40v2H32zm0 7h40v2H32zm0 7h40v2H32z"></path>
                                </svg>
                            </div>
                        </div>
                        <div class="settings__table settings__themes">
                            <div v-bind:class="[bookSettings.theme=='white'?'settings__active':'', 'settings__cell', 'settings__cell_theme', 'settings__cell_theme-white']" v-on:click="$store.commit(types.SET_THEME, 'white'); theme=1;">
                                <span>White</span>
                            </div>
                            <div v-bind:class="[bookSettings.theme=='sepia'?'settings__active':'', 'settings__cell', 'settings__cell_theme', 'settings__cell_theme-sepia']" v-on:click="$store.commit(types.SET_THEME, 'sepia'); theme=2;">
                                <span>Sepia</span>
                            </div>
                            <div v-bind:class="[bookSettings.theme=='black'?'settings__active':'', 'settings__cell', 'settings__cell_theme', 'settings__cell_theme-black']" v-on:click="$store.commit(types.SET_THEME, 'black'); theme=3;">
                                <span>Black</span>
                            </div>
                        </div><div class="settings__table settings__alignment">
                            <div class="settings__cell">
                                <span>Alignment</span>
                            </div>
                            <div v-bind:class="[bookSettings.textAlign=='left'?'settings__active':'', 'settings__cell', 'settings__cell_right']" v-on:click="$store.commit(types.SET_ALIGNMENT, 'left'); alignment=1;">
                                <span class="settings__icon"><svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24"><path d="M4 5h16v2H4zm0 4h12v2H4zm0 4h16v2H4zm0 4h12v2H4z"></path></svg></span>
                            </div>
                            <div v-bind:class="[bookSettings.textAlign=='justify'?'settings__active':'', 'settings__cell', 'settings__cell_right']" v-on:click="$store.commit(types.SET_ALIGNMENT, 'justify'); alignment=2;">
                                <span class="settings__icon" style="margin-right:20px">
                                    <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24">
                                        <path d="M4 5h16v2H4zm0 4h16v2H4zm0 4h16v2H4zm0 4h16v2H4z"></path>
                                    </svg>
                                </span>
                            </div>
                        </div>
                        <div>
                            <div v-bind:class="[bookSettings.fontFamily=='Times New Roman,serif'?'settings__active':'', 'settings__font', 'settings__font_times']" v-on:click="$store.commit(types.SET_FONT_FAMILY, 'Times New Roman,serif'); fontFamily=1;">Times</div>
                            <div v-bind:class="[bookSettings.fontFamily=='Arial,sans-serif'?'settings__active':'', 'settings__font', 'settings__font_arial']" v-on:click="$store.commit(types.SET_FONT_FAMILY, 'Arial,sans-serif'); fontFamily=2;">Arial</div>
                            <div v-bind:class="[bookSettings.fontFamily=='Verdana,sans-serif'?'settings__active':'', 'settings__font', 'settings__font_verdana']" v-on:click="$store.commit(types.SET_FONT_FAMILY, 'Verdana,sans-serif'); fontFamily=3;">Verdana</div>
                        </div>
                    </div>
                </div>
            </div>
        </transition>
</div>
</template>

<script>
    import * as types from '../store/mutation-types'
    import { mapState } from 'vuex'

    export default {
        data() {
            return {
                showSettings: false,
                types: types,
                lineHeight: 2,
                alignment: 2,
                fontFamily: 3,
                theme:1,
            }
        },

    computed: {
    ...mapState([
    'bookSettings'
    ])
    },

    }
</script>

<style lang="less" scoped>
    @import "../less/constants.less";
    @button-right-position: 30px;
    .fade-enter-active, .fade-leave-active {
    transition: opacity .5s;
    }
    .fade-enter, .fade-leave-to /* .fade-leave-active до версии 2.1.8 */ {
    opacity: 0;
    }

    .settings-button {
        font-size: 1.5em;
        position: fixed;
        top: 80px;
        right: @button-right-position;
    }

    .dropdown {
        width: 312px;
        position: fixed;
        background: white;
        top: 125px;
        right: @button-right-position;
        border-radius: 2px;
        box-shadow: 0 0 10px -1px rgba(0,0,0,.2);
        color: #302119;
        margin: 14px 0 0;
        text-align: left;
        list-style: none;
        transition: opacity .1s ease-in,transform .1s ease-in,visibility .1s ease-in;
        cursor: auto;
        will-change: transform;
    }

    .dropdown__tail {
        position: absolute;
        top: -30px;
        right: 10px;
        height: 30px;
        width: 30px;
        overflow: hidden;
        &::before{
    background-color:white;
        content: "";
        position: absolute;
        width: 18px;
        height: 18px;
        top: 21px;
        left: 6px;
        box-shadow: 0 0 10px -1px rgba(0,0,0,.2);
        transform: rotate(45deg);
        }
    }

    .settings {
        min-width: 312px;
        color: #9a938d;
    }

    .settings__table {
        display: table;
        width: 100%;
    }

    .settings__cell {
        font-size: 14px;
        display: table-cell;
        height: 48px;
        text-align: center;
        vertical-align: middle;
        -webkit-box-sizing: border-box;
        box-sizing: border-box;
        cursor: pointer;
    }

    .settings__icon {
        display: inline-block;
        vertical-align: top;
        fill: #9a938d;
        width: 24px;
        height: 24px;
    }

    .settings__font-size {
        border-bottom: 1px solid #efedec;
    }

    .settings__bigger, .settings__smaller {
        font-weight: 700;
        width: 50%;
    }

    .settings__bigger {
        font-size: 24px;
        border-left: 1px solid #efedec;
    }

    .settings__line-height {
        margin-top: 8px;
        border-bottom: 1px solid #efedec;
    }

    .settings__themes {
        border-bottom: 1px solid #efedec;
    }

    .settings__line-height .settings__cell {
        width: 104px;
    }

    .settings__line-height .settings__icon {
        width: 104px;
        height: 48px;
    }

    .settings__cell_theme.settings__active span {
        border-color: #4264fd;
    }

    .settings__alignment .settings__cell_right {
        width: 56px;
    }

    .settings__alignment .settings__cell, .settings__hyphens .settings__cell, .settings__mode .settings__cell {
        text-align: left;
        padding-left: 16px;
        cursor: default;
    }

    .settings__alignment .settings__cell_right, .settings__hyphens .settings__cell_right, .settings__mode .settings__cell_right {
        text-align: right;
        cursor: pointer;
        padding-left: 0;
    }

    .settings__alignment {
        color: #000;
    }

    .settings__cell_theme span {

    display: inline-block;
    height: 32px;
    line-height: 32px;
    width: 72px;
    border-radius: 100px;
    border: 1px solid;
    border-top-color: currentcolor;
    border-right-color: currentcolor;
    border-bottom-color: currentcolor;
    border-left-color: currentcolor;

    }
    .settings__cell_theme-sepia span {

    background: #f5efdc;
    border-color: #dcd1c3;
    color: #6e422f;

    }
    .settings__cell_theme-black span {
    background: #000;
    border-color: #49423f;
    color: #bababa;
    }

    .settings__font_times {
        font-family: Times New Roman,serif;
    }

    .settings__font_arial {
        font-family: Arial,sans-serif;
    }

    .settings__font_verdana {
        font-family: Verdana,sans-serif;
    }

    .settings__font {
        display: block;
        height: 48px;
        font-size: 18px;
        text-align: center;
        line-height: 48px;
        color: #000;
        border-bottom: 1px solid #efedec;
        cursor: pointer;
    }

    .settings__active {
    color: #4264fd;
    }

    .settings__active .settings__icon {
    fill: #4264fd;
    }
</style>
