import React, { Component } from "react";
import PropTypes from "prop-types";
import { Button, Dialog, DialogActions, DialogContent, DialogTitle, DialogContentText, Slide } from '@material-ui/core';
import { dateTimeFormat } from "../../../utils/date-utils";
import { getOrderShipmentTracking } from "../../../crud/order.crud";

class ShipmentTrackingDialog extends Component {
    constructor() {
        super();
        this.handleClose = this.handleClose.bind(this);
        this.populateShipmentTracking = this.populateShipmentTracking.bind(this);

        this.state = {
            loading: true,
            orderShipment: {}
        };
    }

    componentDidMount() {
        const { order } = this.props;
        this.populateShipmentTracking(order.id);
    }

    static propTypes = {
        open: PropTypes.bool.isRequired,
        order: PropTypes.object.isRequired,
        handleClose: PropTypes.func.isRequired
    };

    populateShipmentTracking(orderId) {
        getOrderShipmentTracking(orderId)
            .then(res => {
                this.setState({ orderShipment: res.data, loading: false });
            });
    }

    handleClose() {
        this.props.handleClose();
    }

    render() {
        const { open } = this.props;
        if (this.state.loading) {
            return (<></>);
        }
        else {
            return (
                <Dialog
                    open={open}
                    onClose={this.handleClose}
                    aria-labelledby="alert-dialog-slide-title"
                    aria-describedby="alert-dialog-slide-description"
                >
                    <DialogTitle id="alert-dialog-slide-title">Lacak Status Pengiriman</DialogTitle>
                    <DialogContent>
                        <div className="container mt-3">
                            <div className="row border-top border-bottom p-2">
                                <div className="col-12 font-16px font-weight-500">
                                    Lacak Pesanan
                                    </div>
                            </div>

                            {
                                this.state.orderShipment.shipmentTrackings.map((tracking, index) => (
                                    <div key={"shipmentTracking" + index}>
                                        <div className={(index == 0) ? "row p-3 kt-font-success" : "row p-3"}>
                                            <div className="col-12">
                                                <span>{tracking.description}</span>
                                                <div className="font-12px">{dateTimeFormat(tracking.trackingDate)}</div>
                                            </div>
                                        </div>
                                        <hr className="mt-1 mb-1" />
                                    </div>
                                ))
                            }

                        </div>
                    </DialogContent>
                    <DialogActions>
                        <Button onClick={this.handleClose} color="primary">Tutup</Button>
                    </DialogActions>
                </Dialog>
            );
        }
    }
}

export default ShipmentTrackingDialog;