import { combineReducers } from 'redux';
import shoppingCartReducer from './shopping-cart/reducer';
import customerReducer from './customer/reducer';

export default combineReducers({
    shoppingCart: shoppingCartReducer,
    customer: customerReducer
});