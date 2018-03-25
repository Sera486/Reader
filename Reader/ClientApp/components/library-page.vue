<template>
    <div class="book-preview-container">
        <loading-wheel v-bind:loading="!books"></loading-wheel>
        <book-preview v-for="book in books" v-bind="book"></book-preview>
    </div>
</template>

<script>
    import { getBook } from '../store/api'

	export default {
        data() {
            return {
                books: null
            }
        },

        async created() {
            this.books = await getBook();
        }
	 }
</script>

<style lang="less" scoped>
    .book-preview-container{
        display:flex;
        flex-wrap:wrap;
        justify-content:space-around;
    }
</style>
