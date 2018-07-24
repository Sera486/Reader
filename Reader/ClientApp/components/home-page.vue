<template>
    <div>
        <h2 style="margin-top:120px;margin-left:80px;"> Reader</h2>
        <book-loader v-on:bookLoaded="updateBook"></book-loader>
        <book-preview v-if="book!=null" v-bind="book"></book-preview>
        <ul class="changelog">
            <div>Last updates:</div>
            </br>
            <li v-for="commit in commits">
                {{commit.commit.author.date}} {{commit.commit.message}}
            </li>
        </ul>
    </div>
</template>

<script>
    import Api from '../store/api'
    import axios from 'axios'

export default {
        data() { 
            return {
                book: null,
                commits: null
            }
        },

        methods: {
            updateBook(book) {
                this.book = book;
            },
        },

        async created() {
            let response = await axios.get("https://api.github.com/repos/Sera486/Reader/commits?sha=dev&per_page=10");
            this.commits = response.data;
        }
}
</script>

<style scoped>
    .changelog{
        margin-left:60px;
        margin-top:60px;
    }

    .book-preview{
        margin-left: 40%;
    }
</style>
