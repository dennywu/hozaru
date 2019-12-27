import React, { Component } from "react";
import { Link } from "react-router-dom";
import { Portlet, PortletBody, PortletFooter, PortletHeader, PortletHeaderToolbar } from "../../../partials/content/Portlet";
import { Table, TableBody, TableCell, TableHead, TableRow, TablePagination } from '@material-ui/core';
import Paper from '@material-ui/core/Paper';
import { getPaymentMethods } from '../../../crud/paymentmethod.crud';
import RowPaymentMethod from "./row-payment-method";

class ListPaymentMethod extends Component {
    constructor() {
        super();
        this.syncPaymentMethod = this.syncPaymentMethod.bind(this);
        this.handleChangePage = this.handleChangePage.bind(this);
        this.handleChangeRowsPerPage = this.handleChangeRowsPerPage.bind(this);

        this.state = {
            paymentMethods: [],
            rowsPerPage: 10,
            page: 0,
            totalRow: 0
        };
    }

    componentDidMount() {
        this.syncPaymentMethod();
    }

    syncPaymentMethod(status) {
        getPaymentMethods(this.state.page, this.state.rowsPerPage)
            .then(res => {
                this.setState({ paymentMethods: res.data.items, totalRow: res.data.totalCount });
            });
    }

    handleChangePage(ev, page) {
        this.setState({ page: page }, () => {
            this.syncPaymentMethod();
        });
    }

    handleChangeRowsPerPage(ev, rowsPerPage) {
        this.setState({ rowsPerPage: rowsPerPage.key, page: 0 }, () => {
            this.syncPaymentMethod();
        });
    }

    render() {
        const { paymentMethods } = this.state;
        return (
            <div className="kt-form kt-form--label-right order-page">
                <Portlet>
                    <PortletHeader
                        toolbar={
                            <PortletHeaderToolbar>
                                <Link to="/paymentmethods/new" className="btn btn-primary btn-sm kt-btn">
                                    <i className="fa fa-plus-circle" />
                                    Tambah Metode Pembayaran
                                    </Link>
                            </PortletHeaderToolbar>
                        }
                    />
                    <PortletBody>
                        <Paper>
                            <div style={{ overflow: "auto" }}>
                                <Table stickyHeader aria-label="sticky table">
                                    <TableHead>
                                        <TableRow>
                                            <TableCell>Nama Metode Pembayaran</TableCell>
                                            <TableCell align="right">Nama Bank</TableCell>
                                            <TableCell align="right">Nama Rekening</TableCell>
                                            <TableCell align="right">Nomor Rekening</TableCell>
                                            <TableCell align="right">Perlu Konfirmasi?</TableCell>
                                            <TableCell align="right">Status</TableCell>
                                            <TableCell align="right"></TableCell>
                                        </TableRow>
                                    </TableHead>
                                    <TableBody>
                                        {paymentMethods.map(paymentMethod => (
                                            <RowPaymentMethod key={paymentMethod.id} paymentMethod={paymentMethod} />
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

export default ListPaymentMethod;