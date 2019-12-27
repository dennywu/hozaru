import React, { Component } from 'react';
import { default as NumberFormat } from 'react-number-format';
import { dateTimeFormat } from '../../utils/DateUtil';
import Loading from '../../components/loading';
import axios from 'axios';

class Payment extends Component {
    constructor() {
        super();
        this.populateOrder = this.populateOrder.bind(this);
        this.handleClickUploadPayment = this.handleClickUploadPayment.bind(this);
        this.handleClickPayLater = this.handleClickPayLater.bind(this);

        this.state = {
            order: {},
            orderId: '',
            loading: true
        };
    }

    componentDidMount() {
        let orderId = this.props.match.params.id;
        this.setState({
            orderId: orderId
        }, () => {
            this.populateOrder();
        });
    }

    async populateOrder() {
        axios.get('/api/orders/' + this.state.orderId)
            .then(res => {
                this.setState({ order: res.data, loading: false });
            });
    }

    handleClickUploadPayment(event) {
        event.preventDefault();
        this.props.history.push('/payment-confirmation/' + this.state.orderId);
    }

    handleClickPayLater(event) {
        event.preventDefault();
        this.props.history.push('/order/' + this.state.orderId);
    }

    render() {
        if (this.state.loading)
            return <Loading />;

        return (
            <div className="container">
                <div className="row">
                    <div className="col-12 font-15px">Total Pembayaran:</div>
                    <div className="col-12 color-orange font-weight-bold font-27px">
                        <NumberFormat value={this.state.order.summary.netTotal} displayType={'text'} thousandSeparator={true} prefix={'Rp '} />
                    </div>
                </div>
                <div className="row mt-2">
                    <div className="col-12">
                        <div className="background-warning p-1 pl-2">Bayar pesanan sesuai jumlah diatas</div>
                    </div>
                </div>
                <div className="row mt-3 mb-3">
                    <div className="col-12 font-15px ">
                        Dicek dalam 24 jam setelah bukti transfer diupload.
                    </div>
                </div>
                <div className="row mt-3 mb-3 border-top border-bottom background-grey">
                    <div className="col-1 pt-2 pb-2">
                        <span className="badge badge-secondary">1</span>
                    </div>
                    <div className="col-10 pl-1">
                        <div className="pt-2 pb-2">
                            Gunakan ATM / iBanking / mBanking / setor tunai untuk transfer ke rekening berikut ini:
                        </div>
                    </div>
                </div>
                <div className="row">
                    <div className="col-12">
                        <div className="row pl-4">
                            <div className="col-1">
                                <img className='logobank'
                                    alt={this.state.order.payment.paymentMethod.bankName}
                                    srcSet={this.state.order.payment.paymentMethod.imageUrl} />
                            </div>
                            <div className="col-9 pl-6">
                                <div className="font-weight-bold">{this.state.order.payment.paymentMethod.bankName}</div>
                                <div>No. Rekening: <span className="font-weight-bold">{this.state.order.payment.paymentMethod.accountNumber}</span></div>
                                <div>Nama Rekening: <span className="font-weight-bold">{this.state.order.payment.paymentMethod.accountName}</span></div>
                                <div>Cabang: <span className="font-weight-bold">{this.state.order.payment.paymentMethod.bankBranch}</span></div>
                            </div>
                        </div>
                    </div>
                </div>
                <div className="row mt-3 background-grey border-top">
                    <div className="col-1 pt-2 pb-2">
                        <span className="badge badge-secondary">2</span>
                    </div>
                    <div className="col-10 pl-1">
                        <div className="pt-2 pb-2">
                            Silahkan upload bukti transfer sebelum:
                            <div className="font-weight-bold">{dateTimeFormat(this.state.order.dueDateConfirmation)}</div>
                        </div>
                    </div>
                </div>
                <div className="row mb-3 background-grey">
                    <div className="col-1 pt-2 pb-2">
                        <span className="badge badge-secondary">3</span>
                    </div>
                    <div className="col-10 pl-1">
                        <div className="pt-2 pb-2">
                            Demi keamanan transaksi, mohon untuk tidak membagikan bukti atau konfirmasi pembayaran pesanan
                                kepada siapapun, selain mengunggahnya disini
                        </div>
                    </div>
                </div>
                <div className="row mt-4">
                    <button type="submit" className="btn btn-primary margin-center width-95percent" onClick={this.handleClickUploadPayment}>
                        Upload bukti transfer Sekarang
                        </button>
                </div>
                <div className="row mt-3 mb-2">
                    <button type="submit" className="btn btn-secondary margin-center width-95percent" onClick={this.handleClickPayLater}>Upload bukti transfer Nanti Saja</button>
                </div>
            </div>
        );
    }
};

export default Payment;