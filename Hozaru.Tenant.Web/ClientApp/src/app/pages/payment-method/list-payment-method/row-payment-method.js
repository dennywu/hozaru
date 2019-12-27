import React, { Component } from "react";
import { withRouter } from "react-router-dom";
import { TableCell, TableRow } from '@material-ui/core';

class RowPaymentMethod extends Component {
    constructor() {
        super();
        this.handleShowEditPaymentMethod = this.handleShowEditPaymentMethod.bind(this);
    }

    handleShowEditPaymentMethod() {
        const { paymentMethod } = this.props;
        this.props.history.push("/paymentmethods/" + paymentMethod.id + "/edit");
    }

    render() {
        const { paymentMethod } = this.props;
        return (
            <TableRow onClick={this.handleShowEditPaymentMethod}>
                <TableCell component="th" scope="row">{paymentMethod.name}</TableCell>
                <TableCell align="right">{paymentMethod.bankName}</TableCell>
                <TableCell align="right">{paymentMethod.accountName}</TableCell>
                <TableCell align="right">{paymentMethod.accountNumber}</TableCell>
                <TableCell align="right">{paymentMethod.isManualConfirmation ? "Iya" : "Tidak"}</TableCell>
                <TableCell align="right">{paymentMethod.disabled ? "Non Aktif" : "Aktif"}</TableCell>
                <TableCell align="right">
                    <a className="text-underline">Edit</a>
                </TableCell>
            </TableRow>
        );
    }
}

export default withRouter(RowPaymentMethod);