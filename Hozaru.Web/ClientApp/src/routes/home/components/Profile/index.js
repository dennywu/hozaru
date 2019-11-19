import React, { Component } from 'react';
import './StoreProfile.css';
import axios from 'axios';

export class StoreProfile extends Component {
    constructor() {
        super();
        this.populateTenantInformation = this.populateTenantInformation.bind(this);

        this.state = {
            totalOrder: 0,
            totalProduct: 0
        }
    }

    componentDidMount() {
        this.populateTenantInformation();
    }

    async populateTenantInformation() {
        axios.get('/api/tenant')
            .then(res => {
                const data = res.data;
                this.setState({
                    totalOrder: data.totalOrder,
                    totalProduct: data.totalProduct
                });
            });
    }

    render() {
        return (
            <div className="container">
                <div className="row">
                    <div className="col-4">
                        <div className="store-brand-logo rounded-circle margin-center"></div>
                    </div>
                    <div className="col-8">
                        <div className="row text-center margin-top-15px">
                            <div className="col-4">
                                <span className="font-weight-bold">{this.state.totalProduct}</span>
                                <div className="clearfix">Produk</div>
                            </div>
                            <div className="col-4">
                                <span className="font-weight-bold">{this.state.totalOrder}</span>
                                <div className="clearfix">Terjual</div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        );
    }
}