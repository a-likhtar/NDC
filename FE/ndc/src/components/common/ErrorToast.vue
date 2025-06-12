<script setup>
import { ref, onMounted } from 'vue';
import { Toast } from 'bootstrap';

const message = ref('');
const toast = ref(null);
let toastInstance = null;

onMounted(() => {
    toastInstance = new Toast(toast.value, {
        autohide: true,
        delay: 5000
    });

    window.addEventListener('error', (event) => {
        message.value = event.detail.message;
        toastInstance.show();
    });
});

</script>

<template>
    <div class="position-fixed bottom-0 end-0 p-3" style="z-index: 10000;">
        <div ref="toast" id="errorToast" class="toast align-items-center text-bg-danger border-0" role="alert"
            aria-live="assertive" aria-atomic="true">
            <div class="d-flex">
                <div class="toast-body">
                    {{ message }}
                </div>
                <button type="button" class="btn-close btn-close-white me-2 m-auto" data-bs-dismiss="toast"
                    aria-label="Close">
                </button>
            </div>
        </div>
    </div>
</template>