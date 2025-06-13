<script setup>
import { computed } from 'vue';

    const props = defineProps({
        meteorites: {
            groups: [],
            totalCount: Number,
            totalMass: Number,
        },
        sort: {
            sortBy: String,
            sortDescending: Boolean,
        },
    });

    const sortIconClass = computed(() => {
        return {
            'bi bi-sort-numeric-down-alt': props.sort.sortDescending,
            'bi bi-sort-numeric-up': !props.sort.sortDescending,
        };
    });

    const emit = defineEmits(['update:sort']);

    function updateSortOrder(sortBy) {
        let sortDescending = sortBy != props.sort.sortBy ? false : !props.sort.sortDescending
    
        emit('update:sort', { sortBy, sortDescending });
    }
</script>

<template>
    <table class="table table-striped">
        <thead>
            <tr>
                <th scope="col">#</th>
                <th scope="col" class="ndc-table-header" @click="updateSortOrder('Year')">
                    Year
                    <i v-if="props.sort.sortBy == 'Year'" :class="sortIconClass"></i>
                </th>
                <th scope="col" class="ndc-table-header" @click="updateSortOrder('Count')">
                    Meteorites count
                    <i v-if="props.sort.sortBy == 'Count'" :class="sortIconClass"></i>
                </th>
                <th scope="col" class="ndc-table-header" @click="updateSortOrder('TotalMass')">
                    Total mass
                    <i v-if="props.sort.sortBy == 'TotalMass'" :class="sortIconClass"></i>
                </th>
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

<style scoped>
    .ndc-table-header {
        cursor: pointer;
    }
</style>