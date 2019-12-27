import { CHANGE_TENANT_INFO } from './actionTypes';

export const changeTenantInfo = (tenantInfo) => dispatch => {
    dispatch({
        type: CHANGE_TENANT_INFO,
        payload: tenantInfo
    });
};