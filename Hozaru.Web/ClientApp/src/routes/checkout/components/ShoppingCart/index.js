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

        return (
            <>
                {shoppingCartItems.map(item =>
                    <div key={item.product.id}>
                        <ShoppingCartItem
                            product={item.product}
                            quantity={item.quantity}
                            changeQuantity={this.props.changeQuantity}
                            removeProduct={this.props.removeProduct}
                            removeComponent={this.removeShoppingCartComponent} />
                        <hr className="mt-1 mb-1"></hr>
                    </div>
                )}
            </>
        );
    }
}

const mapStateToProps = state => ({
    shoppingCartItems: state.shoppingCart.items
});

export default connect(mapStateToProps, { changeQuantity, removeProduct })(ShoppingCart);
