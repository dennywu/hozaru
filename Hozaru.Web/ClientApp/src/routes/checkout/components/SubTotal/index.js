import React, { Component } from 'react';
import { connect } from 'react-redux';
import PropTypes from 'prop-types';
import { default as NumberFormat } from 'react-number-format';

class SubTotal extends Component {
    static propTypes = {
        shoppingCartSummary: PropTypes.object.isRequired
    };

    render() {
        const { shoppingCartSummary } = this.props;
        return (
            <div className="container mt-2 mb-2">
                <div className="row">
                    <div className="col-7">
                        <span className="font-weight-normal">
                            Total Pesanan
                            (<b>{shoppingCartSummary.totalQuantity}</b> Produk):
                         </span>
                    </div>
                    <div className="col-5 text-right">
                        <span className="font-weight-bolder">
                            <NumberFormat value={shoppingCartSummary.subTotal} displayType={'text'} thousandSeparator={true} prefix={'Rp '} />
                        </span>
                    </div>
                </div>
            </div>
        );
    }
}


const mapStateToProps = state => ({
    shoppingCartSummary: state.shoppingCart.summary
});

export default connect(mapStateToProps, {})(SubTotal);
