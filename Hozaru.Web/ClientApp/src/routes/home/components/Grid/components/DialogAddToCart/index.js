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
        closeDialog: PropTypes.func.isRequired,
        addProduct: PropTypes.func.isRequired
    };

    constructor(props) {
        super(props);

        this.state = {
            isOpen: props.isOpen,
            product: props.product,
            quantity: 1,
            note: ''
        };

        this.toggle = this.toggle.bind(this);
        this.changeQuantity = this.changeQuantity.bind(this);
        this.changeNote = this.changeNote.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
    }

    toggle() {
        this.setState(prevState => ({
            isOpen: !prevState.isOpen
        }),
            function () {
                this.props.closeDialog();
            }
        );
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
        this.toggle();
    }

    render() {
        return (
            <Modal isOpen={this.state.isOpen} fade={false} toogle={this.toogle} backdrop="static" className="modal-checkout modal-without-header fixed-bottom">
                <ModalClose toggle={this.toggle} />
                <form onSubmit={this.handleSubmit}>
                    <ModalBody>
                        <div className="container">
                            <div className="row">
                                <div className="col-3">
                                    <img className="image-product-addtocart" srcSet={process.env.PUBLIC_URL + "/images/default-product.jpg"} alt="" />
                                </div>
                                <div className="col-9">
                                    <div className="font-weight-600">{this.props.product.name}</div>
                                    <div className="font-weight-bolder">
                                        <NumberFormat value={this.props.product.price} displayType={'text'} thousandSeparator={true} prefix={'Rp '} />
                                    </div>
                                </div>
                            </div>
                            <div className="row pt-3">
                                <div className="col-12">
                                    <textarea className="form-control" rows="3" placeholder="Cacatan: Warna / Ukuran (Optional)" defaultValue={this.state.note} onBlur={this.changeNote}>
                                    </textarea>
                                </div>
                            </div>
                            <div className="row pt-3">
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