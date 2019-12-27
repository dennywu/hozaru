import { STORE_API_KEY } from './actionTypes';

export const storeApiKey = (apiKey) => dispatch => {
    dispatch({
        type: STORE_API_KEY,
        payload: apiKey
    });
};