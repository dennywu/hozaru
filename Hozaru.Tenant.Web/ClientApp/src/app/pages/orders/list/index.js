import React, { Component } from "react";
import { Portlet, PortletBody, PortletFooter, PortletHeader, PortletHeaderToolbar } from "../../../partials/content/Portlet";
import { Table, TableBody, TableCell, TableHead, TableRow, TablePagination } from '@material-ui/core';
import Paper from '@material-ui/core/Paper';
import { getOrders } from "../../../crud/order.crud";
import TabStatus from "./tab-status";
import RowOrder from "./row-order";
import { StatusOrder } from "../status-order";
import "../style.css";

class ListOrder extends Component {
    constructor() {
        super();
        this.handleChangeStatus = this.handleChangeStatus.bind(this);
        this.syncOrders = this.syncOrders.bind(this);
        this.handleChangePage = this.handleChangePage.bind(this);
        this.handleChangeRowsPerPage = this.handleChangeRowsPerPage.bind(this);

        this.state = {
            activeTab: 0,
            orders: [],
            rowsPerPage: 10,
            page: 0,
            totalRow: 0,
            status: StatusOrder.ALL
        };
    }

    componentDidMount() {
        this.syncOrders();
    }

    syncOrders(status) {
        getOrders(this.state.status, this.state.page, this.state.rowsPerPage)
            .then(res => {
                this.setState({ orders: res.data.items, totalRow: res.data.totalCount });
            });
    }

    handleShowDetailOrder(ev) {
        ev.preventDefault();
        alert("showDetail");
    }

    handleChangeStatus(event, newValue) {
        this.setState({ activeTab: newValue })
        var status = StatusOrder.ALL;
        switch (newValue) {
            case 0: status = StatusOrder.ALL; break;
            case 1: status = StatusOrder.DRAFT; break;
            case 2: status = StatusOrder.WAITINGFORPAYMENT; break;
            case 3: status = StatusOrder.PAYMENTREJECTED; break;
            case 4: status = StatusOrder.PACKAGING; break;
            case 5: status = StatusOrder.SHIPPING; break;
            case 6: status = StatusOrder.DONE; break;
            case 6: status = StatusOrder.CANCELED; break;
        }
        this.setState({ status: status, page: 0 }, () => {
            this.syncOrders();
        });
    }

    handleChangePage(ev, page) {
        this.setState({ page: page }, () => {
            this.syncOrders();
        });
    }

    handleChangeRowsPerPage(ev, rowsPerPage) {
        this.setState({ rowsPerPage: rowsPerPage.key, page: 0 }, () => {
            this.syncOrders();
        });
    }

    render() {
        const { orders } = this.state;
        return (
            <div className="kt-form kt-form--label-right order-page">
                <TabStatus activeTab={this.state.activeTab} handleChangeStatus={this.handleChangeStatus} />
                <Portlet>
                    <PortletHeader
                        toolbar={
                            <PortletHeaderToolbar>
                            </PortletHeaderToolbar>
                        }
                    />
                    <PortletBody>
                        <Paper>
                            <div style={{ overflow: "auto" }}>
                                <Table stickyHeader aria-label="sticky table">
                                    <TableHead>
                                        <TableRow>
                                            <TableCell>Nomor Order</TableCell>
                                            <TableCell align="right">Nama Pelanggan</TableCell>
                                            <TableCell align="right">Jumlah&nbsp;(Rp)</TableCell>
                                            <TableCell align="right">Jasa Pengiriman</TableCell>
                                            <TableCell align="right">Status</TableCell>
                                            <TableCell align="right"></TableCell>
                                        </TableRow>
                                    </TableHead>
                                    <TableBody>
                                        {orders.map(order => (
                                            <RowOrder key={order.id} order={order} />
                                        ))}
                                    </TableBody>
                                </Table>
                            </div>
                            {
                                this.state.totalRow > 0 &&
                                <TablePagination
                                    rowsPerPageOptions={[5, 10, 25]}
                                    component="div"
                                    count={this.state.totalRow}
                                    rowsPerPage={this.state.rowsPerPage}
                                    page={this.state.page}
                                    backIconButtonProps={{
                                        'aria-label': 'Previous Page',
                                    }}
                                    nextIconButtonProps={{
                                        'aria-label': 'Next Page',
                                    }}
                                    onChangePage={this.handleChangePage}
                                    onChangeRowsPerPage={this.handleChangeRowsPerPage}
                                />
                            }
                        </Paper>
                    </PortletBody>
                </Portlet>
            </div>
        );
    }
}

export default ListOrder;