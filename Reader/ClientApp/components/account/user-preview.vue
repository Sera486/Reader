<template>
    <div>
        <img class="img-responsive img-circle" src="" alt="">
        <div class="username">{{user.userName}}</div>
    </div>
</template>

<script>  
    import { mapState } from 'vuex'
    export default {
        data() {
            return {
            }
        },

    computed: {
            ...mapState([
                'user',
            ])
        }
    }
</script>

<style lang="less" scoped>
    .username{
        color:skyblue;
    }
</style>
