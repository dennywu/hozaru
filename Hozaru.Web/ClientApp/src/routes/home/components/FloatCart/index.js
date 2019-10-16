import React, { Component } from 'react';
import PropTypes from 'prop-types';
import { connect } from 'react-redux';
import { default as NumberFormat } from 'react-number-format';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faCheckCircle } from '@fortawesome/free-solid-svg-icons';
import { Link } from 'react-router-dom';
import { loadCart } from '../../../../services/shopping-cart/actions';

class FloatCart extends Component {

    static propTypes = {
        loadCart: PropTypes.func.isRequired
    };

    redirectToCheckoutPage() {
        this.props.history.push('/checkout');
    }

    render() {
        const { shoppingCartSummary } = this.props;
        let contents = "";
        if (shoppingCartSummary.totalQuantity !== 0) {
            contents = <div className="sectionaddtocart fixed-bottom border-top">
                <div className="row p-2 pl-4 pr-4">
                    <div className="col-6">
                        <FontAwesomeIcon icon={faCheckCircle} className="text-success" />
                        <span className="font-weight-600">
                            <b> {shoppingCartSummary.totalQuantity} </b> produk terpilih</span>
                    </div>
                    <div className="col-6 text-right">
                        <span className="">Total </span>
                        <span className="font-weight-bolder">
                            <NumberFormat value={shoppingCartSummary.totalSummary} displayType={'text'} thousandSeparator={true} prefix={'Rp '} />
                        </span>
                    </div>
                </div >
                <div className="row">
                    <div className="col-12">
                        <Link to="/checkout">
                            <button type="button" className="btn btn-primary button-full">Checkout</button>
                        </Link>
                    </div>
                </div>
            </div >
        }
        return (
            <div>
                {contents}
            </div>
        );
    };
};

const mapStateToProps = state => ({
    shoppingCartItems: state.shoppingCart.items,
    shoppingCartSummary: state.shoppingCart.summary,
    newProduct: state.shoppingCart.productToAdd
});

export default connect(mapStateToProps, { loadCart })(FloatCart);