<template>
    <div>
        <p v-if="!htmlBook"><em>Loading...</em></p>
        <div v-if="htmlBook" v-html="htmlBook" class="book-container">

        </div>
    </div>
</template>

<script>
    import axios from 'axios'
    import { getBook } from '../store/api'

	export default {
        data() {
            return {
                htmlBook: null
            }
        },
        
        async created() {
            let book = await getBook(this.$route.params.id);
            document.title = book.title;
            this.htmlBook = (await axios.get(book.fileURL)).data;
            
        }
	 }
</script>

<style lang="less" scoped>

    p{
        display:block;
        margin:0px;
    }

    a{
        text-decoration:none;
    }

    a:hover{
        text-decoration:none;
    }

    blockquote {
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

    .book-container {
        background-color: #FEFEFE;
        color: #000000;
        font-family: Verdana, Geneva, Arial, Helvetica, sans-serif;
        text-align: justify;
        overflow: auto;
        padding-right: 40px;
        padding-left: 40px;
        max-height: 80vh;
    }
</style>
