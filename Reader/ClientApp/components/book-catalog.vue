<template>
    <div class="catalog">
        <div>
            <h4 style="display:inline; margin-right:10px">{{name}} </h4>
            <i class="glyphicon glyphicon-edit" style="color:gray;"></i>
        </div>
        <h5 v-if="books.length==0" style="color:grey;">Empty catalog</h5>
        <book-preview v-for="book in books" v-bind="book"></book-preview>       
    </div>
</template>

<script>
    import { getCatalog } from '../store/api'
	export default {
        props: ['id', 'name', 'books'],

        data() {
            return {
            }
        },

        async created() {
            let $this = this;
            this.books.forEach(function (item) {
                item.control = "RemoveFromCatalog";
                item.catalogId = $this.id;
            });
        },
	 }
</script>

<style lang="less" scoped>
    .catalog {
        margin:10px;
        padding:5px;
        box-shadow: 2px 0 7px rgba(0, 0, 0, 0.3);
    }
</style>
