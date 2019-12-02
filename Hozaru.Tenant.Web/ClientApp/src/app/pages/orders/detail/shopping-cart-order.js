import React, { Component } from "react";
import PropTypes from "prop-types";
import { Table, TableHead, TableRow, TableCell, TableBody } from '@material-ui/core';
import { Row, Col } from "react-bootstrap";
import DescriptionIcon from '@material-ui/icons/Description';
import { Portlet, PortletBody } from "../../../partials/content/Portlet";
import { default as NumberFormat } from 'react-number-format';
import ShoppingCartItemOrder from "./shopping-cart-item-order";

class ShoppingCartOrder extends Component {
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
                            <DescriptionIcon />
                        </Col>
                        <Col xs={11}>
                            <h6>Informasi Pesanan</h6>
                            <div style={{ overflow: "auto" }}>
                                <Table size="small">
                                    <TableHead>
                                        <TableRow>
                                            <TableCell style={{ width: "60px", paddingRight: "0px" }}>Produk</TableCell>
                                            <TableCell style={{ minWidth: "200px" }}></TableCell>
                                            <TableCell align="right" style={{ minWidth: "120px" }}>Harga Satuan</TableCell>
                                            <TableCell align="right" style={{ minWidth: "40px" }}>Jumlah</TableCell>
                                            <TableCell align="right" style={{ minWidth: "100px" }}>Subtotal</TableCell>
                                        </TableRow>
                                    </TableHead>
                                    <TableBody>
                                        {order.items.map(orderItem => (
                                            <ShoppingCartItemOrder key={orderItem.id} orderItem={orderItem} />
                                        ))}

                                        <TableRow>
                                            <TableCell rowSpan={4} colSpan={2} />
                                            <TableCell colSpan={2} align="right">Total Pesanan</TableCell>
                                            <TableCell align="right">
                                                <NumberFormat value={order.summary.subTotal} displayType={'text'} thousandSeparator={true} prefix={'Rp '} />
                                            </TableCell>
                                        </TableRow>
                                        <TableRow>
                                            <TableCell colSpan={2} align="right">Ongkos Kirim</TableCell>
                                            <TableCell align="right">
                                                <NumberFormat value={order.summary.shippingCost} displayType={'text'} thousandSeparator={true} prefix={'Rp '} />
                                            </TableCell>
                                        </TableRow>
                                        <TableRow>
                                            <TableCell colSpan={2} align="right">Potongan Diskon</TableCell>
                                            <TableCell align="right">
                                                <NumberFormat value={0} displayType={'text'} thousandSeparator={true} prefix={'Rp '} />
                                            </TableCell>
                                        </TableRow>
                                        <TableRow>
                                            <TableCell colSpan={2} align="right">Total</TableCell>
                                            <TableCell align="right">
                                                <NumberFormat value={order.summary.total} displayType={'text'} thousandSeparator={true} prefix={'Rp '} />
                                            </TableCell>
                                        </TableRow>
                                    </TableBody>
                                </Table>
                            </div>
                        </Col>
                    </Row>
                </PortletBody>
            </Portlet>            
        );
    }
}

export default ShoppingCartOrder;