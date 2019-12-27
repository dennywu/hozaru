import React, { Component } from "react";
import PropTypes from "prop-types";
import Button from '@material-ui/core/Button';
import { Row, Col } from "react-bootstrap";
import { StatusOrder } from "../status-order";
import CancelOrderDialog from "./cancel-order-dialog";
import CompleteOrderDialog from "./complete-order-dialog";

class Toolbar extends Component {
    constructor() {
        super();
        this.handleOpenCancelOrderDialog = this.handleOpenCancelOrderDialog.bind(this);
        this.handleCloseCancelOrderDialog = this.handleCloseCancelOrderDialog.bind(this);

        this.handleOpenCompleteOrderDialog = this.handleOpenCompleteOrderDialog.bind(this);
        this.handleCloseCompleteOrderDialog = this.handleCloseCompleteOrderDialog.bind(this);

        this.state = {
            openCancelOrderDialog: false,
            openCompleteOrderDialog: false,
        };
    }

    handleOpenCancelOrderDialog() {
        this.setState({ openCancelOrderDialog: true });
    }

    handleCloseCancelOrderDialog() {
        this.setState({ openCancelOrderDialog: false });
    }

    handleOpenCompleteOrderDialog() {
        this.setState({ openCompleteOrderDialog: true });
    }

    handleCloseCompleteOrderDialog() {
        this.setState({ openCompleteOrderDialog: false });
    }

    static propTypes = {
        order: PropTypes.object.isRequired
    };

    render() {
        const { order } = this.props;
        return (
            <>
                {
                    (order.statusText === StatusOrder.PACKAGING || order.statusText === StatusOrder.SHIPPING) &&
                    <Row>
                        <Col xs={12}>
                            <Button variant="contained" color="primary" onClick={this.handleOpenCompleteOrderDialog}>Selesaikan Pesanan</Button>
                            <CompleteOrderDialog
                                order={order}
                                open={this.state.openCompleteOrderDialog}
                                handleClose={this.handleCloseCompleteOrderDialog}
                            />
                        </Col>
                    </Row>
                }

                {
                    (order.statusText === StatusOrder.VOID || order.statusText === StatusOrder.DONE || order.statusText === StatusOrder.RETURNED) ?
                        <></>
                        :
                        <Row className="mt-10px">
                            <Col xs={12}>
                                <Button variant="contained" color="secondary" onClick={this.handleOpenCancelOrderDialog}>Batalkan Pesanan</Button>
                                <CancelOrderDialog
                                    order={order}
                                    open={this.state.openCancelOrderDialog}
                                    handleClose={this.handleCloseCancelOrderDialog}
                                />
                            </Col>
                        </Row>
                }
            </>
        );
    }
}

export default Toolbar;