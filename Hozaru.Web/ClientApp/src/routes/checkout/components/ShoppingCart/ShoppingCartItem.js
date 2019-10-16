import React, { Component } from 'react';
import PropTypes from 'prop-types'
import { default as NumberFormat } from 'react-number-format';
import { default as NumericInput } from 'react-numeric-input';
import { confirmAlert } from 'react-confirm-alert';
import 'react-confirm-alert/src/react-confirm-alert.css' // Import css
import './style.css';

class ShoppingCartItem extends Component {

    constructor(props) {
        super(props);
        this.changeQuantity = this.changeQuantity.bind(this);
        this.removeShoppingCartItem = this.removeShoppingCartItem.bind(this);
        this.handleClickDelete = this.handleClickDelete.bind(this);

        this.state = {
            quantity: this.props.quantity
        };
    }

    static propTypes = {
        product: PropTypes.object.isRequired
    };

    changeQuantity(quantity) {
        if (quantity === null) {
            return;
        }

        if (quantity === 0) {
            this.removeShoppingCartItem();
        }
        else {
            this.setState({ quantity: quantity });
            this.props.changeQuantity(this.props.product, quantity);
        }
    }

    removeShoppingCartItem() {
        confirmAlert({
            customUI: ({ onClose }) => {
                return (
                    <div className='container'>
                        <div className='custom-ui'>
                            <div className='row'>
                                <div className='col-12'>
                                    <p className='text-center'>Yakin untuk menghapus produk ini dari keranjang?</p>
                                </div>
                            </div>
                            <div className='row'>
                                <div className='col-6'>
                                    <button className='form-control' onClick={() => {
                                        this.setState({ quantity: this.state.quantity });
                                        onClose();
                                    }}>Nanti Saja</button>
                                </div>
                                <div className='col-6'>
                                    <button className='form-control btn-danger' onClick={() => {
                                        this.handleClickDelete()
                                        onClose()
                                    }}>Hapus</button>
                                </div>
                            </div>
                        </div>
                    </div>
                )
            }
        })
    }

    handleClickDelete() {
        this.props.removeProduct(this.props.product);
        this.props.removeComponent();
    }

    render() {
        return (
            <div className="row">
                <div className="col-3 image-product">
                    <img className="w100" srcSet={process.env.PUBLIC_URL + "/images/default-product.jpg"} alt={this.props.product.name} />
                </div>
                <div className="col-9">
                    <div className="font-weight-600 text-truncate">{this.props.product.name}</div>
                    <div className="row">
                        <div className="col-12 numeric-input narrow small pt-1">
                            <NumericInput className="form-control narrow" min={0} max={1000} value={this.state.quantity} mobile={true} onChange={this.changeQuantity} />
                        </div>
                        <div className="col-8 font-weight-bolder pt-1">
                            <NumberFormat value={this.props.product.price} displayType={'text'} thousandSeparator={true} prefix={'Rp '} />
                        </div>
                    </div>
                </div>
            </div>
        );
    }
}

export default ShoppingCartItem;