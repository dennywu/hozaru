import axios from 'axios';

export const API_TENANT_EXPEDITION_SERVICE_URL = "api/expeditions/tenantexpeditionservices";

export function getTenantExpeditionServices() {
    return axios.get(API_TENANT_EXPEDITION_SERVICE_URL);
}

export function updateStatusTenantExpeditionService(tenantExpeditionServiceId, status) {
    var data = {
        id: tenantExpeditionServiceId,
        isActive: status
    };
    return axios.put(API_TENANT_EXPEDITION_SERVICE_URL, data);
}

