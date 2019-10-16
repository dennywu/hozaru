import React, { Component } from 'react';
import { Modal, ModalBody, ModalFooter } from 'reactstrap';
import PropTypes from 'prop-types';
import { ModalClose } from '../../../../../../components/modal/modal-close';
import { default as NumberFormat } from 'react-number-format';

export default class DialogProductDetail extends Component {

    constructor(props) {
        super(props);

        this.state = {
            isOpen: props.isOpen
        };

        this.toggle = this.toggle.bind(this);
    }

    static propTypes = {
        isOpen: PropTypes.bool.isRequired,
        product: PropTypes.object.isRequired,
        closeDialog: PropTypes.func.isRequired
    };

    toggle() {
        this.setState(prevState => ({
            isOpen: !prevState.isOpen
        }),
            function () {
                this.props.closeDialog();
            }
        );
    }

    render() {
        return (
            <Modal isOpen={this.state.isOpen} fade={true} centered={true} toogle={this.toogle} backdrop={true} className="modal-addtocart modal-without-header">
                <ModalClose toggle={this.toggle} />
                <ModalBody>
                    <img className="image-product-addtocart" srcSet={process.env.PUBLIC_URL + "/images/default-product.jpg"} alt="" />
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