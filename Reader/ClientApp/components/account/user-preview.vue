<template>
    <div>
        <a class="navbar-brand userImgHead" title="Manage">
            <img style="height: 99%;" class="img-responsive img-circle userImgHead" src="" alt="">
        </a>
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
            ...mapState({
                user: state => state.user
            })
        }
    }
</script>

<style lang="less">

</style>
