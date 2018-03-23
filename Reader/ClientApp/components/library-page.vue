<template>
    <div>
        <book-preview v-for="book in books" v-bind="book"></book-preview>
    </div>
</template>

<script>
    import { getBook } from '../store/api'

	export default {
        data() {
            return {
                books: []
            }
        },

        async created() {
            this.books = await getBook();
        }
	 }
</script>

<style lang="less" scoped>
</style>
