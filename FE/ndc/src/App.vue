<script setup>
import Ndc from './components/Ndc.vue';
import { ref, onMounted } from 'vue';
import ErrorToast from './components/common/ErrorToast.vue';

const isLoading = ref(false);

onMounted(() => {
  window.addEventListener('loadingStatus', handleLoading);
});

function handleLoading(event) {
  isLoading.value = event.detail.isLoading;
}

</script>

<template>
  <div id="app">
    <div v-if="isLoading" class="spinner-container">
      <div class="spinner-border text-primary" role="status">
        <span class="visually-hidden">Loading...</span>
      </div>
    </div>
    <ndc msg="Nasa Data Visualizer" />
    <error-toast />
  </div>
</template>

<style scoped>
.spinner-container {
  position: fixed;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background-color: rgba(255, 255, 255, 0.8);
  display: flex;
  justify-content: center;
  align-items: center;
  z-index: 10000;
}
</style>
