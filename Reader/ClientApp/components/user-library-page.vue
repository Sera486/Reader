<template>
    <div style="padding:0px 40px">
        <loading-wheel v-if="catalogs==null"></loading-wheel>

            <h4>Create new catalog</h4>
            <div class="col-sm-5">
                <input type="text" class="form-control col-xs-5" id="catalog-name" />
            </div>
            <div class="btn btn-default" v-on:click="createCatalog">Create</div>

        <book-catalog v-for="catalog in catalogs" v-bind="catalog"></book-catalog>
    </div>
</template>

<script>
    import { getUserCatalogs, getUploadedBooksCatalog , postNewCatalog } from '../store/api'
	export default {
        data() {
            return {
                catalogs: null,
            }
        },

        methods: {
            async createCatalog(e) {
                let response = await postNewCatalog(document.getElementById('catalog-name').value);
                this.catalogs = await getUserCatalogs(this.$store.state.user.id);
            }
        },

        async created() {
            this.catalogs = [];
            console.log(this.$store.state.user.id);
            this.catalogs = await getUserCatalogs(this.$store.state.user.id);
            console.log(this.catalogs);
        }
	 }
</script>

<style lang="less" scoped>

</style>
