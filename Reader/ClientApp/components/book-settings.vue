<template>
    <div>
        <div class="btn btn-default settings-button" v-on:click="showSettings = !showSettings"><i class="glyphicon  glyphicon-cog"></i></div>
        <transition name="fade">
            <div v-if="showSettings" class="settings-container">
                <div class="dropdown dropdown_hidden dropdown_position-top-right">
                    <div class="tail"></div>
                    <div class="settings">
                        <div class="table font-size">
                            <div class="cell smaller" v-on:click="$store.commit(types.DECREASE_FONT_SIZE);">A-</div>
                            <div class="cell bigger" v-on:click="$store.commit(types.INCREASE_FONT_SIZE);">A+</div>
                        </div>
                        <div class="table line-height">
                            <div v-bind:class="[bookSettings.lineHeight == LineHeight.Small?'active':'', 'cell']" v-on:click="setLineHeight(LineHeight.Small)">
                                <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 104 48" class="icon">
                                    <path d="M32 16h40v2H32zm0 4h40v2H32zm0 4h40v2H32z"></path>
                                </svg>
                            </div>
                            <div v-bind:class="[bookSettings.lineHeight == LineHeight.Medium?'active':'', 'cell']" v-on:click="setLineHeight(LineHeight.Medium)">
                                <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 104 48" class="icon">
                                    <path d="M32 16h40v2H32zm0 5h40v2H32zm0 5h40v2H32z"></path>
                                </svg>
                            </div>
                            <div v-bind:class="[bookSettings.lineHeight == LineHeight.Big?'active':'', 'cell']" v-on:click="setLineHeight(LineHeight.Big)">
                                <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 104 48" class="icon">
                                    <path d="M32 16h40v2H32zm0 7h40v2H32zm0 7h40v2H32z"></path>
                                </svg>
                            </div>
                        </div>
                        <div class="table themes">
                            <div v-bind:class="[bookSettings.theme == Theme.White?'active':'', 'cell', 'cell_theme', 'cell_theme-white']" v-on:click="setTheme(Theme.White)">
                                <span>White</span>
                            </div>
                            <div v-bind:class="[bookSettings.theme == Theme.Sepia?'active':'', 'cell', 'cell_theme', 'cell_theme-sepia']" v-on:click="setTheme(Theme.Sepia)">
                                <span>Sepia</span>
                            </div>
                            <div v-bind:class="[bookSettings.theme == Theme.Black?'active':'', 'cell', 'cell_theme', 'cell_theme-black']" v-on:click="setTheme(Theme.Black)">
                                <span>Black</span>
                            </div>
                        </div><div class="table alignment">
                            <div class="cell">
                                <span>Alignment</span>
                            </div>
                            <div v-bind:class="[bookSettings.textAlign=='left'?'active':'', 'cell', 'cell_right']" v-on:click="$store.commit(types.SET_ALIGNMENT, 'left');">
                                <span class="icon"><svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24"><path d="M4 5h16v2H4zm0 4h12v2H4zm0 4h16v2H4zm0 4h12v2H4z"></path></svg></span>
                            </div>
                            <div v-bind:class="[bookSettings.textAlign=='justify'?'active':'', 'cell', 'cell_right']" v-on:click="$store.commit(types.SET_ALIGNMENT, 'justify');">
                                <span class="icon" style="margin-right:20px"><svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24"><path d="M4 5h16v2H4zm0 4h16v2H4zm0 4h16v2H4zm0 4h16v2H4z"></path></svg></span>
                            </div>
                        </div>
                        <div>
                            <div v-bind:class="[bookSettings.fontFamily == FontFamily.Times ? 'active':'', 'font', 'font_times']" v-on:click="setFont(FontFamily.Times)">Times</div>
                            <div v-bind:class="[bookSettings.fontFamily == FontFamily.Arial ? 'active':'', 'font', 'font_arial']" v-on:click="setFont(FontFamily.Arial)">Arial</div>
                            <div v-bind:class="[bookSettings.fontFamily == FontFamily.Verdana ? 'active':'', 'font', 'font_verdana']" v-on:click="setFont(FontFamily.Verdana)">Verdana</div>
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
    import { FontFamily, LineHeight, Theme } from '../store/enums'

    export default {
        data() {
            return {
                showSettings: false,
                types: types,
                FontFamily: FontFamily,
                LineHeight: LineHeight,
                Theme: Theme,
            }
        },
        methods: {
            setFont(fontFamily) {
                this.$store.commit(types.SET_FONT_FAMILY, fontFamily);
            },

            setLineHeight(lineHeight) {
                this.$store.commit(types.SET_LINE_HEGHT, lineHeight); 
            },

            setTheme(theme) {
                this.$store.commit(types.SET_THEME, theme);
            },
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

    .fade-enter, .fade-leave-to {
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

    .tail {
    position: absolute;
    top: -30px;
    right: 10px;
    height: 30px;
    width: 30px;
    overflow: hidden;
        &::before {
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

    .table {
        margin:0px;
        display: table;
        width: 100%;
    }

    .cell {
        font-size: 14px;
        display: table-cell;
        height: 48px;
        text-align: center;
        vertical-align: middle;
        cursor:pointer;
    }

    .icon {
        display: inline-block;
        vertical-align: top;
        fill: #9a938d;
        width: 24px;
        height: 24px;
    }

    .font-size {
        border-bottom: 1px solid #efedec;
        .cell{
            font-weight: 700;
            width: 50%;
            &:hover{
                color: #4264fd;
            }
            .bigger {
                font-size: 24px;
                border-left: 1px solid #efedec;
            }
        }
    }

    .line-height {
        margin-top: 8px;
        border-bottom: 1px solid #efedec;
        .cell {
            width: 104px;
        }
        .icon {
            width: 104px;
            height: 48px;
        }
    }

    .themes {
        border-bottom: 1px solid #efedec;
    }

    .cell_theme.active span {
        border-color: #4264fd;
    }

    .cell_theme span {
        display: inline-block;
        height: 32px;
        line-height: 32px;
        width: 72px;
        border-radius: 100px;
        border: 1px solid;
    }

    .cell_theme-white span {
        background-color: #FEFEFE;
        border-color: #333333;
        color: #333333;
    }

    .cell_theme-sepia span {
        background-color: #f5efdc;
        border-color: #dcd1c3;
        color: #6e422f;
    }

    .cell_theme-black span {
        background-color: #000;
        border-color: #49423f;
        color: #bababa;
    }

    .alignment {
        color: #000;
        .cell_right {
            width: 56px;
            text-align: right;
            cursor: pointer;
            padding-left: 0;
        }
        .cell {
            text-align: left;
            padding-left: 16px;
            cursor: default;
        }
    }

    .font {
        display: block;
        height: 48px;
        font-size: 18px;
        text-align: center;
        line-height: 48px;
        color: #000;
        border-bottom: 1px solid #efedec;
        cursor: pointer;
    }

    .font_times {
        font-family: Times New Roman,serif;
    }

    .font_arial {
        font-family: Arial,sans-serif;
    }

    .font_verdana {
        font-family: Verdana,sans-serif;
    }

    .active {
        color: #4264fd;
        .icon {
            fill: #4264fd;
        }
    }
</style>
