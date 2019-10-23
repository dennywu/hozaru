import React, { Component } from 'react';
import { Modal, ModalBody, ModalFooter } from 'reactstrap';
import PropTypes from 'prop-types';
import { ModalClose } from '../../../../../../components/modal/modal-close';
import { default as NumberFormat } from 'react-number-format';

export default class DialogProductDetail extends Component {
    static propTypes = {
        isOpen: PropTypes.bool.isRequired,
        product: PropTypes.object.isRequired
    };

    render() {
        return (
            <Modal isOpen={this.props.isOpen} fade={true} centered={true} toggle={this.props.toggle} backdrop={true} className="modal-addtocart modal-without-header">
                <ModalClose toggle={this.props.toggle} />
                <ModalBody>
                    <img className="image-product-addtocart" srcSet={"/api/product/" + this.props.product.id + "/image"} alt={this.props.product.name} />
                    <div className="product-description">
                        <div className="font-weight-600">{this.props.product.name}</div>
                        <div className="font-weight-bolder">
                            <NumberFormat value={this.props.product.price} displayType={'text'} thousandSeparator={true} prefix={'Rp '} />
                        </div>
                    </div>
                </ModalBody>
                <ModalFooter>
                    <button type="button" className="btn btn-primary addtocart" onClick={this.props.addToCart}>Beli Sekarang</button>
                </ModalFooter>
            </Modal>
        );
    }
}