import React, { Component, Suspense } from 'react';
import { Switch, Route, Redirect } from 'react-router-dom';
import { LayoutSplashScreen } from '../../../_metronic';
import ListOrder from './list';
import DetailOrder from "./detail";

class ProductPage extends Component {
    render() {
        return (
            <Suspense fallback={<LayoutSplashScreen />}>
                <Switch>
                    <Route exact path="/orders" component={ListOrder} />
                    <Route exact path="/orders/:id/detail" component={DetailOrder} />
                </Switch>
            </Suspense>
        );
    }
}

export default ProductPage;