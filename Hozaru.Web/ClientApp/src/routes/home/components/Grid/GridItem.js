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
            <div className="product-image cursor-pointer">
                <div className="product-image-container">
                    <img
                        decoding="auto"
                        srcSet={"/api/product/" + this.props.product.id + "/image"}
                        data-toggle="modal"
                        alt={this.props.product.name}
                        onClick={this.showDialogProductDetail}
                    />
                </div>
                {
                    this.state.showDialogProductDetail &&
                    <DialogProductDetail
                        isOpen={this.state.showDialogProductDetail}
                        toggle={this.closeDialogProductDetail}
                        addToCart={this.addToCart}
                        product={this.props.product}
                    />
                }
                {
                    this.state.showDialogAddToCart &&
                    <DialogAddToCart
                        isOpen={this.state.showDialogAddToCart}
                        toggle={this.closeDialogAddToCart}
                        product={this.props.product}
                    />
                }
            </div>
        );
    }
}