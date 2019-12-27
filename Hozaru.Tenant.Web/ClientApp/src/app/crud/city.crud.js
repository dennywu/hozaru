import axios from 'axios';

export const API_CITY_URL = "api/city";

export function getCities(searchKey) {
    return axios.get(API_CITY_URL + "?searchKey=" + searchKey);
}