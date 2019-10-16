import React, { Component } from 'react';
import './StoreProfile.css';

export class StoreProfile extends Component {

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
                                <span className="font-weight-bold">19</span>
                                <div className="clearfix">Produk</div>
                            </div>
                            <div className="col-4">
                                <span className="font-weight-bold">399</span>
                                <div className="clearfix">Terjual</div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        );
    }
}