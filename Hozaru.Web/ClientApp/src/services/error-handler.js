import axios from 'axios'
import Alert from '../utils/Alert'

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
            if (error.response.data.Message === undefined) {
                message = defaultErrorMessage;
            } else {
                message = error.response.data.Message;
            }
        }
        Alert.error(message);
    } else {
        Alert.error(error.message);
    }
    return Promise.reject(error);
}

// apply interceptor on response
axios.interceptors.response.use(
    response => response,
    errorResponseHandler
);

export default errorResponseHandler;