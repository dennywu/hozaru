import { CHANGE_TENANT_INFO } from './actionTypes';

const initialState = {
    name: '',
    totalProduct: '',
    totalOrder: '',
    whatsapp: '',
    whatsappUrl: ''
};

export default function (state = initialState, action) {
    switch (action.type) {
        case CHANGE_TENANT_INFO:
            return Object.assign({}, action.payload);

        default:
            return state;
    }
};