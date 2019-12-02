import React, { Component } from "react";
import PropTypes from "prop-types";
import { TableRow, TableCell } from '@material-ui/core';
import { default as NumberFormat } from 'react-number-format';

class ShoppingCartItemOrder extends Component {
    static propTypes = {
        orderItem: PropTypes.object.isRequired
    };

    render() {
        const { orderItem } = this.props;
        return (
            <TableRow>
                <TableCell style={{ paddingRight: "0px" }}>
                    <img style={{ height: "60px" }} srcSet={orderItem.product.firstProductImage.url} />
                </TableCell>
                <TableCell style={{ paddingRight: "0px" }}>
                    <span>{orderItem.product.name}</span>
                </TableCell>
                <TableCell align="right">
                    <NumberFormat value={orderItem.price} displayType={'text'} thousandSeparator={true} prefix={''} />
                </TableCell>
                <TableCell align="right">
                    {orderItem.quantity}
                </TableCell>
                <TableCell align="right">
                    <NumberFormat value={orderItem.total} displayType={'text'} thousandSeparator={true} prefix={''} />
                </TableCell>
            </TableRow>
        );
    }
}

export default ShoppingCartItemOrder;