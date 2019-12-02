import React, { Component } from "react";
import { withRouter } from "react-router-dom";
import { TableCell, TableRow } from '@material-ui/core';
import { getStatusIndonesia } from "../status-order";

class RowOrder extends Component {
    constructor() {
        super();
        this.handleShowDetailOrder = this.handleShowDetailOrder.bind(this);
    }

    handleShowDetailOrder() {
        const { order } = this.props;
        this.props.history.push("/orders/" + order.id + "/detail");
    }

    render() {
        const { order } = this.props;
        return (
            <TableRow onClick={this.handleShowDetailOrder}>
                <TableCell component="th" scope="row">{order.orderNumber}</TableCell>
                <TableCell align="right">{order.customerName}</TableCell>
                <TableCell align="right">{order.totalSummary}</TableCell>
                <TableCell align="right">{order.expeditionFullName}</TableCell>
                <TableCell align="right">{getStatusIndonesia(order.statusText)}</TableCell>
                <TableCell align="right">
                    <a className="text-underline">Periksa Rincian</a>
                </TableCell>
            </TableRow>
        );
    }
}

export default withRouter(RowOrder);