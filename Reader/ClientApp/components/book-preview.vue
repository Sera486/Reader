p<template> 
        <div class="book-preview">
            <div class="image-container" v-on:click="openBook">
                <img class="cover" :src="cover" alt="cover">
            </div>
            <div class="title">{{title}}</div>
            <div class="author">{{author}}</div>
        </div>
</template>

<script>
    export default {
        data() {
            return {
            }
        },

        methods: {
            openBook() {
                this.$router.push({ name: 'book', params: { id: this.id } });
            }
        },

        props: ['id','title', 'author', 'cover']
    }
</script>

<style lang="less" >
    .book-preview {
        padding: 15px;
        margin: 10px;
        width: 250px;
        height: 400px;
        box-shadow: 2px 0 7px rgba(0, 0, 0, 0.3);
        display:inline-block;
        border-radius: 3px;
        overflow: hidden;
        transition: box-shadow 0.5s;
        &:hover {
            box-shadow: 4px 0 10px rgba(0, 0, 0, 0.3);
        }

        .cover{
            cursor: pointer;
        }

    .image-container {
        margin-bottom: 10px;
        display: block;
        width: 100%;
        height: 90%;
        position: relative;
        overflow: hidden;
        img {
            margin:auto;
            display: block;
            max-width: 100%;
            max-height: 100%;
            position: absolute;
            top: 0;
            bottom: 0;
            left: 0;
            right: 0;
            }
        }


        .title {
        }

        .author {
            color: dimgrey;
            font-size: 0.85em;
        }
    }
</style>
