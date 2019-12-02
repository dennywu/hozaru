import React, { Component } from 'react';
import { Tab, Tabs } from "@material-ui/core";
import AppBar from '@material-ui/core/AppBar';

class TabStatus extends Component {
    render() {
        return (
            <AppBar position="static" color="default">
                <Tabs
                    indicatorColor="primary"
                    textColor="primary"
                    variant="scrollable"
                    scrollButtons="on"
                    value={this.props.activeTab}
                    onChange={this.props.handleChangeStatus}
                >
                    <Tab label="Semua" style={{ minWidth:"130px" }} />
                    <Tab label="Belum Bayar" style={{ minWidth: "130px" }}/>
                    <Tab label="Verifikasi Pembayaran" style={{ minWidth: "130px" }}/>
                    <Tab label="Gagal Bayar" style={{ minWidth: "130px" }}/>
                    <Tab label="Perlu Dikirim" style={{ minWidth: "130px" }}/>
                    <Tab label="Sedang Dikirim" style={{ minWidth: "130px" }}/>
                    <Tab label="Selesai" style={{ minWidth: "130px" }}/>
                    <Tab label="Pembatalan" style={{ minWidth: "130px" }}/>
                    <Tab label="Pengembalian" style={{ minWidth: "130px" }}/>
                </Tabs>
            </AppBar>
        );
    }
}

export default TabStatus;
