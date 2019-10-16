import React, { Component } from 'react';
import './Grid.css';
import DialogProductDetail from './components/DialogProductDetail';
import DialogAddToCart from './components/DialogAddToCart';

export class GridItem extends Component {

    constructor(props) {
        super(props);
        this.showDialogProductDetail = this.showDialogProductDetail.bind(this);
        this.closeDialogProductDetail = this.closeDialogProductDetail.bind(this);
        this.addToCart = this.addToCart.bind(this);
        this.closeDialogAddToCart = this.closeDialogAddToCart.bind(this);

        this.state = {
            showDialogProductDetail: false,
            showDialogAddToCart: false
        }
    }

    showDialogProductDetail() {
        this.setState({ showDialogProductDetail: true });
    }

    closeDialogProductDetail() {
        this.setState({ showDialogProductDetail: false });
    }

    addToCart() {
        this.setState({
            showDialogProductDetail: false,
            showDialogAddToCart: true
        });
    }

    closeDialogAddToCart() {
        this.setState({
            showDialogAddToCart: false
        });
    }

    render() {
        return (
            <div className="product-image">
                <div className="product-image-container">
                    <img
                        decoding="auto"
                        srcSet={process.env.PUBLIC_URL + "/images/default-product.jpg"}
                        data-toggle="modal"
                        alt="abc"
                        onClick={this.showDialogProductDetail}
                    />
                </div>
                {
                    this.state.showDialogProductDetail &&
                    <DialogProductDetail
                        isOpen={this.state.showDialogProductDetail}
                        closeDialog={this.closeDialogProductDetail}
                        addToCart={this.addToCart}
                        product={this.props.product}
                    />
                }
                {
                    this.state.showDialogAddToCart &&
                    <DialogAddToCart
                        isOpen={this.state.showDialogAddToCart}
                        closeDialog={this.closeDialogAddToCart}
                        product={this.props.product}
                    />
                }
            </div>
        );
    }
}