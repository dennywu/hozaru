import { LOAD_CART, ADD_PRODUCT, REMOVE_PRODUCT, CHANGE_QUANTITY, CHANGE_FREIGHT, CHANGE_NOTE, CHANGE_PAYMENT_TYPE } from './actionTypes';

export const loadCart = product => ({
    type: LOAD_CART,
    payload: product
});

export const addProduct = (product, quantity, note) => dispatch => {
    dispatch({
        type: ADD_PRODUCT,
        payload: product,
        quantity: quantity,
        note: note
    });
};

export const removeProduct = product => ({
    type: REMOVE_PRODUCT,
    payload: product
});

export const changeQuantity = (product, quantity) => dispatch => {
    dispatch({
        type: CHANGE_QUANTITY,
        payload: product,
        quantity: quantity
    });
};

export const changeFreight = (expeditionCode, shippingRate, totalWeight) => dispatch => {
    dispatch({
        type: CHANGE_FREIGHT,
        expeditionCode: expeditionCode,
        shippingRate: shippingRate,
        totalWeight: totalWeight
    });
};

export const changeNote = (note) => dispatch => {
    dispatch({
        type: CHANGE_NOTE,
        note: note
    });
};

export const changePaymentType = (paymentType) => dispatch => {
    dispatch({
        type: CHANGE_PAYMENT_TYPE,
        paymentType: paymentType
    });
};