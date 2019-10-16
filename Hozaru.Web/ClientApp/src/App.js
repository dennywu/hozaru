import React, { Component } from 'react';
import { Route, Switch } from 'react-router';
import { Layout } from './Layout';
import { Provider } from 'react-redux';
import { Home } from './routes/home';
import Checkout from './routes/checkout';
import store from './services/store';
import './custom.css';

export default class App extends Component {
    static displayName = App.name;

    render() {
        return (
            <Provider store={store({})}>
                <Layout>
                    <Switch>
                        <Route exact path='/' component={Home} />
                        <Route path='/checkout' component={Checkout} abc={"abc"} />
                    </Switch>
                </Layout>
            </Provider>
        );
    }
}
