import React, { Component } from "react";
import PropTypes from "prop-types";
import { TextField } from '@material-ui/core';
import { Button, Dialog, DialogActions, DialogContent, DialogTitle, DialogContentText, Slide } from '@material-ui/core';
import { cancelOrder } from "../../../crud/order.crud";

class CancelOrderDialog extends Component {
    constructor() {
        super();
        this.handleClose = this.handleClose.bind(this);
        this.handleConfirm = this.handleConfirm.bind(this);
        this.handleChangeReason = this.handleChangeReason.bind(this);

        this.state = {
            reason: 'Dikarenakan Permintaan Pelanggan.'
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
        cancelOrder(order.id, this.state.reason)
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
                        Dengan klik tombol <b>Batalkan</b>, maka Pesanan ini tidak akan bisa digunakan lagi. <br />
                        Anda yakin untuk Membatalkan Pesanan <b>{order.orderNumber} </b>?
                    </DialogContentText>
                    <TextField
                        autoFocus
                        multiline
                        rows="3"
                        rowsMax="4"
                        margin="normal"
                        helperText="Masukan pesan mengapa Pesanan ini Anda batalkan"
                        variant="outlined"
                        defaultValue={this.state.reason}
                        onBlur={this.handleChangeReason}
                    />
                </DialogContent>
                <DialogActions>
                    <Button onClick={this.handleClose} color="secondary">Tutup</Button>
                    <Button onClick={this.handleConfirm} color="primary">Batalkan</Button>
                </DialogActions>
            </Dialog>
        );
    }
}

export default CancelOrderDialog;