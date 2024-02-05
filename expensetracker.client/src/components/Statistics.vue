<template>
    <Pie :data="data" :options="options" />
</template>

<script>
    import { Chart as ChartJS, ArcElement, Tooltip, Legend } from 'chart.js'
    import { Pie } from 'vue-chartjs'
    import { store } from '../store.js'

    ChartJS.register(ArcElement, Tooltip, Legend)

    export default {
        components: {
            Pie
        },
        setup() {
            return { store };
        },
        data() {
            return {
                options: {
                    responsive: true,
                    maintainAspectRatio: false
                }
            };
        },
        methods: {
            random(min, max) {
                return Math.random() * (max - min) + min;
            },
            generateColor() {
                var red = this.random(100, 255);
                var green = this.random(100, 255);
                var blue = this.random(100, 255);
                return "rgb(" + red + "," + green + "," + blue + ")";
            },
            getData() {
                var categories = new Set();
                var spendings = new Map();
                var colors = [];
                for (var i = 0; i < store.expenses.length; i++) {
                    var expense = store.expenses[i];
                    if (expense.category) {
                        categories.add(expense.category);
                        if (!spendings.has(expense.category))
                            spendings.set(expense.category, 0);
                        spendings.set(expense.category, spendings.get(expense.category) + expense.amount);
                    }
                }

                for (var i = 0; i < spendings.size; i++)
                    colors.push(this.generateColor());

                return {
                    labels: Array.from(categories),
                    datasets: [
                        {
                            backgroundColor: colors,
                            data: Array.from(spendings.values())
                        }
                    ]
                };
            }
        },
        computed: {
            data: function () {
                return this.getData();
            },
        }
    };
</script>