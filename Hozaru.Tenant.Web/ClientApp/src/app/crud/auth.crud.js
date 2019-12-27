import axios from "axios";

export const LOGIN_URL = "api/auth";
export const REFRESH_TOKEN = "api/refreshtoken";
export const REGISTER_URL = "api/auth/register";
export const REQUEST_PASSWORD_URL = "api/auth/forgot-password";

export const ME_URL = "api/me";

export function login(userNameOrEmailAddress, password, tenancyName) {
    return axios.post(LOGIN_URL, { userNameOrEmailAddress, password, tenancyName });
}

export function register(email, fullname, username, password) {
  return axios.post(REGISTER_URL, { email, fullname, username, password });
}

export function requestPassword(email) {
  return axios.post(REQUEST_PASSWORD_URL, { email });
}

export function getUserByToken() {
  // Authorization head should be fulfilled in interceptor.
  return axios.get(ME_URL);
}

export function refreshToken(accessToken, refreshToken) {
    return axios.post(REFRESH_TOKEN, { accessToken, refreshToken });
}