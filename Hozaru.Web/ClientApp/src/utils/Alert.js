import 'sweetalert2/dist/sweetalert2.css'
import swal from 'sweetalert2';

const alert = {
    error: (message, title = 'Oops...') => {
        swal.fire(title, message, 'error');
    },
    success: (message, title = 'Yea...') => {
        swal.fire(title, message, 'success');
    }
};

export default alert;