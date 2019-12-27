import axios from 'axios';

export const API_DISTRICT_URL = "api/district";

export function getDistrictses(cityId, searchKey) {
    return axios.get(API_DISTRICT_URL + "?cityId=" + cityId + "&searchKey=" + searchKey);
}