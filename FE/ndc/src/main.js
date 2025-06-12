import { createApp } from 'vue';
import './assets/css/normalize.css';
import 'bootstrap/dist/css/bootstrap.css';
import 'tom-select/dist/css/tom-select.css';
import { round } from './utils/filters';
import App from './App.vue';

import * as bootstrap from 'bootstrap';
window.bootstrap = bootstrap;

const app = createApp(App);

app.config.globalProperties.$filters = {
    round,
};

app.mount('#app');