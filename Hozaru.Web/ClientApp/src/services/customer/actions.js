import { CHANGE_CUSTOMER_INFO } from './actionTypes';

export const changeCustomerInfo = (customer) => dispatch => {
    dispatch({
        type: CHANGE_CUSTOMER_INFO,
        payload: customer
    });
};