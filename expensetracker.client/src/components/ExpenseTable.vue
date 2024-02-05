<template>
    <div class="expense-component">
        <div v-if="loading" class="loading">
            Loading...
        </div>

        <div v-if="!loading && error">
            <a @click="getExpenses();getCurrencies();getCategories();">Could not connect to server. Click here to refresh</a>
        </div>

        <div v-if="!loading && !error && store.expenses" class="content">
            <div id="buttons">
                <button @click="showAddExpense">Add expense</button>
                <button @click="clearExpenses">Clear expenses</button>
            </div>
            <table>
                <thead>
                    <tr>
                        <th>Amount:</th>
                        <th>Currency:</th>
                        <th>Category:</th>
                        <th>Recipient:</th>
                        <th>Description:</th>
                        <th>Date:</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="expense in store.expenses" :key="expense.id">
                        <td>{{ expense.amount }}</td>
                        <td>{{ expense.currency }}</td>
                        <td>{{ expense.category }}</td>
                        <td>{{ expense.recipient }}</td>
                        <td>{{ expense.description }}</td>
                        <td>{{ formattedDate(expense.date) }}</td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>

    <div class="modal">
        <Modal v-show="addExpenseActive" @close="closeAddExpense" title="Add new expense">
            <ExpenseForm @cancel="closeAddExpense" @submit="closeAddExpense"/>
        </Modal>
    </div>
</template>

<script lang="js">
    import ExpenseForm from './ExpenseForm.vue'
    import Modal from './Modal.vue'
    import { store } from '../store.js'

    export default {
        components: { Modal, ExpenseForm },
        setup() {
            return { store };
        },
        data() {
            return {
                addExpenseActive: false,
                loading: false,
                error: ''
            };
        },
        created() {
            this.getExpenses();
            this.getCurrencies();
            this.getCategories();
        },
        watch: {
            '$route': 'getExpenses'
        },
        methods: {
            async getExpenses() {
                this.loading = true;
                try {
                    var response = await fetch('expenses/getExpenses');
                    if (!response.ok)
                        throw new Error('The server responded with error!');
                    var json = await response.json();
                    this.loading = false;
                    this.error = null;
                    store.expenses = json;
                }
                catch (error) {
                    this.loading = false;
                    this.error = error.message;
                    console.error('Failed to get expenses:', error);
                }
            },
            async clearExpenses() {
                try {
                    var response = await fetch('expenses/clearExpenses');
                    if (!response.ok)
                        throw new Error('The server responded with error!');
                    this.loading = false;
                    store.expenses = [];
                }
                catch (error) {
                    console.error('Failed to get expenses:', error);
                }
            },
            showAddExpense() {
                this.addExpenseActive = true;
            },
            closeAddExpense() {
                this.addExpenseActive = false;
            },
            formattedDate(date) {
                if (date === null)
                    return "-";
                if (date === undefined)
                    return "-";
                if (typeof date == 'string')
                    date = new Date(date);
                const year = date.getFullYear();
                const month = date.getMonth() + 1;
                const day = date.getDate();
                return `${year}-${month.toString().padStart(2, '0')}-${day.toString().padStart(2, '0')}`;
            },
            async getCurrencies() {
                try {
                    var response = await fetch('currencies/getCurrencies');
                    if (!response.ok)
                        throw new Error('The server responded with error!');
                    var json = await response.json();
                    store.currencies = json;
                }
                catch (error) {
                    console.error('Failed to get currencies:', error);
                }
            },
            async getCategories() {
                try {
                    var response = await fetch('categories/getCategories');
                    if (!response.ok)
                        throw new Error('The server responded with error!');
                    var json = await response.json();
                    store.categories = json;
                }
                catch (error) {
                    console.error('Failed to get categories:', error);
                }
            }
        },
    }
</script>

<style scoped>

    #buttons {
        text-align: left;
    }

    th {
        font-weight: bold;
    }

    tr:nth-child(even) {
        background: #F2F2F2;
    }

    tr:nth-child(odd) {
        background: #FFF;
    }

    th, td {
        padding-left: .5rem;
        padding-right: .5rem;
    }

    .expense-component {
        text-align: center;
    }

    table {
        width: 100%;
    }
</style>
