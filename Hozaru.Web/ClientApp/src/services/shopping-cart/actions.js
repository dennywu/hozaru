import { LOAD_CART, ADD_PRODUCT, REMOVE_PRODUCT, CHANGE_QUANTITY, CHANGE_FREIGHT, CHANGE_NOTE, CHANGE_PAYMENT_METHOD, RESET_SHOPPING_CART, UPDATE_PRODUCT } from './actionTypes';

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

export const updateProduct = (product) => dispatch => {
    dispatch({
        type: UPDATE_PRODUCT,
        payload: product
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

export const changeFreight = (expeditionServiceId, shippingRate, totalWeight) => dispatch => {
    dispatch({
        type: CHANGE_FREIGHT,
        expeditionServiceId: expeditionServiceId,
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

export const changePaymentMethod = (paymentMethod) => dispatch => {
    dispatch({
        type: CHANGE_PAYMENT_METHOD,
        paymentMethod: paymentMethod
    });
};

export const resetShoppingCart = () => dispatch => {
    dispatch({
        type: RESET_SHOPPING_CART
    });
};