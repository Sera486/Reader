<template>
    <div>
        <p v-if="!htmlBook"><em>Loading...</em></p>
        <div v-if="htmlBook" v-html="htmlBook">

        </div>
    </div>
</template>

<script>
    import axios from 'axios'
    import api from '../store/api'

	export default {
        data() {
            return {
                htmlBook: null
            }
        },
        
        async created() {
            let book = await api.getBook(this.$route.params.id);
            document.title = book.title;
            this.htmlBook = (await axios.get(book.fileURL)).data;
            
        }
	 }
</script>

<style lang="less">

    body {
        background-color: #FEFEFE;
        color: #000000;
        font-family: Verdana, Geneva, Arial, Helvetica, sans-serif;
        text-align: justify
    }

    small {
        font-size: 80%
    }

    p{
        display:block;
        margin:0px;
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

</style>
