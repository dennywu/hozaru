import React, { Component } from "react";
import PropTypes from "prop-types";
import { TextField } from '@material-ui/core';
import { Button, Dialog, DialogActions, DialogContent, DialogTitle, DialogContentText, Slide } from '@material-ui/core';
import { rejectOrder } from "../../../crud/order.crud";

class RejectPaymentDialog extends Component {
    constructor() {
        super();
        this.handleClose = this.handleClose.bind(this);
        this.handleConfirm = this.handleConfirm.bind(this);
        this.handleChangeReason = this.handleChangeReason.bind(this);

        this.state = {
            reason: 'Dikarenakan Anda salah Upload Bukti Transfer.'
        };
    }

    static propTypes = {
        open: PropTypes.bool.isRequired,
        order: PropTypes.object.isRequired,
        handleClose: PropTypes.func.isRequired
    };

    handleChangeReason(ev) {
        var reason = ev.target.value;
        this.setState({ reason: reason });
    }

    handleClose() {
        this.props.handleClose();
    }

    handleConfirm() {
        const { order } = this.props;
        const self = this;
        rejectOrder(order.id, this.state.reason)
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
                        Dengan klik tombol <b>Tolak</b>, maka Pembayaran customer adalah Tidak Benar. <br />
                        Anda yakin untuk Menolak Pembayaran Pesanan <b>{order.orderNumber} </b>?
                    </DialogContentText>
                    <TextField
                        autoFocus
                        multiline
                        rows="3"
                        rowsMax="4"
                        margin="normal"
                        helperText="Masukan pesan mengapa Pembayaran ini Anda tolak"
                        variant="outlined"
                        value={this.state.reason}
                        onBlur={this.handleChangeReason}
                    />
                </DialogContent>
                <DialogActions>
                    <Button onClick={this.handleClose} color="secondary">Tutup</Button>
                    <Button onClick={this.handleConfirm} color="primary">Tolak</Button>
                </DialogActions>
            </Dialog>
        );
    }
}

export default RejectPaymentDialog;