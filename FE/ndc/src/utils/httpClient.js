import axios from "axios";

const httpClient = axios.create({
    baseURL: 'http://localhost:5084/api',
    timeout: 10000,
    headers: {
        'Content-Type': 'application/json',
    }
});

const loadingRequests = new Set();

const emitLoadingStatus = () => {
    const isLoading = loadingRequests.size > 0;
    window.dispatchEvent(new CustomEvent('loadingStatus', {
        detail: { isLoading }
    }));
};

const emitError = (message) => {
    window.dispatchEvent(new CustomEvent('error', {
        detail: { message }
    }));
};

httpClient.interceptors.request.use(config => {
    const requestId = Symbol();
    loadingRequests.add(requestId);

    emitLoadingStatus();

    return { ...config, __requestId: requestId };
});

httpClient.interceptors.response.use(
    response => {
        const requestId = response.config.__requestId;
        loadingRequests.delete(requestId);
        emitLoadingStatus();
        return response;
    },
    error => {
        const requestId = error.config?.__requestId;
        if (requestId) loadingRequests.delete(requestId);

        emitLoadingStatus();

        let message = 'Error while executing request';

        if (error.response) {
            message = `Error ${error.response.status}: ${error.response.data.message || 'Unknown error'}`;
        } else if (error.request) {
            message = 'No response from server';
        }

        emitError(message);

        return Promise.reject(error);
    }
);

export default httpClient;