import React, { Component } from 'react';
import StoreProfile from './components/Profile';
import { Grid } from './components/Grid';
import FloatCart from './components/FloatCart';

export default class Home extends Component {
    constructor() {
        super();
    }

    render() {
        return (
            <div className="mt-2">
                <StoreProfile />
                <hr className="mt-2 mb-2" />
                <Grid />
                <FloatCart />
            </div>
        );
    }
}