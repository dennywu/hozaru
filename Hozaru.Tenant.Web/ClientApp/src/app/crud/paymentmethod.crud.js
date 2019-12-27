import axios from 'axios';

export const API_PAYMENT_METHOD_URL = "api/paymentmethods";

export function getPaymentMethods(page, rowPerPage) {
    return axios.get(API_PAYMENT_METHOD_URL + "/full", {
        params: {
            skipCount: ((page) * rowPerPage),
            maxResultCount: rowPerPage
        }
    });
}

export function getPaymentMethod(paymentMethodId) {
    return axios.get(API_PAYMENT_METHOD_URL + "/" + paymentMethodId);
}

export function getBanks(searchKey) {
    return axios.get(API_PAYMENT_METHOD_URL + "/banks", {
        params: {
            searchKey: searchKey
        }
    });
}

export function createPaymentMethod(data) {
    return axios.post(API_PAYMENT_METHOD_URL, data);
}

export function updatePaymentMethod(data) {
    return axios.put(API_PAYMENT_METHOD_URL, data);
}