import React, { Component } from "react";
import { withRouter } from "react-router-dom";
import { Row, Col } from "react-bootstrap";
import { getOrder } from "../../../crud/order.crud";
import StatusOrder from "./status-order";
import HeaderOrder from "./header-order";
import ShippingOrder from "./shipping-order";
import ShoppingCartOrder from "./shopping-cart-order";
import PaymentOrder from "./payment-order";
import Notes from "./notes";
import Toolbar from "./toolbar";

class DetailOrder extends Component {
    constructor() {
        super();
        this.syncOrder = this.syncOrder.bind(this);
        this.state = {
            order: null
        };
    }

    componentDidMount() {
        this.syncOrder();
    }

    syncOrder() {
        const orderId = this.props.match.params.id;
        getOrder(orderId)
            .then(res => {
                this.setState({ order: res.data });
            });
    }

    render() {
        const { order } = this.state;
        if (!order) {
            return (
                <div>Orderan tidak ditemukan</div>
            );
        }
        else {
            return (
                <div className="kt-form kt-form--label-right order-page">
                    <Row>
                        <Col xs={12} sm={10}>
                            <StatusOrder order={order} />
                            <HeaderOrder order={order} />
                            <ShippingOrder order={order} />
                            <PaymentOrder order={order} />
                            <ShoppingCartOrder order={order} />
                            <Notes order={order} />
                        </Col>
                        <Col xs={12} sm={2}>
                        </Col>
                    </Row>
                </div>
            );
        }
    }
};

export default withRouter(DetailOrder);