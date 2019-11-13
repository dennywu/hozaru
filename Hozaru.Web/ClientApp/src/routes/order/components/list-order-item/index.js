import React, { Component } from 'react';
import PropTypes from 'prop-types';
import OrderItem from './order-item';

class ListOrderItem extends Component {
    static propTypes = {
        order: PropTypes.object.isRequired
    };

    render() {
        const { order } = this.props;
        let contents = [];
        for (var i = 0; i < order.items.length; i++) {
            var item = order.items[i];
            var line = (i !== (order.items.length - 1)) && <hr className="mt-1 mb-1"></hr>;
            contents.push(
                <div className="row mt-2 mb-2" key={item.id}>
                    <OrderItem item={item} />
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

export default ListOrderItem;