import axios from 'axios';
import { API_URL } from '../configuration';

axios.defaults.baseURL = API_URL;
axios.interceptors.request.use(
    config => {
        var apiKey = window.ApiKey && window.ApiKey.apiKey;
        config.headers["X-Api-Key"] = apiKey;
        return config;
    }, function (error) {
        // Do something with request error
        return Promise.reject(error);
    }
);

export default axios;