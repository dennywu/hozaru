import { combineReducers } from 'redux';
import shoppingCartReducer from './shopping-cart/reducer';

export default combineReducers({
    shoppingCart: shoppingCartReducer
});