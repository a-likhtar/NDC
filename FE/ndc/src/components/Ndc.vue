<script setup>
import NdcTable from './tables/NdcTable.vue';
import NdcSelect from './base/NdcSelect.vue';
import { ref } from 'vue';
import httpClient from '../utils/httpClient';
import { generateRange } from '../utils/helpers';

const selectedName = ref('');
const selectedClass = ref(null);
const selectedYearFrom = ref(null);
const selectedYearTo = ref(null);
const meteorites = ref([]);

const years = generateRange(new Date(500, 0), new Date());

const fetchMeteorites = async () => {
    const queryParams = {
        NameContains: selectedName.value,
        MeteoriteClassId: selectedClass.value,
        YearFrom: selectedYearFrom.value,
        YearTo: selectedYearTo.value,
        SortDescending: false,
        SortBy: 'Year'
    };

    const response = await httpClient.get('/meteorites/grouped', { params: queryParams });
    meteorites.value = response.data;
}

</script>

<template>
    <div class="container">
        <div class="row my-3">
            <h2>Filters & Search</h2>
            <div class="col">
                <div class="form-floating mb-3">
                    <input v-model="selectedName" type="text" class="form-control" id="meteoriteName" placeholder="Meteorite name">
                    <label for="meteoriteName">Meteorite name</label>
                </div>
            </div>
            <div class="col">
                <ndc-select
                    v-model="selectedClass"
                    :api-url="'http://localhost:5084/api/meteorites/classes'"
                    placeholder="Select meteorite class"    
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
            <button @click="fetchMeteorites" class="btn btn-primary mb-3">Load</button>
        </div>
        <div class="row">
            <ndc-table :meteorites="meteorites"/>
        </div>
    </div>
</template>

<style scoped></style>