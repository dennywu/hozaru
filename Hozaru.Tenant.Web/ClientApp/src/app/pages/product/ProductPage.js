import React, { Component, Suspense } from 'react';
import { Switch, Route, Redirect } from 'react-router-dom';
import { LayoutSplashScreen } from '../../../_metronic';
import ListProduct from './list-product';
import CreateProduct from './create-product';
import EditProduct from "./edit-product";

class ProductPage extends Component {
    render() {
        return (
            <Suspense fallback={<LayoutSplashScreen />}>
                <Switch>
                    <Route exact path="/products" component={ListProduct} />
                    <Route path="/products/new" component={CreateProduct} />
                    <Route path="/products/:id/edit" component={EditProduct} />
                </Switch>
            </Suspense>
            );
    }
}

export default ProductPage;