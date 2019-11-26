import Alert from '../../app/partials/layout/Alert';

export function removeCSSClass(ele, cls) {
    const reg = new RegExp("(\\s|^)" + cls + "(\\s|$)");
    ele.className = ele.className.replace(reg, " ");
}

export function addCSSClass(ele, cls) {
    ele.classList.add(cls);
}

export const toAbsoluteUrl = pathname => process.env.PUBLIC_URL + pathname;

function errorResponseHandler(error) {
    // check for errorHandle config
    if (error.config.hasOwnProperty('errorHandle') && error.config.errorHandle === false) {
        return Promise.reject(error);
    }

    var defaultErrorMessage = 'Terjadi sesuatu yang tidak terduga sehingga tidak bisa menyelesaikan permintaan Anda. Kami mohon maaf.';
    // if has response show the error
    if (error.response) {
        var message = '';
        if (typeof error.response.data === "string") {
            message = error.response.data;
        } else {
            if (error.response.data.Message) {
                message = error.response.data.Message;
            } else if (typeof error.response.data.errors === "object") {
                message = error.response.data.errors[Object.keys(error.response.data.errors)[0]][0];
            } else {
                message = defaultErrorMessage;
            }
        }
        Alert.error(message);
    } else {
        Alert.error(error.message);
    }
    return Promise.reject(error);
}

export function setupAxios(axios, store) {

    axios.defaults.baseURL = "http://localhost:8989";
    axios.interceptors.request.use(
        config => {
            //const {
            //  auth: { authToken }
            //} = "C5BFF7F0-B4DF-475E-A331-F737424F013C";//store.getState();

            var apiKey = "C5BFF7F0-B4DF-475E-A331-F737424F013C";
            //if (authToken) {
            config.headers["X-Api-Key"] = apiKey;
            //}

            return config;
        },
        err => Promise.reject(err)
    );

    axios.interceptors.response.use(
        response => response,
        errorResponseHandler
    );
}


/*  removeStorage: removes a key from localStorage and its sibling expiracy key
    params:
        key <string>     : localStorage key to remove
    returns:
        <boolean> : telling if operation succeeded
 */
export function removeStorage(key) {
    try {
        localStorage.setItem(key, "");
        localStorage.setItem(key + "_expiresIn", "");
    } catch (e) {
        console.log(
            "removeStorage: Error removing key [" +
            key +
            "] from localStorage: " +
            JSON.stringify(e)
        );
        return false;
    }
    return true;
}

/*  getStorage: retrieves a key from localStorage previously set with setStorage().
    params:
        key <string> : localStorage key
    returns:
        <string> : value of localStorage key
        null : in case of expired key or failure
 */
export function getStorage(key) {
    const now = Date.now(); //epoch time, lets deal only with integer
    // set expiration for storage
    let expiresIn = localStorage.getItem(key + "_expiresIn");
    if (expiresIn === undefined || expiresIn === null) {
        expiresIn = 0;
    }

    expiresIn = Math.abs(expiresIn);
    if (expiresIn < now) {
        // Expired
        removeStorage(key);
        return null;
    } else {
        try {
            const value = localStorage.getItem(key);
            return value;
        } catch (e) {
            console.log(
                "getStorage: Error reading key [" +
                key +
                "] from localStorage: " +
                JSON.stringify(e)
            );
            return null;
        }
    }
}
/*  setStorage: writes a key into localStorage setting a expire time
    params:
        key <string>     : localStorage key
        value <string>   : localStorage value
        expires <number> : number of seconds from now to expire the key
    returns:
        <boolean> : telling if operation succeeded
 */
export function setStorage(key, value, expires) {
    if (expires === undefined || expires === null) {
        expires = 24 * 60 * 60; // default: seconds for 1 day
    }

    const now = Date.now(); //millisecs since epoch time, lets deal only with integer
    const schedule = now + expires * 1000;
    try {
        localStorage.setItem(key, value);
        localStorage.setItem(key + "_expiresIn", schedule);
    } catch (e) {
        console.log(
            "setStorage: Error setting key [" +
            key +
            "] in localStorage: " +
            JSON.stringify(e)
        );
        return false;
    }
    return true;
}
