import { CHANGE_CUSTOMER_INFO } from './actionTypes';

const initialState = {
    name: '',
    whatsapp: '',
    email: '',
    address: '',
    city: null,
    districts: null
};

export default function (state = initialState, action) {
    switch (action.type) {
        case CHANGE_CUSTOMER_INFO:
            return {
                ...state,
                customerToChange: Object.assign({}, action.payload)
            };
        default:
            return state;
    }
};