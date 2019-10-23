import React, { Component } from 'react';
import { connect } from 'react-redux';
import ShoppingCartItem from './ShoppingCartItem';
import { changeQuantity, removeProduct } from '../../../../services/shopping-cart/actions';

class ShoppingCart extends Component {

    constructor() {
        super();
        this.removeShoppingCartComponent = this.removeShoppingCartComponent.bind(this);
    }

    removeShoppingCartComponent() {
        this.forceUpdate();
    }

    render() {
        const { shoppingCartItems } = this.props;
        var contents = [];
        for (var i = 0; i < shoppingCartItems.length; i++) {
            var item = shoppingCartItems[i];
            var line = (i !== (shoppingCartItems.length - 1)) && <hr className="mt-1 mb-1"></hr>;
            contents.push(
                <div key={item.product.id}>
                    <ShoppingCartItem
                        product={item.product}
                        quantity={item.quantity}
                        changeQuantity={this.props.changeQuantity}
                        removeProduct={this.props.removeProduct}
                        removeComponent={this.removeShoppingCartComponent} />
                    {line}
                </div>
            );
        }

        return (
            <>
                {contents}
            </>
        );
    }
}

const mapStateToProps = state => ({
    shoppingCartItems: state.shoppingCart.items
});

export default connect(mapStateToProps, { changeQuantity, removeProduct })(ShoppingCart);
