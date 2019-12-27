import React, { Component, Suspense } from 'react';
import { Switch, Route, Redirect } from 'react-router-dom';
import { LayoutSplashScreen } from '../../../_metronic';
import ListPaymentMethod from './list-payment-method';
import CreatePaymentMethod from './create-payment-method';
import EditPaymentMethod from './edit-payment-method';

class PaymentMethodPage extends Component {
    render() {
        return (
            <Suspense fallback={<LayoutSplashScreen />}>
                <Switch>
                    <Route exact path="/paymentmethods" component={ListPaymentMethod} />
                    <Route path="/paymentmethods/new" component={CreatePaymentMethod} />
                    <Route path="/paymentmethods/:id/edit" component={EditPaymentMethod} />
                </Switch>
            </Suspense>
        );
    }
}

export default PaymentMethodPage;