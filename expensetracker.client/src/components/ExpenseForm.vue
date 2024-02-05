<template>
    <form @submit.prevent="submitExpense(this.expense)">
        <table>
            <tr>
                <th>Amount:</th>
                <td><input type="number" name="amount" v-model="expense.amount" min="1" max="1000000000" /></td>
            </tr>
            <tr>
                <th>Currency:</th>
                <td>
                    <select name="currency" v-model="expense.currency">
                        <option v-for="currency in store.currencies" :key="currency.id">{{ currency.name }}</option>
                    </select>
                </td>
            </tr>
            <tr>
                <th>Category:</th>
                <td>
                    <select name="category" v-model="expense.category">
                        <option v-for="category in store.categories" :key="category.id">{{ category.name }}</option>
                    </select>
                </td>
            </tr>
            <tr>
                <th>Recipient:</th>
                <td><input type="text" name="recipient" v-model="expense.recipient" required maxlength="100"/></td>
            </tr>
            <tr>
                <th>Description:</th>
                <td><input type="text" name="description" v-model="expense.description" required maxlength="100"/></td>
            </tr>
            <tr>
                <th>Date:</th>
                <td><input type="date" name="date" v-model="expense.date" required/></td>
            </tr>
        </table>
        <div v-if="error" style="color:red">{{error}}</div>
        <button id="cancel-id" @click.prevent="onCancel">Cancel</button>
        <button id="submit-id" type="submit" @click="onAdd">Add</button>
    </form>
</template>

<script>
    import { store } from '../store.js'

    export default {
        setup() {
            return { store };
        },
        data() {
            return {
                expense: {
                    amount: 100,
                    recipient: "",
                    currency: "USD",
                    category: "Books",
                    description: "",
                    date: this.getCurrentDate()
                },
                error:undefined
            }
        },
        methods: {
            async submitExpense(newExpense) {
                try {
                    const response = await fetch('expenses/addExpense', {
                        method: 'POST',
                        headers: {
                            'Content-Type': 'application/json',
                        },
                        body: JSON.stringify(newExpense),
                    });

                    if (!response.ok) {
                        var text = await response.text();
                        var isJson = false;
                        try {
                            JSON.parse(text);
                            isJson = true;
                        }
                        catch (error) { }
                        if (isJson)
                            throw new Error("The server responded with error: " + response.status + ":" + response.statusText);
                        else throw new Error("The server responded with error: " + text);
                    }

                    var addedExpense = await response.json();
                    store.expenses = [...store.expenses, addedExpense];

                    this.error = null;
                } catch (error) {
                    this.error = error.message;
                    console.error('Failed to save the expense:', error);
                }

                this.$emit('submit');
            },
            onCancel() {
                console.log('Cancelled operation.');
                this.$emit('cancel');
            },
            isJson(text) {
                try {
                    JSON.parse(text);
                    return true;
                } catch (err) { }
                return false;
            },
            getCurrentDate() {
                var now = new Date();
                var day = ("0" + now.getDate()).slice(-2);
                var month = ("0" + (now.getMonth() + 1)).slice(-2);
                var today = now.getFullYear() + "-" + (month) + "-" + (day);
                return today;
            }
        }
    }
</script>

<style scoped>

    th {
        text-align: right;
    }

    table {
        border-spacing: 5px;
        margin:10px;
    }

    select {
        width: 100%;
    }

    input {
        width: 100%;
    }

    #submit-id, #cancel-id {
        float: right;
    }

</style>