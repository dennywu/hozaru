import React, { Component } from 'react';
import PropTypes from 'prop-types';
import { default as NumberFormat } from 'react-number-format';

class OrderItem extends Component {
    static propTypes = {
        item: PropTypes.object.isRequired
    };

    render() {
        const { item } = this.props;
        return (
            <>
                <div className="col-3 image-product">
                    <img className="w100" srcSet={"/api/product/" + item.product.id + "/image"} alt={item.product.name} />
                </div>
                <div className="col-9">
                    <div className="font-weight-600 text-truncate">{item.product.name}</div>
                    <div className="row">
                        <div className="col-12">
                            <span className="font-12px">
                                {item.quantity} x <NumberFormat value={item.price} displayType={'text'} thousandSeparator={true} prefix={'Rp '} />
                            </span>
                        </div>
                        <div className="col-12 font-weight-500 color-orange">
                            <NumberFormat value={item.total} displayType={'text'} thousandSeparator={true} prefix={'Rp '} />
                        </div>
                    </div>
                </div>
            </>
        );
    }
}

export default OrderItem;