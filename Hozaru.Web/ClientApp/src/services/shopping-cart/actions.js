import { LOAD_CART, ADD_PRODUCT, REMOVE_PRODUCT, CHANGE_QUANTITY } from './actionTypes';

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