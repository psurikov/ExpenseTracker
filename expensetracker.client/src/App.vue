<template>

    <header class="title">
        <h1>Expense Tracker</h1>
    </header>

    <section class="layout">
        <div class="sidebar">
            <div class="menu">
                <div class="menu-item" @click="switchMenu('expenses')"><a href="#expenses">Expenses</a></div>
                <div class="menu-item" @click="switchMenu('statistics')"><a href="#statistics">Statistics</a></div>
                <div class="menu-item" @click="switchMenu('about')"><a href="#about">About</a></div>
            </div>
        </div>
        <div class="content">
            <div class="container">

                <div id="expenses" class="menu-content active">
                    <ExpenseTable />
                </div>

                <div id="statistics" class="menu-content">
                    <Statistics />
                </div>

                <div id="about" class="menu-content">
                    <About />
                </div>

            </div>
        </div>
    </section>

</template>

<script lang="js">
    import ExpenseTable from './components/ExpenseTable.vue'
    import ExpenseForm from './components/ExpenseForm.vue'
    import Statistics from './components/Statistics.vue'
    import About from './components/About.vue'
    import Modal from './components/Modal.vue'

    export default {
        components: { ExpenseTable, ExpenseForm, Statistics, About, Modal },
        data() {
            return {
                activeMenu: 'expenses',
            };
        },
        created() {
        },
        methods: {
            switchMenu(id) {
                var tabs = document.querySelectorAll('.menu-content');
                for (var i = 0; i < tabs.length; i++) {
                    tabs[i].classList.remove('active');
                }
                document.getElementById(id).classList.add('active');
                //this.$refs.name.classList.remove('active');
            },
        },
    };
</script>

<style scoped>

    .title {
        text-align: left;
        margin: 0px 0px 50px 0px
    }

    .layout {
        display: grid;
        grid: "sidebar body" 1fr / auto 1fr;
        gap: 16px;
    }

    .sidebar {
        grid-area: sidebar;
        border-right-width: 1px;
        border-right-color: gray;
        border-right-style: solid;
    }

    .menu-content {
        display: none;
        overflow: hidden;
    }

    .menu-content.active {
        display: block;
    }

    .menu {
        display: flex;
        flex-direction: column;
        margin: 0px 100px 0px 0px
    }

    .menu-item {
        text-align: left;
    }

    .menu-item a {
        font-size: 20px;
        text-decoration: none;
        display: block;
    }

    .menu-item a:hover {
        color: black;
    }
</style>
