import axios from 'axios';

export const API_PRODUCT_URL = "api/orders";

export function getOrders(status, page, rowPerPage) {
    return axios.get(API_PRODUCT_URL, {
        params: {
            status: status,
            skipCount: ((page) * rowPerPage),
            maxResultCount: rowPerPage
        }
    });
}

export function getOrder(orderId) {
    return axios.get(API_PRODUCT_URL + "/" + orderId);
}

export function approveOrder(orderId) {
    return axios.put(API_PRODUCT_URL + "/approve/" + orderId);
}

export function rejectOrder(orderId, reason) {
    return axios.put(API_PRODUCT_URL + "/reject", { id: orderId, reason: reason });
}

export function updateAirWaybill(orderId, airWaybill) {
    return axios.put(API_PRODUCT_URL + "/airwaybill", { id: orderId, airWaybill: airWaybill });
}