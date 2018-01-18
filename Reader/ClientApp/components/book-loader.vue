<template>
    <div>
        <div class="form-group">
            <div>
                <label class="control-label"></label>
                <span class="btn btn-default btn-block">
                    <input type="file" id="file">
                </span>
                <hr>
            </div>
        </div>
        <div>
            <button class="btn waves-effect waves-teal" v-on:click="saveBook">Загрузить</button>
        </div>
    </div>
</template>

<script>
    import { postBook} from '../store/api'

    export default {
        data() {
            return {
            }
        },

        methods: {
            async saveBook() {
                let response = await postBook(document.getElementById('file').files[0]);
                console.log(response);
                console.log("save book result" + "response:" + response + "\n response data" + response.data);
                this.$emit("bookLoaded", response);
            }
        }
	 }
</script>

<style lang="less">

</style>
