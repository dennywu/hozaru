import axios from 'axios';

export const API_PRODUCT_URL = "api/product";

export function getProducts() {
    return axios.get(API_PRODUCT_URL + "/all");
}

export function getProduct(productId) {
    return axios.get(API_PRODUCT_URL + "/" + productId);
}

export function createProduct(data, config) {
    return axios.post(API_PRODUCT_URL, data, config);
}

export function editProduct(data, config) {
    return axios.put(API_PRODUCT_URL, data, config);
}

export function archiveProduct(data, config) {
    return axios.put(API_PRODUCT_URL + "/" + data + "/archive", data, config);
}

export function activateProduct(data, config) {
    return axios.put(API_PRODUCT_URL + "/" + data + "/activate", data, config);
}