<script setup>
import { ref, onMounted } from 'vue';
import TomSelect from 'tom-select';
import httpClient from '../../utils/httpClient';

const props = defineProps({
    modelValue: {
        type: [String, Number, Array, Object],
        default: null,
    },
    apiUrl: {
        type: String,
    },
    id: {
        type: String,
        default: 'select',
    },
    placeholder: {
        type: String,
        default: 'Select value',
    },
    staticOptions: {
        type: Array,
        default: null,
    }
});

const emit = defineEmits(['update:modelValue']);

const selectRef = ref(null);
let selectInstance = null;
let cachedOptions = [];

const loadOptions = async () => {
    if (cachedOptions.length > 0) return cachedOptions;
    
    try {
        const response = await httpClient.get(`${props.apiUrl}`);

        cachedOptions = response.data.map(item => ({
            value: item.id,
            label: item.name,
        }));

        return cachedOptions;
    } catch (error) {
        return [];
    }

}

onMounted(async () => {
    const options = props.staticOptions ?? await loadOptions();

    selectInstance = new TomSelect(selectRef.value, {
        options: options,
        valueField: 'value',
        labelField: 'label',
        searchField: 'label',
        maxItems: 1,
        plugins: ['clear_button'],
        onChange: (value) => {
            emit('update:modelValue', value);
        },
        onLoad: () => {
            if (props.modelValue) {
                selectInstance.setValue(props.modelValue);
            }
        }
    });

    if (props.modelValue) {
        selectInstance.setValue(props.modelValue);
    }
});

</script>

<template>
    <select :id="id" ref="selectRef" :placeholder="placeholder" class="flex-grow-1"></select>
</template>

<style>
    .ts-control {
        border: 1px solid var(--bs-border-color);
        border-radius: 0.375em;
        padding: 19px;
    }

    .plugin-clear_button .clear-button {
        font-size: 30px;
        top: 40%;
    }
</style>