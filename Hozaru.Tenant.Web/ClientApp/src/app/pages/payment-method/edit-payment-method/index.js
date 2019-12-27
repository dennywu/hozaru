import React, { Component } from 'react';
import { withRouter } from "react-router-dom";
import { Portlet, PortletBody } from '../../../partials/content/Portlet';
import { Form, Row, Col } from 'react-bootstrap';
import { getPaymentMethod, updatePaymentMethod } from "../../../crud/paymentmethod.crud";
import HozaruButton from "../../../partials/layout/hozaru-button";

class EditPaymentMethod extends Component {
    constructor() {
        super();
        this.syncPaymentMethod = this.syncPaymentMethod.bind(this);
        this.handleChangeBankBranch = this.handleChangeBankBranch.bind(this);
        this.handleChangeAccountName = this.handleChangeAccountName.bind(this);
        this.handleChangeAccountNumber = this.handleChangeAccountNumber.bind(this);
        this.handleChangeDisabled = this.handleChangeDisabled.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
        this.handleCancel = this.handleCancel.bind(this);

        this.state = {
            id: '',
            bankName: '',
            bankBranch: '',
            accountName: '',
            accountNumber: '',
            isDisabled: '',
            buttonState: '',
            buttonCancelState: '',
        };
    }

    componentDidMount() {
        this.syncPaymentMethod();
    }

    syncPaymentMethod() {
        let paymentMethodId = this.props.match.params.id;
        getPaymentMethod(paymentMethodId)
            .then(res => {
                this.setState({
                    id: res.data.id,
                    bankName: res.data.bankName,
                    bankBranch: res.data.bankBranch,
                    accountName: res.data.accountName,
                    accountNumber: res.data.accountNumber,
                    isDisabled: res.data.disabled
                });
            });
    }

    handleChangeBankBranch(ev) {
        let bankBranch = ev.target.value;
        this.setState({ bankBranch: bankBranch });
    }

    handleChangeAccountName(ev) {
        let accountName = ev.target.value;
        this.setState({ acccountName: accountName });
    }

    handleChangeAccountNumber(ev) {
        let accountNumber = ev.target.value;
        this.setState({ accountNumber: accountNumber });
    }

    handleChangeDisabled(ev) {
        let isDisabled = ev.target.checked;
        this.setState({ isDisabled: isDisabled });
    }

    handleSubmit(ev) {
        ev.preventDefault();
        this.setState({ buttonState: 'loading' });
        const self = this;
        var data = {
            id: this.state.id,
            bankBranch: this.state.bankBranch,
            accountName: this.state.accountName,
            accountNumber: this.state.accountNumber,
            isDisabled: this.state.isDisabled
        };
        updatePaymentMethod(data)
            .then(res => {
                self.props.history.push("/paymentmethods");
            })
            .finally(() => {
                this.setState({ buttonState: '' });
            });
    }

    handleCancel(ev) {
        ev.preventDefault();
        this.props.history.push("/paymentmethods");
    }

    render() {
        return (
            <div className="kt-form kt-form--label-right">
                <Portlet>
                    <PortletBody>
                        <Form onSubmit={this.handleSubmit}>
                            <Form.Group as={Row}>
                                <Col sm={6} xs={12}>
                                    <Form.Label>Bank</Form.Label>
                                    <Form.Control type="text" disabled={true} defaultValue={this.state.bankName} />
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
                                    <Form.Check
                                        custom
                                        type="checkbox"
                                        checked={this.state.isDisabled}
                                        onChange={this.handleChangeDisabled}
                                        label="Ingin di Non Aktifkan?"
                                        id={"paymentMehtod-isDisabled"}
                                    />
                                </Col>
                            </Form.Group>

                            <Form.Group as={Row}>
                                <Col>
                                    <HozaruButton className="btn btn-secondary" state={this.state.buttonCancelState} onClick={this.handleCancel}>Batal</HozaruButton>
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

export default withRouter(EditPaymentMethod);