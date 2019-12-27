import React, { Component } from "react";
import PropTypes from "prop-types";
import { TextField } from '@material-ui/core';
import { Button, Dialog, DialogActions, DialogContent, DialogTitle, DialogContentText, Slide } from '@material-ui/core';
import { completeOrder } from "../../../crud/order.crud";

class CompleteOrderDialog extends Component {
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
        completeOrder(order.id)
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
                <DialogContent>
                    <DialogContentText id="alert-dialog-slide-description">
                        Dengan klik tombol <b>Selesaikan</b>, maka Pesanan ini Akan diselesaikan dan tidak bisa diubah kembali.<br />
                        Anda yakin untuk Selesaikan Pesanan <b>{order.orderNumber} </b>?
                    </DialogContentText>
                </DialogContent>
                <DialogActions>
                    <Button onClick={this.handleClose} color="secondary">Tutup</Button>
                    <Button onClick={this.handleConfirm} color="primary">Selesaikan</Button>
                </DialogActions>
            </Dialog>
        );
    }
}

export default CompleteOrderDialog;