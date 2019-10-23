﻿import { LOAD_CART, ADD_PRODUCT, REMOVE_PRODUCT, CHANGE_QUANTITY, CHANGE_FREIGHT, CHANGE_NOTE, CHANGE_PAYMENT_TYPE } from './actionTypes';

const initialState = {
    items: [],
    note: '',
    paymentType: '',
    freight: {
        expeditionCode: '',
        rate: '',
        totalWeight: ''
    },
    summary: {
        totalQuantity: 0,
        subTotal: 0,
        shippingRate: 0,
        expeditionCode: '',
        voucher: 0,
        totalSummary: 0
    }
};

const calculateSummary = (shoppingCart) => {
    let productQuantity = shoppingCart.items.reduce((sum, p) => {
        sum += p.quantity;
        return sum;
    }, 0);

    let subTotal = shoppingCart.items.reduce((sum, p) => {
        sum += p.total;
        return sum;
    }, 0);

    let totalSummary = subTotal + shoppingCart.freight.rate;

    return {
        totalQuantity: productQuantity,
        subTotal: subTotal,
        totalSummary: totalSummary
    };
};

export default function (state = initialState, action) {
    switch (action.type) {
        case LOAD_CART:
            return {
                ...state,
                products: action.payload
            };
        case ADD_PRODUCT:
            let shoppingCart = state;
            let product = action.payload;
            let note = action.note;
            let quantity = action.quantity;

            let productAlreadyInCart = false;

            let shoppingCartItem = {
                product: product,
                quantity: quantity,
                note: note,
                total: product.price * quantity
            };

            shoppingCart.items.forEach(item => {
                if (item.product.id === product.id) {
                    item.quantity += quantity;
                    item.note = note;
                    item.total = product.price * item.quantity;
                    productAlreadyInCart = true;
                }
            });
            if (!productAlreadyInCart)
                shoppingCart.items.push(shoppingCartItem);

            shoppingCart.summary = calculateSummary(state);

            return {
                ...state,
                productToAdd: Object.assign({}, action.payload)
            };
        case REMOVE_PRODUCT:
            let shoppingCartRemoveProduct = state;
            let productRemoveProduct = action.payload;

            const index = shoppingCartRemoveProduct.items.findIndex(item => item.product.id === productRemoveProduct.id);
            if (index >= 0) {
                shoppingCartRemoveProduct.items.splice(index, 1);
            }

            shoppingCartRemoveProduct.summary = calculateSummary(state);

            return {
                ...state,
                productToRemove: Object.assign({}, action.payload)
            };
        case CHANGE_QUANTITY:
            let shoppingCartChangeQty = state;
            let productChangeQty = action.payload;
            let quantityChangeQty = action.quantity;

            shoppingCartChangeQty.items.forEach(item => {
                if (item.product.id === productChangeQty.id) {
                    item.quantity = quantityChangeQty;
                    item.total = productChangeQty.price * item.quantity;
                }
            });

            shoppingCartChangeQty.summary = calculateSummary(state);

            return {
                ...state,
                quantityToChange: Object.assign({}, { productId: productChangeQty.id, quantity: quantityChangeQty })
            };
        case CHANGE_FREIGHT:
            state.freight = { expeditionCode: action.expeditionCode, rate: action.shippingRate, totalWeight: action.totalWeight };
            state.summary = calculateSummary(state);

            return {
                ...state
            };

        case CHANGE_NOTE:
            state.note = action.note;

            return {
                ...state
            };

        case CHANGE_PAYMENT_TYPE:
            state.paymentType = action.paymentType;
            return {
                ...state
            };
        default:
            return state;
    }
};