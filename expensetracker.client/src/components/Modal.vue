<template>
    <div class="modal_background" @click.self="closeModal">
        <div class="modal">
            <div id="modal-title-id">{{title}}</div>
            <div id="modal-close-id" @click="closeModal"><a>&times;</a></div>
            <slot></slot>
        </div>
    </div>
</template>

<script>
    export default {
        data() {
            return {
            }
        },
        props: ["title"],
        methods: {
            closeModal() {
                this.$emit('close');
            }
        }
    }
</script>

<style scoped>
    .modal {
        background-color: #fefefe;
        margin: auto;
        padding: 20px;
        border: 1px solid #888;
        width: auto;
        height: auto;
        max-width: 600px;
        position: relative;
    }

    .modal_background {
        left: 0;
        top: 0;
        position: fixed;
        overflow: auto;
        background: #b0b0b0a0;
        width: 100%;
        height: 100%;
        display: flex;
        justify-content: center;
        align-items: center;
    }

    #modal-title-id {
        display: inline-block;
    }

    #modal-close-id {
        display: inline-block;
        float: right;
        cursor: pointer;
        font-size: 18px;
    } 
</style>