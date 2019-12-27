import { API_URL } from "../../configuration";
import Alert from "../partials/layout/Alert";
import * as authCrud from "../crud/auth.crud";
import * as auth from "../store/ducks/auth.duck";
import store from "../store/store";
import axios from "axios";

export function setupAxios(axios, store) {
    axios.defaults.baseURL = API_URL;
    axios.interceptors.request.use(
        config => {
            const { auth: { authToken } } = store.getState();
            //var apiKey = "C5BFF7F0-B4DF-475E-A331-F737424F013C";
            if (authToken) {
                config.headers.Authorization = "Bearer " + authToken;
            }
            return config;
        },
        err => Promise.reject(err)
    );

    axios.interceptors.response.use(
        response => response,
        errorResponseHandler
    );
}

function errorResponseHandler(error) {
    if (error.config.hasOwnProperty('errorHandle') && error.config.errorHandle === false) {
        return Promise.reject(error);
    }

    if (error.response.status == 401 && !error.config._retry) {
        return unAuthrorizeHandler(error);
    }
    else {
        if (error.response.headers["token-expired"] !== "true") {
            var defaultErrorMessage = 'Terjadi sesuatu yang tidak terduga sehingga tidak bisa menyelesaikan permintaan Anda. Kami mohon maaf.';
            if (error.response) {
                var message = '';
                if (typeof error.response.data === "string") {
                    message = error.response.data;
                } else {
                    if (error.response.data.Message) {
                        message = error.response.data.Message;
                    } else if (typeof error.response.data.errors === "object") {
                        message = error.response.data.errors[Object.keys(error.response.data.errors)[0]][0];
                    } else {
                        message = defaultErrorMessage;
                    }
                }
                Alert.error(message);
            } else {
                Alert.error(error.message);
            }
        }
    }
    return Promise.reject(error);
}

function unAuthrorizeHandler(error) {
    if (error.response.headers["token-expired"] === "true") {
        const { auth: { authToken, refreshToken } } = store.getState();
        const originalRequest = error.config;
        return authCrud.refreshToken(authToken, refreshToken)
            .then(({ data: { RefreshToken, AccessToken: { Token } } }) => {
                store.dispatch({ type: auth.actionTypes.RefreshToken, payload: { authToken: Token, refreshToken: RefreshToken } });
                originalRequest._retry = true;
                return axios.request(originalRequest)
            })
            .catch(() => {
                store.dispatch({ type: auth.actionTypes.Logout });
                error.config.handled = true;
                return Promise.reject(error);
            });
    }
    return Promise.reject(error);
}