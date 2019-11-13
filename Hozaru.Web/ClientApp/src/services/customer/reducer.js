import { CHANGE_CUSTOMER_INFO, VALIDATE } from './actionTypes';

const initialState = {
    name: '',
    whatsapp: '',
    email: '',
    address: '',
    city: null,
    districts: null,
    errors: {
        name: {
            hasError: false,
            message: ''
        },
        whatsapp: {
            hasError: false,
            message: ''
        },
        email: {
            hasError: false,
            message: ''
        },
        address: {
            hasError: false,
            message: ''
        },
        city: {
            hasError: false,
            message: ''
        },
        districts: {
            hasError: false,
            message: ''
        },
        hasError: false
    }
};

const validate = (state) => {
    if (!state.customerToChange) {
        return;
    }

    if (state.customerToChange.name !== state.name || state.name === '')
        state.errors.name = (state.name !== '') ? { hasError: false, message: '' } : { hasError: true, message: 'Masukan Nama Anda' };

    if (state.customerToChange.whatsapp !== state.whatsapp || state.whatsapp === '')
        state.errors.whatsapp = (state.whatsapp !== '') ? { hasError: false, message: '' } : { hasError: true, message: 'Masukan No. Whatsapp Anda' };

    if (state.customerToChange.email !== state.email || state.email === '')
        state.errors.email = (state.email !== '') ? { hasError: false, message: '' } : { hasError: true, message: 'Masukan Email Anda' };

    if (state.customerToChange.address !== state.address || state.address === '')
        state.errors.address = (state.address !== '') ? { hasError: false, message: '' } : { hasError: true, message: 'Masukan Alamat Anda' };

    if (state.customerToChange.city !== state.city || state.city === null)
        state.errors.city = (state.city !== null) ? { hasError: false, message: '' } : { hasError: true, message: 'Pilih Kota Anda' };

    if (state.customerToChange.districts !== state.districts || state.districts === null)
        state.errors.districts = (state.districts !== null) ? { hasError: false, message: '' } : { hasError: true, message: 'Pilih Kecamatan Anda' };

    state.errors.hasError = (state.errors.name.hasError || state.errors.whatsapp.hasError || state.errors.email.hasError || state.errors.address.hasError ||
        state.errors.city.hasError || state.errors.districts.hasError);
};

export default function (state = initialState, action) {
    switch (action.type) {
        case CHANGE_CUSTOMER_INFO:
            validate(state);
            return {
                ...state,
                customerToChange: Object.assign({}, action.payload)
            };
        case VALIDATE:
            validate(state);
            return {
                ...state
            };
        default:
            return state;
    }
};