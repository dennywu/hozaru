import { combineReducers } from 'redux';
import shoppingCartReducer from './shopping-cart/reducer';
import customerReducer from './customer/reducer';
import tenantReducer from './tenant/reducer';
import apiKeyReducer from './api-key/reducer';

export default combineReducers({
    shoppingCart: shoppingCartReducer,
    customer: customerReducer,
    tenant: tenantReducer,
    apiKey: apiKeyReducer
});