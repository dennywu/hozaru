import React, { Component } from 'react';
import { connect } from 'react-redux';
import PropTypes from 'prop-types';
import { default as NumberFormat } from 'react-number-format';

class Summary extends Component {
    static propTypes = {
        shoppingCartSummary: PropTypes.object.isRequired
    };

    render() {
        const { shoppingCartSummary, freight } = this.props;
        return (
            <div className="container mt-3 pb-2 ">
                <div className="row font-13px">
                    <div className="col-7">
                        <span className="font-weight-normal">Subtotal untuk Produk</span>
                    </div>
                    <div className="col-5 text-right">
                        <span className="font-weight-normal">
                            <NumberFormat value={shoppingCartSummary.subTotal} displayType={'text'} thousandSeparator={true} prefix={'Rp '} />
                        </span>
                    </div>
                </div>
                <div className="row font-13px">
                    <div className="col-7">
                        <span className="font-weight-normal">Subtotal Pengiriman</span>
                    </div>
                    <div className="col-5 text-right">
                        <span className="font-weight-normal">
                            <NumberFormat value={freight.rate} displayType={'text'} thousandSeparator={true} prefix={'Rp '} />
                        </span>
                    </div>
                </div>
                {
                    shoppingCartSummary.voucher &&
                    <div className="row font-13px">
                        <div className="col-7">
                            <span className="font-weight-normal">Voucher</span>
                        </div>
                        <div className="col-5 text-right">
                            <span className="font-weight-normal">Rp -5.000</span>
                        </div>
                    </div>
                }
                <div className="row font-16px">
                    <div className="col-7">
                        <span className="font-weight-bold">Total Pembayaran</span>
                    </div>
                    <div className="col-5 text-right">
                        <span className="font-weight-bolder">
                            <NumberFormat value={shoppingCartSummary.totalSummary} displayType={'text'} thousandSeparator={true} prefix={'Rp '} />
                        </span>
                    </div>
                </div>
            </div>
        );
    }
}


const mapStateToProps = state => ({
    freight: state.shoppingCart.freight,
    shoppingCartSummary: state.shoppingCart.summary
});

export default connect(mapStateToProps, {})(Summary);
