import axios from 'axios';

export const API_TENANT_URL = "api/tenants";

export function getTenant() {
    return axios.get(API_TENANT_URL);
}

export function editTenant(data, config) {
    return axios.put(API_TENANT_URL, data, config);
}