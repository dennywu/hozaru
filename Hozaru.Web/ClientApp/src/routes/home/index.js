import React, { Component } from 'react';
import { StoreProfile } from './components/Profile';
import { Grid } from './components/Grid';
import FloatCart from './components/FloatCart';

export class Home extends Component {
    static displayName = Home.name;

    render() {
        return (
            <div>
                <StoreProfile />
                <hr />
                <Grid />
                <FloatCart />
            </div>
        );
    }
}
