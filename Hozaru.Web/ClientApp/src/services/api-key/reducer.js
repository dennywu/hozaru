import { STORE_API_KEY } from './actionTypes';

const initialState = {
    tenancyName: '',
    apiKey: ''
};

export default function (state = initialState, action) {
    switch (action.type) {
        case STORE_API_KEY:
            return Object.assign({}, action.payload);

        default:
            return state;
    }
};