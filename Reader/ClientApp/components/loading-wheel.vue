<template>
    <div v-show="loading" class="wheel">
        <div class="circle" id="circle-mid">
            <div class="circle" id="circle-left"></div>
            <div class="circle" id="circle-right"></div>
        </div>
    </div>
</template>

<script>
    export default {
        props: {
            loading: {
                type: Boolean,
                default: true
            }
        },
        data() {
            return {
            }
        }
    }
</script>

<style lang="less" scoped>
@import "../less/constants.less";

    .wheel {
        position: fixed;
        top: 50%;
        left: 50%;
    }

    .circle {
        opacity: 0.75;
        background-color: @book-text-color;
        height: 15px;
        width: 15px;
        border-radius: 100%;
    }

    #circle-mid {
        margin-top: auto;
        margin-bottom: auto;
        margin-right: auto;
        margin-left: auto;
        animation: v-rotateStretchDelay 1s 0s infinite cubic-bezier(.7,-.13,.22,.86);
        animation-fill-mode: both;
        display: inline-block;
        position: relative;
    }

    #circle-left {
        position: absolute;
        top: 0;
        left: -25px;
    }

    #circle-right {
        position: absolute;
        top: 0;
        left: 25px;
    }

    @-webkit-keyframes v-rotateStretchDelay {
        0% {
            transform: rotate(0deg);
        }

        50% {
            transform: rotate(180deg);
        }

        100% {
            transform: rotate(360deg);
        }
    }

    @keyframes v-rotateStretchDelay {
        0% {
            transform: rotate(0deg);
        }

        50% {
            transform: rotate(180deg);
        }

        100% {
            transform: rotate(360deg);
        }
    }
</style>
