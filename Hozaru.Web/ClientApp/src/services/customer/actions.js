import { CHANGE_CUSTOMER_INFO, VALIDATE } from './actionTypes';

export const changeCustomerInfo = (customer) => dispatch => {
    dispatch({
        type: CHANGE_CUSTOMER_INFO,
        payload: customer
    });
};

export const validateCustomerInfo = () => dispatch => {
    dispatch({
        type: VALIDATE
    });
};