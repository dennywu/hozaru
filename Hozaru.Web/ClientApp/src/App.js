import React, { Component } from 'react';
import { Route, Switch, withRouter } from 'react-router';
import Layout from './Layout';
import { Provider } from 'react-redux';
import Home  from './routes/home';
import Checkout from './routes/checkout';
import Payment from './routes/payment';
import PaymentConfirmation from './routes/payment-confirmation';
import Order from './routes/order';
import OrderShipmentTracking from './routes/order/shipment-tracking';
import store from './services/store';
import './custom.css';
import './services/error-handler';
import './services/axios-interceptor';

export default class App extends Component {
    static displayName = App.name;
    render() {
        return (
            <Provider store={store({})}>
                <Layout>
                    <Switch>
                        <Route exact path='/' component={withRouter(Home)} />
                        <Route path='/checkout' component={withRouter(Checkout)} />
                        <Route path='/payment/:id' component={withRouter(Payment)} />
                        <Route path='/payment-confirmation/:id' component={withRouter(PaymentConfirmation)} />
                        <Route exact path='/order/:id' component={withRouter(Order)} />
                        <Route path='/order/:id/tracking' component={withRouter(OrderShipmentTracking)} />
                    </Switch>
                </Layout>
            </Provider>
        );
    }
}
