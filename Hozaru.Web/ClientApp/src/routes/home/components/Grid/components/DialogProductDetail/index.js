import React, { Component } from 'react';
import { Modal, ModalBody, ModalFooter } from 'reactstrap';
import PropTypes from 'prop-types';
import { ModalClose } from '../../../../../../components/modal/modal-close';
import { default as NumberFormat } from 'react-number-format';
import { Carousel } from 'react-responsive-carousel';
import "react-responsive-carousel/lib/styles/carousel.min.css";
import "./style.css";
import ShowMoreText from 'react-show-more-text';

export default class DialogProductDetail extends Component {
    static propTypes = {
        isOpen: PropTypes.bool.isRequired,
        product: PropTypes.object.isRequired
    };

    render() {
        const { product } = this.props;

        var carousel = [];
        for (var i = 0; i < product.images.length; i++) {
            var item = product.images[i];
            carousel.push(
                <div key={item.id}>
                    <img className="image-product-addtocart" srcSet={item.url} alt={this.props.product.name} />
                </div>
            );
        }

        return (
            <Modal isOpen={this.props.isOpen} fade={true} centered={true} toggle={this.props.toggle} backdrop={true} className="modal-addtocart modal-without-header modal-dialog-scrollable">
                <ModalClose toggle={this.props.toggle} />
                <ModalBody>
                    <Carousel showThumbs={false} showArrows={false} statusFormatter={(current, total) => { return current + '/' + total }}>
                        {carousel}
                    </Carousel>

                    <div className="product-description">
                        <div className="font-weight-600">{product.name}</div>
                        <div className="font-weight-bolder">
                            <NumberFormat value={product.price} displayType={'text'} thousandSeparator={true} prefix={'Rp '} />
                        </div>
                        <div>
                            <ShowMoreText
                                lines={2}
                                more='more'
                                less='less'
                                expanded={false}
                                className="product-description"
                            >
                                {product.description}
                            </ShowMoreText>
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