<template>
    <div>
        <book-loader v-on:bookLoaded="updateBook"></book-loader>
        <book-preview v-if="book!=null" v-bind="book"></book-preview>
    </div>
</template>

<script>
import Api from '../store/api'

export default {
        data() { 
            return {
                book: null
            }
        },

        methods: {
            updateBook(book) {
                console.log("Update book" + book);
                this.book = book;
            }
        }
}
</script>

<style>
</style>
