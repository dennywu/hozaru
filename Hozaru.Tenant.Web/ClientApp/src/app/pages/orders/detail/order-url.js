import React, { Component } from "react";
import PropTypes from "prop-types";
import { Row, Col } from "react-bootstrap";
import { Portlet, PortletBody } from "../../../partials/content/Portlet";

class OrderUrl extends Component {

    static propTypes = {
        order: PropTypes.object.isRequired
    };

    render() {
        const { order } = this.props;
        return (
            <Portlet>
                <PortletBody>
                    <Row>
                        <Col xs={12} sm={3}>
                            <span className="font-weight-bold">Link Pesanan:</span>
                        </Col>
                        <Col xs={12} sm={9}>
                            <a href={order.orderUrl} target="_blank">{order.orderUrl}</a>
                        </Col>
                    </Row>
                </PortletBody>
            </Portlet>
        );
    }
}

export default OrderUrl;