import React, { Component } from "react";
import PropTypes from "prop-types";
import { Portlet, PortletBody } from "../../../partials/content/Portlet";
import DescriptionIcon from '@material-ui/icons/Description';
import RoomIcon from '@material-ui/icons/Room';
import { Row, Col } from "react-bootstrap";
import { Button } from '@material-ui/core';

class HeaderOrder extends Component {
    static propTypes = {
        order: PropTypes.object.isRequired
    };

    render() {
        const { order } = this.props;
        return (
            <Portlet>
                <PortletBody>
                    <Row>
                        <Col xs={1} sm={1} className="text-right">
                            <DescriptionIcon />
                        </Col>
                        <Col xs={11} sm={5} className="mb-10px">
                            <h6>Nomor Pesanan</h6>
                            <div className="">{order.orderNumber}</div>
                            <div className="mt-5px">
                                <Button variant="outlined" color="secondary" onClick={() => { window.open(order.customer.whatsappUrl) }}>Hubungi Pembeli</Button>
                            </div>
                        </Col>
                        <Col xs={1} sm={1} className="text-right">
                            <RoomIcon />
                        </Col>
                        <Col xs={11} sm={5}>
                            <h6>Alamat Pengiriman</h6>
                            <div className="">{order.customer.customerName}, {order.customer.whatsappNumber}</div>
                            <div className="">{order.customer.address}</div>
                        </Col>
                    </Row>
                </PortletBody>
            </Portlet>
        );
    }
}

export default HeaderOrder;