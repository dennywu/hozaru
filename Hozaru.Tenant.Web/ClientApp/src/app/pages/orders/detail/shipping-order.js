import React, { Component } from "react";
import { Portlet, PortletBody } from "../../../partials/content/Portlet";
import { Row, Col } from "react-bootstrap";
import LocalShippingIcon from '@material-ui/icons/LocalShipping';
import PropTypes from "prop-types";
import Chip from '@material-ui/core/Chip';
import { Button } from '@material-ui/core';
import AirWaybillDialog from "./airwaybill-dialog";
import ShipmentTrackingDialog from "./shipment-tracking-dialog";
import { StatusOrder } from "../status-order";
import { dateTimeFormat } from "../../../utils/date-utils"

class ShippingOrder extends Component {
    constructor() {
        super();
        this.openAirWaybillDialog = this.openAirWaybillDialog.bind(this);
        this.handleCloseAirWaybillDialog = this.handleCloseAirWaybillDialog.bind(this);
        this.openShipmentTrackingDialog = this.openShipmentTrackingDialog.bind(this);
        this.handleCloseShipmentTrackingDialog = this.handleCloseShipmentTrackingDialog.bind(this);
        this.state = {
            openAirWaybillDialog: false,
            openShipmentTrackingDialog: false
        };
    }

    openAirWaybillDialog() {
        this.setState({ openAirWaybillDialog: true });
    }

    handleCloseAirWaybillDialog() {
        this.setState({ openAirWaybillDialog: false });
    }

    openShipmentTrackingDialog() {
        this.setState({ openShipmentTrackingDialog: true });
    }

    handleCloseShipmentTrackingDialog() {
        this.setState({ openShipmentTrackingDialog: false });
    }

    static propTypes = {
        order: PropTypes.object.isRequired
    };

    render() {
        const { order } = this.props;
        return (
            <Portlet>
                <PortletBody>
                    <Row>
                        <Col xs={1} className="text-right">
                            <LocalShippingIcon />
                        </Col>
                        <Col xs={11}>
                            <h6>Informasi Pengiriman</h6>
                            <Row>
                                <Col xs={12} sm={6} className="mb-10px">
                                    <div>
                                        <span className="font-weight-bold">{order.shipment.expeditionService.fullName}</span>
                                        {
                                            order.shipment.airWayBill &&
                                            <span> ( No. Resi: {order.shipment.airWayBill} )</span>
                                        }
                                    </div>
                                    {
                                        (order.statusText === StatusOrder.PACKAGING) &&
                                        <span className="text-primary">Masukkan Nomor Resi Pengiriman</span>
                                    }
                                    {
                                        (order.statusText === StatusOrder.SHIPPING || order.statusText === StatusOrder.DONE) &&
                                        (
                                            <>
                                                <div className="mt-5px kt-font-success">{order.shipment.lastShipmentTracking.description}</div>
                                                <div className="font-12px mb-10px">{dateTimeFormat(order.shipment.lastShipmentTracking.trackingDate)}</div>
                                                <Button variant="outlined" color="secondary" onClick={this.openShipmentTrackingDialog}>Lacak</Button>
                                                {
                                                    (this.state.openShipmentTrackingDialog) &&
                                                    <ShipmentTrackingDialog
                                                        order={order}
                                                        open={this.state.openShipmentTrackingDialog}
                                                        handleClose={this.handleCloseShipmentTrackingDialog}
                                                    />
                                                }
                                            </>
                                        )
                                    }
                                </Col>
                                {
                                    (order.statusText === StatusOrder.PACKAGING || order.statusText === StatusOrder.SHIPPING) &&
                                    <Col xs={12} sm={6}>
                                        {
                                            !order.shipment.airWayBill &&
                                            <Button variant="outlined" color="primary" onClick={this.openAirWaybillDialog}>Masukan Nomor Resi</Button>
                                        }
                                        {
                                            order.shipment.airWayBill &&
                                            <Button variant="outlined" color="primary" onClick={this.openAirWaybillDialog} className="ml-10px">Ubah Nomor Resi</Button>
                                        }

                                        <AirWaybillDialog
                                            order={order}
                                            open={this.state.openAirWaybillDialog}
                                            handleClose={this.handleCloseAirWaybillDialog}
                                        />
                                    </Col>
                                }
                            </Row>
                        </Col>
                    </Row>
                </PortletBody>
            </Portlet>
        );
    }
};

export default ShippingOrder;