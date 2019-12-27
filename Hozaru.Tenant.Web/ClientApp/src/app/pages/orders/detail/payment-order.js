import React, { Component } from "react";
import PropTypes from "prop-types";
import { Row, Col } from "react-bootstrap";
import { Portlet, PortletBody } from "../../../partials/content/Portlet";
import MonetizationOnIcon from '@material-ui/icons/MonetizationOn';
import { dateTimeFormat } from "../../../utils/date-utils";
import { StatusOrder } from "../status-order";
import ApprovalPaymentDialog from "./approval-payment-dialog";
import RejectPaymentDialog from "./reject-payment-dialog";
import { Button, Dialog, DialogActions, DialogContent, DialogTitle } from '@material-ui/core';


class PaymentOrder extends Component {
    constructor() {
        super();
        this.handleCloseDialogReceipt = this.handleCloseDialogReceipt.bind(this);
        this.handleOpenDialogReceipt = this.handleOpenDialogReceipt.bind(this);
        this.handleOpenApprovalPaymentDialog = this.handleOpenApprovalPaymentDialog.bind(this);
        this.handleCloseApprovalPaymentDialog = this.handleCloseApprovalPaymentDialog.bind(this);
        this.handleOpenRejectPaymentDialog = this.handleOpenRejectPaymentDialog.bind(this);
        this.handleCloseRejectPaymentDialog = this.handleCloseRejectPaymentDialog.bind(this);
        this.state = {
            receiptOpenDialog: false,
            openApprovalPaymentDialog: false,
            openRejectPaymentDialog: false
        };
    }

    static propTypes = {
        order: PropTypes.object.isRequired
    };

    handleOpenDialogReceipt() {
        this.setState({ receiptOpenDialog: true });
    }

    handleCloseDialogReceipt() {
        this.setState({ receiptOpenDialog: false });
    }

    handleOpenApprovalPaymentDialog() {
        this.setState({ openApprovalPaymentDialog: true });
    }

    handleCloseApprovalPaymentDialog() {
        this.setState({ openApprovalPaymentDialog: false });
    }

    handleOpenRejectPaymentDialog() {
        this.setState({ openRejectPaymentDialog: true });
    }

    handleCloseRejectPaymentDialog() {
        this.setState({ openRejectPaymentDialog: false });
    }

    render() {
        const { order } = this.props;
        return (
            <Portlet className={order.statusText === StatusOrder.REVIEW ? "background-warning" : ""}>
                <PortletBody>
                    <Row>
                        <Col xs={1} className="text-right">
                            <MonetizationOnIcon />
                        </Col>
                        <Col xs={11}>
                            <h6>Informasi Pembayaran</h6>
                            <Row>
                                <Col xs={12}>
                                    <div className="font-weight-bold">{order.payment.paymentMethod.name}</div>
                                </Col>
                            </Row>
                            {
                                order.statusText !== StatusOrder.DRAFT &&
                                <>
                                    <Row>
                                        <Col md={6} xs={12}>
                                            <div>Tanggal Pembayaran: <span className="font-weight-bold">{dateTimeFormat(order.payment.lastPaymentDate)}</span></div>
                                            <div>Nama Pengirim: <span className="font-weight-bold">{order.payment.lastPayment.paymentAccountName}</span></div>
                                        </Col>
                                        <Col md={6} xs={12}>
                                            <div>Transfer dari bank: <span className="font-weight-bold">{order.payment.lastPayment.paymentBankName}</span></div>
                                            <div>No. Rekening Pengirim: <span className="font-weight-bold">{order.payment.lastPayment.paymentAccountNumber}</span></div>
                                        </Col>
                                    </Row>
                                    <Row className="mt-10px">
                                        <Col xs={12} sm={6} className="mb-10px">
                                            <Button onClick={this.handleOpenDialogReceipt} variant="contained" color="secondary">
                                                Lihat bukti pembayaran
                                            </Button>
                                        </Col>
                                        <Col xs={12} sm={6}>
                                            {
                                                (order.statusText === StatusOrder.WAITINGFORPAYMENT || order.statusText === StatusOrder.PAYMENTREJECTED) &&
                                                <>
                                                    <Button variant="contained" color="primary" className="mr-10px" onClick={this.handleOpenApprovalPaymentDialog}>Setujui Pembayaran</Button>
                                                    <ApprovalPaymentDialog
                                                        order={order}
                                                        open={this.state.openApprovalPaymentDialog}
                                                        handleClose={this.handleCloseApprovalPaymentDialog}
                                                    />
                                                </>
                                            }

                                            {
                                                (order.statusText === StatusOrder.WAITINGFORPAYMENT) &&
                                                <>
                                                    <Button variant="contained" color="secondary" onClick={this.handleOpenRejectPaymentDialog}>Tolak Pembayaran</Button>
                                                    <RejectPaymentDialog
                                                        order={order}
                                                        open={this.state.openRejectPaymentDialog}
                                                        handleClose={this.handleCloseRejectPaymentDialog}
                                                    />
                                                </>
                                            }

                                            
                                        </Col>
                                    </Row>
                                    <Dialog
                                        open={this.state.receiptOpenDialog}
                                        onClose={this.handleCloseDialogReceipt}
                                        aria-labelledby="responsive-dialog-title"
                                    >
                                        <DialogTitle id="responsive-dialog-title">Bukti Pembayaran Pesanan <b> {order.orderNumber} </b></DialogTitle>
                                        <DialogContent>
                                            <img srcSet={order.payment.lastPayment.url} alt="Bukti Pembayaran" style={{ maxWidth: "400px" }} />
                                        </DialogContent>
                                        <DialogActions>
                                            <Button onClick={this.handleCloseDialogReceipt} color="primary" autoFocus>Tutup</Button>
                                        </DialogActions>
                                    </Dialog>
                                </>
                            }
                        </Col>
                    </Row>
                </PortletBody>
            </Portlet >
        );
    }
};

export default PaymentOrder;