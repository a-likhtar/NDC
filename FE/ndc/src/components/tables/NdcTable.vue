<script setup>
    defineProps({
        meteorites: {
            groups: [],
            totalCount: Number,
            totalMass: Number,
        },
    });
</script>

<template>
    <table class="table table-striped">
        <thead>
            <tr>
                <th scope="col">#</th>
                <th scope="col">Year</th>
                <th scope="col">Meteorites count</th>
                <th scope="col">Total mass</th>
            </tr>
        </thead>
        <tbody>
            <tr v-for="(group, index) in meteorites.groups">
                <th scope="row">{{ index + 1 }}</th>
                <td>{{ group.year }}</td>
                <td>{{ group.meteoritesCount }}</td>
                <td>{{ $filters.round(group.mass) || 'Not specified' }}</td>
            </tr>
        </tbody>
        <tfoot>
            <tr>
                <th scope="row"></th>
                <th>Total</th>
                <th>{{ meteorites.totalCount || 0 }}</th>
                <th>{{ $filters.round(meteorites.totalMass) || 0}}</th>
            </tr>
        </tfoot>
    </table>
</template>

<style scoped></style>