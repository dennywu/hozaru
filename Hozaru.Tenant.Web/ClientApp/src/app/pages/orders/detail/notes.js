import React, { Component } from "react";
import PropTypes from "prop-types";
import { Row, Col } from "react-bootstrap";
import { Portlet, PortletBody } from "../../../partials/content/Portlet";

class Notes extends Component {

    static propTypes = {
        order: PropTypes.object.isRequired
    };

    render() {
        const { order } = this.props;
        if (order.note) {
            return (
                <Portlet>
                    <PortletBody>
                        <Row>
                            <Col xs={12} sm={3}>
                                <span className="font-weight-bold">Pesan Pelanggan:</span>
                            </Col>
                            <Col xs={12} sm={9}>
                                {order.note}
                            </Col>
                        </Row>
                    </PortletBody>
                </Portlet>
            );
        }
        else {
            return <></>;
        }
    }
}

export default Notes;