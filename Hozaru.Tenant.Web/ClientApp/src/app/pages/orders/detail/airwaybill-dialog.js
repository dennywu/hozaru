import React, { Component } from "react";
import PropTypes from "prop-types";
import { TextField } from '@material-ui/core';
import { Button, Dialog, DialogActions, DialogContent, DialogTitle, DialogContentText, Slide } from '@material-ui/core';
import { updateAirWaybill } from "../../../crud/order.crud";

class AirWaybillDialog extends Component {
    constructor() {
        super();
        this.handleClose = this.handleClose.bind(this);
        this.handleConfirm = this.handleConfirm.bind(this);
        this.handleChangeAirWaybill = this.handleChangeAirWaybill.bind(this);

        this.state = {
            airWaybill: ''
        };
    }

    componentDidMount() {
        const { order } = this.props;
        this.setState({ airWaybill: order.airWaybill });
    }

    static propTypes = {
        open: PropTypes.bool.isRequired,
        order: PropTypes.object.isRequired,
        handleClose: PropTypes.func.isRequired
    };

    handleChangeAirWaybill(ev) {
        var airWaybill = ev.target.value;
        this.setState({ airWaybill: airWaybill });
    }

    handleClose() {
        this.props.handleClose();
    }

    handleConfirm() {
        const { order } = this.props;
        const self = this;
        updateAirWaybill(order.id, this.state.airWaybill)
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
                <DialogTitle id="alert-dialog-slide-title">Nomor Resi Pengiriman</DialogTitle>
                <DialogContent>
                    <DialogContentText id="alert-dialog-slide-description">
                        Masukan nomor resi atau Air Waybill pengiriman Anda.
                    </DialogContentText>
                    <TextField
                        autoFocus
                        multiline
                        rows="3"
                        rowsMax="4"
                        margin="normal"
                        variant="outlined"
                        defaultValue={this.state.airWaybill}
                        onBlur={this.handleChangeAirWaybill}
                    />
                </DialogContent>
                <DialogActions>
                    <Button onClick={this.handleClose} color="secondary">Tutup</Button>
                    <Button onClick={this.handleConfirm} color="primary">Submit</Button>
                </DialogActions>
            </Dialog>
        );
    }
}

export default AirWaybillDialog;