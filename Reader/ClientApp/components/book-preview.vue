<template> 
     <div class="book-preview">
         <div v-if="this.control == 'RemoveFromCatalog' && catalogId!=0" class="control-button" v-on:click="removeFromCatalog">-</div>
         <div v-if="this.control == 'AddToCatalog' && $store.state.user!=null" class="control-button" v-on:click="viewCatalogs">+</div>
<ul class="list-group catalog-list" v-bind:id="'list-container-'+id"></ul>

         <div class="image-container" v-on:click="openBook">
             <img class="cover" :src="cover" alt="cover" />
         </div>
         <div class="title">{{title}}</div>
         <div class="author">{{authors[0].author.name+' '+authors[0].author.surname}}</div>
     </div>
</template>

<script>
    import { getCatalogNames, postAddBookToCatalog, postRemoveBookFromCatalog } from '../store/api'
import { setTimeout } from 'core-js';

    export default {
        props: ['id', 'title', 'authors', 'cover', 'control', 'catalogId'],

        data() {
            return {
            }
        },

        methods: {
            openBook() {
                this.$router.push({ name: 'book', params: { id: this.id } });
            },
            async removeFromCatalog(e) {
                let response = await postRemoveBookFromCatalog(this.catalogId, this.id);
                let bookPreviewEl = e.target.parentElement;
                
                bookPreviewEl.parentElement.removeChild(bookPreviewEl);
                
            },
            async viewCatalogs(e) {
                let $this = this;
                let catalogs = await getCatalogNames(this.$store.state.user.id, this.id);

                let list = document.getElementById("list-container-" + this.id);
                list.innerHTML = "";
                setTimeout(function () {
                    list.innerHTML = "";
                },3000)
                catalogs.forEach(function (item) {
                    let listItem = document.createElement("li");
                    listItem.setAttribute("class", "list-group-item");
                    listItem.innerHTML = item.name;
                    listItem.setAttribute("catalog-id", item.id);
                    listItem.addEventListener("click", function () { $this.addBookToCatalog(item.id, $this.id) }, false);

                    list.appendChild(listItem);
                });
                console.log(catalogs);
                if (catalogs.length == 0) {
                    let item = document.createElement("li");
                    item.setAttribute("class", "list-group-item");
                    item.style.color = "gray";
                    item.style.padding = "0px 15px";
                    item.innerHTML = '<h5>Already in all catalogs</h5>'
                    list.appendChild(item);
                }
                list.style.right = '-'+(list.offsetWidth-12)+"px";
            },
            async addBookToCatalog(catalogId, bookId) {
                console.log(bookId);
                let response = await postAddBookToCatalog(catalogId, bookId);
                let list = document.getElementById("list-container-" + this.id);
                list.innerHTML = "";
            }
        }
    }
</script>

<style lang="less" >
    .book-preview {
        padding: 15px;
        margin: 10px;
        width: 250px;
        height: 400px;
        box-shadow: 2px 0 7px rgba(0, 0, 0, 0.3);
        display: inline-block;
        border-radius: 3px;
        /*overflow-x: visible;
        overflow-y: hidden;*/
        transition: box-shadow 0.5s;
        position: sticky;
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

    .control-button {
        box-shadow: 2px 0 7px rgba(0, 0, 0, 0.3);
        background-color: white;
        border-radius: 4px;
        right: 15px;
        top: 20px;
        padding: 5px 12px;
        padding-bottom: 5px;
        position: absolute;
        display: flex;
        z-index: 1;
        margin: auto;
        font-size: 20px;
        font-weight: 600;
        vertical-align: middle;
        cursor:pointer;
        &:hover
        {
        box-shadow: 4px 0 10px rgba(0, 0, 0, 0.3);
        }
    }

    .catalog-list {
        position: absolute;
        right: -105px;
        top: 20px;
        z-index: 2;
    background-color:white;
    }
        .title {
    white-space: nowrap;
    overflow: hidden;
    text-overflow: ellipsis;
        }

        .author {
    float:right;
            color: dimgrey;
            font-size: 0.85em;
        }
    }
</style>
