<template>
    <div>
        <loading-wheel v-bind:loading="!htmlBook"></loading-wheel>
        <div v-html="htmlBook"
             v-bind:class="['book-container col-xs-12 col-sm-10 col-sm-offset-1 col-md-8 col-md-offset-2', getTheme()]" 
             v-bind:style="{fontFamily:bookSettings.fontFamily, textAlign:bookSettings.textAlign, fontSize:bookSettings.fontSize+'px', lineHeight:bookSettings.lineHeight}">
        </div>
        <book-settings></book-settings>
    </div>
</template>

<script>
    import axios from 'axios'
    import { getBook } from '../store/api'
    import { mapState, mapMutations } from 'vuex'
import bookSettingsVue from './book-settings.vue';

    export default {
        data() {
            return {
                htmlBook: null,
            }
        },

        computed: {
            ...mapState([
                'bookSettings'
            ])
        },
        methods: {
            getTheme() {
                let elems;
                switch (this.bookSettings.theme) {
                    case 'white':
                        elems = document.getElementsByClassName('row');
                        for (var i = 0; i < elems.length; i++) {
                            elems[i].style.backgroundColor = 'white';
                        }
                        return 'theme-white'
                    case 'sepia':
                        elems = document.getElementsByClassName('row');
                        for (var i = 0; i < elems.length; i++) {
                            elems[i].style.backgroundColor = '#f5efdc';
                        }
                        return 'theme-sepia'
                    case 'black':
                        elems = document.getElementsByClassName('row');
                        for (var i = 0; i < elems.length; i++) {
                            elems[i].style.backgroundColor = 'black';
                        }
                        return 'theme-black'
                }
                return 'white'
            }
        },
        async created() {
            let book = await getBook(this.$route.params.id);
            document.title = book.title;
            this.htmlBook = (await axios.get(book.fileURL)).data;
        }
    }
</script>

<style lang="less">
    @import "../less/constants.less";

    .book-container {
    box-shadow: 2px 0 7px rgba(0, 0, 0, 0.3);
        background-color: #FEFEFE;
        font-family: Verdana, Geneva, Arial, Helvetica, sans-serif;
        text-align: justify;
        text-decoration: none !important;
        overflow: auto;
        padding-right: 40px;
        padding-left: 40px;
        overflow:hidden;
            a {
                text-decoration: none;
                color: inherit;
                &:hover {
                    text-decoration: none;
                }
            }
    }

    .book-container p {
        display: block;
        margin: 0px;
    }

    .book-container blockquote {
        margin-left: 4em;
        margin-top: 1em;
        margin-right: 0.2em;
    }

    hr {
        color: Black
    }

    ul {
        margin-left: 0
    }

    .epigraph {
        width: 50%;
        margin-left: 35%;
    }

    .settings-font-times {
    font-family: Times New Roman,serif;
    }

    .settings-font-kazimir {
    font-family: Kazimir,serif;
    }

    .settings-font-arial {
    font-family: Arial,sans-serif;
    }

    .settings-font-verdana {
    font-family: Verdana,sans-serif;
    }

    .theme-white{

    }

    .theme-sepia {
        box-shadow: 2px 0 7px rgba(110, 60, 47, 0.3);
        background: #f5efdc;
        color: #6e422f;
    }

    .theme-black {
        box-shadow: 2px 0 7px rgba(255, 255, 255, 0.3);
        background: #000;
        color: #bababa;
    }
</style>
