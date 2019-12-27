import 'sweetalert2/dist/sweetalert2.css'
import swal from 'sweetalert2';

const alert = {
    error: (message, title = 'Oops...') => {
        swal.fire(title, message, 'error');
    },
    success: (message, callback, title = 'Yea...') => {
        swal.fire(title, message, 'success').then(result => {
            if (callback)
                callback(result);
        });
    }
};

export default alert;