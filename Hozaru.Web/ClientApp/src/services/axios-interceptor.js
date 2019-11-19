import axios from 'axios';
import { API_KEY, API_URL } from '../configuration';

axios.defaults.baseURL = API_URL;
axios.interceptors.request.use(
    config => {
        config.headers["X-Api-Key"] = API_KEY;
        return config;
    }, function (error) {
        // Do something with request error
        return Promise.reject(error);
    }
);

export default axios;