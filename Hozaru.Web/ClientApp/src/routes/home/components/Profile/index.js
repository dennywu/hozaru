import React, { Component } from 'react';
import './StoreProfile.css';
import { API_URL } from "../../../../configuration";
import { connect } from 'react-redux';

class StoreProfile extends Component {
    render() {
        const { tenant } = this.props;
        var brandImageUrl = API_URL + "/api/tenants/brand/" + tenant.tenancyName;
        return (
            <div className="container">
                <div className="row">
                    <div className="col-4">
                        <div className="store-brand-logo rounded-circle margin-center" style={{ backgroundImage: `url(${brandImageUrl})` }}></div>
                    </div>
                    <div className="col-8">
                        <div className="row text-center margin-top-15px">
                            <div className="col-4">
                                <span className="font-weight-bold">{tenant.totalProduct}</span>
                                <div className="clearfix">Produk</div>
                            </div>
                            <div className="col-4">
                                <span className="font-weight-bold">{tenant.totalOrder}</span>
                                <div className="clearfix">Terjual</div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        );
    }
};

const mapStateToProps = state => ({
    tenant: state.tenant
});

export default connect(mapStateToProps, {})(StoreProfile);
