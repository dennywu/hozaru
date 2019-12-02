import React, { Component } from "react";
import { Portlet, PortletBody } from "../../../partials/content/Portlet";
import { Row, Col } from "react-bootstrap";
import LocalShippingIcon from '@material-ui/icons/LocalShipping';
import PropTypes from "prop-types";
import Chip from '@material-ui/core/Chip';
import { Button } from '@material-ui/core';
import AirWaybillDialog from "./airwaybill-dialog";
import { StatusOrder } from "../status-order";

class ShippingOrder extends Component {
    constructor() {
        super();
        this.openAirWaybillDialog = this.openAirWaybillDialog.bind(this);
        this.handleCloseAirWaybillDialog = this.handleCloseAirWaybillDialog.bind(this);
        this.state = {
            openAirWaybillDialog: false
        };
    }

    openAirWaybillDialog() {
        this.setState({ openAirWaybillDialog: true });
    }

    handleCloseAirWaybillDialog() {
        this.setState({ openAirWaybillDialog: false });
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
                                    <div className="font-weight-bold">{order.expedition.fullName} </div>
                                    {
                                        (order.statusText === StatusOrder.PACKAGING || order.statusText === StatusOrder.SHIPPING) &&
                                        (
                                            (order.airWaybill) ?
                                                <Chip color="secondary" className="mt-5px" label={"# " + order.airWaybill} />
                                                :
                                                <span className="text-primary">Masukkan Nomor Resi Pengiriman</span>
                                        )
                                    }
                                </Col>
                                {
                                    (order.statusText === StatusOrder.PACKAGING || order.statusText === StatusOrder.SHIPPING) &&
                                    <Col xs={12} sm={6}>
                                        {
                                            !order.airWaybill &&
                                            <Button variant="outlined" color="primary" onClick={this.openAirWaybillDialog}>Masukan Nomor Resi</Button>
                                        }
                                        {
                                            order.airWaybill &&
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