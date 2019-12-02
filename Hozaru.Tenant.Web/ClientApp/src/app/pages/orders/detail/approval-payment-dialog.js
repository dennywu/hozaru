import React, { Component } from "react";
import { withRouter } from "react-router-dom";
import PropTypes from "prop-types";
import { Button, Dialog, DialogActions, DialogContent, DialogTitle, DialogContentText, Slide } from '@material-ui/core';
import { approveOrder } from "../../../crud/order.crud";

class ApprovalPaymentDialog extends Component {
    constructor() {
        super();
        this.handleClose = this.handleClose.bind(this);
        this.handleConfirm = this.handleConfirm.bind(this);
    }

    static propTypes = {
        open: PropTypes.bool.isRequired,
        order: PropTypes.object.isRequired,
        handleClose: PropTypes.func.isRequired
    };

    handleClose() {
        this.props.handleClose();
    }

    handleConfirm() {
        const { order } = this.props;
        const self = this;
        approveOrder(order.id)
            .then(res => {
                window.location.reload();
            })
            .finally(() => {
                self.props.handleClose();
            });
    }

    render() {
        const { order, open } = this.props;
        return (
            <Dialog
                open={open}
                onClose={this.handleClose}
                aria-labelledby="alert-dialog-slide-title"
                aria-describedby="alert-dialog-slide-description"
            >
                <DialogTitle id="alert-dialog-slide-title"></DialogTitle>
                <DialogContent>
                    <DialogContentText id="alert-dialog-slide-description">
                        Pastikan bukti pembayaran ini adalah benar. <br />
                        Anda yakin untuk Setujui Pembayaran Pesanan <b>{order.orderNumber} </b>?
                    </DialogContentText>
                </DialogContent>
                <DialogActions>
                    <Button onClick={this.handleClose} color="secondary">Tidak</Button>
                    <Button onClick={this.handleConfirm} color="primary">Setuju</Button>
                </DialogActions>
            </Dialog>
        );
    }
}

export default withRouter(ApprovalPaymentDialog);