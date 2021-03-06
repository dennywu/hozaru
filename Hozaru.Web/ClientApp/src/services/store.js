﻿import { compose, createStore, applyMiddleware } from 'redux';
import thunk from 'redux-thunk';
import rootReducer from './reducers';

export default initialState => {
    initialState = JSON.parse(window.localStorage.getItem('hozaru:state')) || initialState;
    const middleware = [thunk];

    const store = createStore(
        rootReducer,
        initialState,
        compose(
            applyMiddleware(...middleware)
            /* window.__REDUX_DEVTOOLS_EXTENSION__ &&
              window.__REDUX_DEVTOOLS_EXTENSION__() */
        )
    );

    store.subscribe(() => {
        const state = store.getState();
        const persist = {
            shoppingCart: state.shoppingCart,
            customer: state.customer,
            tenant: state.tenant,
            apiKey: state.apiKey
        };

        window.localStorage.setItem('hozaru:state', JSON.stringify(persist));
    });

    return store;
};