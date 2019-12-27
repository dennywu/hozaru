import React, { Component } from 'react';
import { withRouter } from "react-router-dom";
import { Portlet, PortletBody } from '../../../partials/content/Portlet';
import { Form, Row, Col } from 'react-bootstrap';
import { getBanks, createPaymentMethod } from "../../../crud/paymentmethod.crud";
import AsyncSelect from 'react-select/async';
import HozaruButton from "../../../partials/layout/hozaru-button";

class CreatePaymentMethod extends Component {
    constructor() {
        super();
        this.loadBanks = this.loadBanks.bind(this);
        this.handleChangeBank = this.handleChangeBank.bind(this);
        this.populateBanks = this.populateBanks.bind(this);
        this.handleChangeBankBranch = this.handleChangeBankBranch.bind(this);
        this.handleChangeAccountName = this.handleChangeAccountName.bind(this);
        this.handleChangeAccountNumber = this.handleChangeAccountNumber.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);

        this.state = {
            buttonState: '',
            bankCode: '',
            bankBranch: '',
            accountName: '',
            accountNumber: ''
        };
    }

    async populateBanks(searchKey) {
        var res = await getBanks(searchKey);
        var result = [];
        res.data.forEach(item => {
            result.push({ label: item.name, value: item.code });
        });
        return result;
    }

    loadBanks = inputValue =>
        new Promise(resolve => {
            resolve(this.populateBanks(inputValue));
        });

    handleChangeBank(bankCode) {
        this.setState({
            bankCode: bankCode
        });
    }

    handleChangeBankBranch(ev) {
        let bankBranch = ev.target.value;
        this.setState({ bankBranch: bankBranch });
    }

    handleChangeAccountName(ev) {
        let acccountName = ev.target.value;
        this.setState({ acccountName: acccountName });
    }

    handleChangeAccountNumber(ev) {
        let accountNumber = ev.target.value;
        this.setState({ accountNumber: accountNumber });
    }

    handleSubmit(ev) {
        ev.preventDefault();
        this.setState({ buttonState: 'loading' });
        const self = this;
        var data = {
            bankCode: this.state.bankCode ? this.state.bankCode.value : undefined,
            bankBranch: this.state.bankBranch,
            accountName: this.state.acccountName,
            accountNumber: this.state.accountNumber
        };
        createPaymentMethod(data)
            .then(res => {
                self.props.history.push("/paymentmethods");
            })
            .finally(() => {
                this.setState({ buttonState: '' });
            });
    }



    render() {
        return (
            <div className="kt-form kt-form--label-right">
                <Portlet>
                    <PortletBody>
                        <Form onSubmit={this.handleSubmit}>
                            <Form.Group as={Row}>
                                <Col sm={6} xs={12}>
                                    <Form.Label>Pilih Bank</Form.Label>
                                    <AsyncSelect className="react-select"
                                        placeholder="Pilih Bank"
                                        defaultOptions
                                        loadOptions={this.loadBanks}
                                        value={this.state.bankCode}
                                        onChange={this.handleChangeBank}
                                    />
                                </Col>
                            </Form.Group>
                            <Form.Group as={Row}>
                                <Col sm={6} xs={12}>
                                    <Form.Label>Cabang Bank</Form.Label>
                                    <Form.Control type="text" placeholder="Kantor Cabang Bank" defaultValue={this.state.bankBranch} onBlur={this.handleChangeBankBranch} />
                                    <Form.Text className="text-muted">
                                        Masukkan Kantor Cabang rekening Anda atau Kota dimana rekening Anda dibuat.
                                </Form.Text>
                                </Col>
                            </Form.Group>
                            <Form.Group as={Row}>
                                <Col>
                                    <Form.Label>Nama Rekening</Form.Label>
                                    <Form.Control type="text" placeholder="Nama Rekening Anda" defaultValue={this.state.accountName} onBlur={this.handleChangeAccountName} />
                                </Col>
                                <Col>
                                    <Form.Label>Nomor Rekening</Form.Label>
                                    <Form.Control type="text" placeholder="Nomor Rekening Anda" defaultValue={this.state.accountNumber} onBlur={this.handleChangeAccountNumber} />
                                </Col>
                            </Form.Group>
                            <Form.Group as={Row}>
                                <Col>
                                    <HozaruButton type="submit" className="btn btn-primary" state={this.state.buttonState}>Simpan</HozaruButton>
                                </Col>
                            </Form.Group>
                        </Form>
                    </PortletBody>
                </Portlet>
            </div >
        );
    }
}

export default withRouter(CreatePaymentMethod);