<script setup>
import NdcTable from './tables/NdcTable.vue';
import NdcSelect from './base/NdcSelect.vue';
import { ref, watch } from 'vue';
import httpClient from '../utils/httpClient';
import { generateRange } from '../utils/helpers';

const selectedName = ref(null);
const selectedClass = ref(null);
const selectedYearFrom = ref(null);
const selectedYearTo = ref(null);
const selectedSortOrder = ref({
    sortBy: 'Year',
    sortDescending: false,
});
const meteorites = ref([]);

const classesApiUrl = import.meta.env.VITE_API_CLASSES_URL;
const meteoritesGroupedApiUrl = import.meta.env.VITE_API_GROUPED_URL;

const years = generateRange(new Date(500, 0), new Date());

const fetchMeteorites = async () => {
    const queryParams = {
        NameContains: selectedName.value,
        MeteoriteClassId: selectedClass.value,
        YearFrom: selectedYearFrom.value,
        YearTo: selectedYearTo.value,
        SortDescending: selectedSortOrder.value.sortDescending,
        SortBy: selectedSortOrder.value.sortBy
    };

    const response = await httpClient.get(meteoritesGroupedApiUrl, { params: queryParams });
    meteorites.value = response.data;
}

watch(
    () => selectedSortOrder.value,
    async () => {
        await fetchMeteorites();
    }
);

</script>

<template>
    <div class="container">
        <div class="row my-3 align-items-center">
            <h3 class="my-3">Specify filters to search</h3>
            <div class="col">
                <div class="form-floating">
                    <input v-model="selectedName" type="text" class="form-control" id="meteoriteName" placeholder="Meteorite name">
                    <label for="meteoriteName">Meteorite name</label>
                </div>
            </div>
            <div class="col">
                <ndc-select
                    v-model="selectedClass"
                    :api-url="classesApiUrl"
                    placeholder="Meteorite class"    
                />
            </div>
            <div class="col">
                <ndc-select
                    v-model="selectedYearFrom"
                    placeholder="Year From"
                    :static-options="years"
                />
            </div>
            <div class="col">
                <ndc-select
                    v-model="selectedYearTo"
                    placeholder="Year To"
                    :static-options="years"
                />
            </div>
            <div class="col">
                <button @click="fetchMeteorites" class="btn btn-primary w-100">
                    <i class="bi bi-search"></i>
                </button>
            </div>
        </div>
        <div class="row">
            <ndc-table :meteorites="meteorites" v-model:sort="selectedSortOrder"/>
        </div>
    </div>
</template>

<style scoped></style>