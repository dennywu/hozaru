import React, { Component } from 'react';
import { Modal, ModalBody, ModalFooter } from 'reactstrap';
import { ModalClose } from '../../../../../../components/modal/modal-close';
import { default as NumberFormat } from 'react-number-format';
import { default as NumericInput } from 'react-numeric-input';
import PropTypes from 'prop-types';
import { connect } from 'react-redux';
import { addProduct } from '../../../../../../services/shopping-cart/actions';

class DialogAddToCart extends Component {
    static propTypes = {
        isOpen: PropTypes.bool.isRequired,
        product: PropTypes.object.isRequired,
        addProduct: PropTypes.func.isRequired
    };

    constructor(props) {
        super(props);

        this.state = {
            product: props.product,
            quantity: 1,
            note: ''
        };

        this.changeQuantity = this.changeQuantity.bind(this);
        this.changeNote = this.changeNote.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
    }

    changeQuantity(quantity) {
        this.setState({ quantity: quantity });
    }

    changeNote(event) {
        this.setState({ note: event.target.value });
    }

    handleSubmit(event) {
        event.preventDefault();
        const { product } = this.state;
        this.props.addProduct(product, this.state.quantity, this.state.note);
        this.props.toggle();
    }

    render() {
        return (
            <Modal isOpen={this.props.isOpen} fade={false} toggle={this.props.toggle} backdrop={true} className="modal-checkout modal-without-header fixed-bottom">
                <ModalClose toggle={this.props.toggle} />
                <form onSubmit={this.handleSubmit}>
                    <ModalBody>
                        <div className="container">
                            <div className="row">
                                <div className="col-3">
                                    <img className="image-product-addtocart" srcSet={this.props.product.firstProductImage.url} alt={this.props.product.name} />
                                </div>
                                <div className="col-9">
                                    <div className="font-weight-600">{this.props.product.name}</div>
                                    <div className="font-weight-bolder">
                                        <NumberFormat value={this.props.product.price} displayType={'text'} thousandSeparator={true} prefix={'Rp '} />
                                    </div>
                                </div>
                            </div>
                            <div className="row mt-3">
                                <div className="col-12 numeric-input center narrow">
                                    <NumericInput className="form-control" min={1} max={1000} value={this.state.quantity} mobile={true} onChange={this.changeQuantity} />
                                </div>
                            </div>
                        </div>
                    </ModalBody>
                    <ModalFooter>
                        <button type="submit" className="btn btn-primary checkout">Tambah ke Keranjang</button>
                    </ModalFooter>
                </form >
            </Modal>
        );
    }
}


const mapStateToProps = state => ({
});

export default connect(mapStateToProps, { addProduct })(DialogAddToCart);