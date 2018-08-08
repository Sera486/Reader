<template>
    <div class="library-container">
        <h4 class="col-xs-offset-3" style="padding-left:20px;">Search:</h4>
        <div class="search-bar col-xs-offset-3 col-xs-6">
            <input type="text" class="form-control search-input" id="search-input" placeholder="Title or Author"/>
            <div class="btn btn-default" @click="searchBooks">Search</div>
        </div>
        <div class="book-preview-container">
            <loading-wheel v-bind:loading="!books"></loading-wheel>
            <book-preview v-for="book in books" v-bind="book"></book-preview>
            <div style="width: 250px; margin:10px"></div ><div style="width: 250px;margin:10px"></div><div style="width: 250px;margin:10px"></div><div style="width: 250px;margin:10px"></div><div style="width: 250px; margin:10px"></div><div style="width: 250px;"></div>
        </div>
    </div>
</template>

<script>
    import { getBook, getSearchBooks } from '../store/api'
    import { mapState } from 'vuex'

	export default {
        data() {
            return {
                books: null
            }
        },

        computed: {
            ...mapState([
                'user'
            ])
        },
        methods: {
            async searchBooks(e) {
                let searchString = document.getElementById('search-input').value;
                this.books = await getSearchBooks(searchString, searchString);

            }
        },
        async created() {
            let books = await getBook();
            console.log(books);
            books.forEach(function (item) {
                item.control = "AddToCatalog";
            });
            this.books = books;
        }
	 }
</script>

<style lang="less" scoped>
    .library-container{
    padding: 0px 40px;
        display:flex;
        flex-direction:column;
     }
    .search-bar {
        display: flex;
        margin-bottom:20px;
        .search-input
    {
        width: 100%;
        margin-right:5px;
    }
    }
    .book-preview-container{
        display:flex;
        flex-flow: row wrap;
        justify-content:space-between;

    }
</style>
