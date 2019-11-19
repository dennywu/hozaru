import React, { Component } from 'react';
import { default as NumberFormat } from 'react-number-format';
import Dropzone from '../../components/dropzone';
import axios from 'axios';
import Loading from '../../components/loading';
import HozaruButton from '../../components/hozaru-button';

class PaymentConfirmation extends Component {
    constructor() {
        super();
        this.populateOrder = this.populateOrder.bind(this);
        this.handleAddImage = this.handleAddImage.bind(this);
        this.removeReceiptImage = this.removeReceiptImage.bind(this);
        this.handleChangeAccountName = this.handleChangeAccountName.bind(this);
        this.handleChangeAccountNumber = this.handleChangeAccountNumber.bind(this);
        this.handleChangeBankName = this.handleChangeBankName.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
        this.validate = this.validate.bind(this);

        this.state = {
            loading: true,
            buttonState: '',
            order: {
                paymentType: {
                    code: '',
                    bankName: '',
                    accountNumber: '',
                    accountName: '',
                    bankBranch: ''
                },
                summary: {
                    total: 0
                }
            },
            orderId: '',
            receiptImage: '',
            fileReceiptImage: null,
            accountName: '',
            accountNumber: '',
            bankName: '',
            errors: {
                accountName: '',
                accountNumber: '',
                bankName: ''
            }
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
        axios.get('/api/order', { params: { id: this.state.orderId } })
            .then(res => {
                this.setState({ order: res.data, loading: false });
            });
    }

    handleAddImage(files) {
        let _self = this;
        this.setState({
            fileReceiptImage: files[0]
        });

        for (var i = 0; i < files.length; i++) {
            var reader = new FileReader();
            reader.onload = (function (aImg) {
                _self.setState({
                    receiptImage: aImg.target.result
                });
            });

            reader.readAsDataURL(files[i]);
        }
    }

    removeReceiptImage() {
        this.setState({
            receiptImage: ''
        });
    }

    handleChangeAccountName(ev) {
        let accountName = ev.target.value;
        this.setState({ errors: { ...this.state.errors, accountName: this.validate(accountName, 'Nama Rekening Pengirim') } });
        this.setState({ accountName: accountName });
    }

    handleChangeAccountNumber(ev) {
        let accountNumber = ev.target.value;
        this.setState({ errors: { ...this.state.errors, accountNumber: this.validate(accountNumber, 'Nomor Rekening Pengirim') } });
        this.setState({ accountNumber: accountNumber });
    }

    handleChangeBankName(ev) {
        let bankName = ev.target.value;
        this.setState({ errors: { ...this.state.errors, bankName: this.validate(bankName, 'Nama Bank Pengirim') } });
        this.setState({ bankName: bankName });
    }

    validate(obj, objName) {
        if (obj === '') {
            return 'Silahkan isi ' + objName;
        } else {
            return '';
        }
    }

    handleSubmit(ev) {
        ev.preventDefault();
        this.setState({
            errors: {
                accountName: this.validate(this.state.accountName, 'Nama Rekening Pengirim'),
                accountNumber: this.validate(this.state.accountNumber, 'Nomor Rekening Pengirim'),
                bankName: this.validate(this.state.bankName, 'Nama Bank Pengirim')
            },
            buttonState: 'loading'
        }, () => {
            if (this.state.errors.accountName !== '' && this.state.errors.accountNumber !== '' && this.state.errors.bankName !== '')
                return;

            if (this.state.receiptImage === '') {
                alert('Silahkan Upload Bukti Transfer');
                return;
            }

            const formData = new FormData();
            formData.append('paymentReceipt', this.state.fileReceiptImage);
            formData.append('id', this.state.orderId);
            formData.append('bankName', this.state.bankName);
            formData.append('accountName', this.state.accountName);
            formData.append('accountNumber', this.state.accountNumber);

            const config = {
                headers: {
                    'content-type': 'multipart/form-data',
                },
            };

            axios.post("/api/order/confirmation", formData, config)
                .then(response => {
                    if (response.statusText === "OK") {
                        this.props.history.push('/order/' + this.state.orderId);
                    }
                    else {
                        alert(response.data);
                    }
                })
                .finally(() => {
                    this.setState({ buttonState: '' });
                });
        });
    }

    render() {
        if (this.state.loading)
            return <Loading />;

        let uploadReceiptContent = "";
        if (this.state.receiptImage) {
            uploadReceiptContent =
                <>
                    <div className='col-12' key={"upload-receipt"}>
                        <img src={this.state.receiptImage}
                            alt="bukti transfer"
                            className="margin-center"
                            style={{ height: 150, display: "block" }} />
                        <div
                            className="mt-2 hand text-center color-orange text-underline"
                            onClick={this.removeReceiptImage}
                        >
                            Hapus bukti transfer
                        </div>
                    </div>
                </>
        }
        else {
            uploadReceiptContent =
                <div className='col-12' key={"upload-receipt"}>
                    <Dropzone onFilesAdded={this.handleAddImage} />
                </div>
        }

        return (
            <form onSubmit={this.handleSubmit}>
                <div className="container">
                    <div className="row pt-2 pb-2">
                        <div className='col-5'>Total Pembayaran</div>
                        <div className='col-7 text-right'>
                            <span className="color-orange font-weight-bold">
                                <NumberFormat value={this.state.order.summary.total} displayType={'text'} thousandSeparator={true} prefix={'Rp '} />
                            </span>
                        </div>
                    </div>
                    <div className="row pt-2 pb-2 border-top">
                        <div className='col-12'>Informasi Bank</div>
                    </div>
                    <div className="row pt-2 pb-2 border-top">
                        <div className="col-12">
                            <div className="row pl-1">
                                <div className="col-1">
                                    <img className='logobank'
                                        alt={this.state.order.paymentType.bankName}
                                        srcSet={this.state.order.paymentType.imageUrl} />
                                </div>
                                <div className="col-9 pl-6">
                                    <div className="font-weight-bold">{this.state.order.paymentType.bankName}</div>
                                    <div>No. Rekening: <span className="font-weight-bold">{this.state.order.paymentType.accountNumber}</span></div>
                                    <div>Nama Rekening: <span className="font-weight-bold">{this.state.order.paymentType.accountName}</span></div>
                                    <div>Cabang: <span className="font-weight-bold">{this.state.order.paymentType.bankBranch}</span></div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div className="row pt-2 pb-2 border-top border-bottom background-grey">
                        <div className='col-12'>
                            Pastikan Anda telah melengkapi seluruh informasi sebelum upload bukti transfer.
                            Kami akan memeriksa bukti Anda dalam 24 jam.
                    </div>
                    </div>
                    <div className="row pt-3 pb-3">
                        {uploadReceiptContent}
                    </div>
                    <div className={this.state.errors.accountName !== '' ? "row pt-2 pb-2 border-top has-error" : "row pt-2 pb-2 border-top"}>
                        <div className='col-6'>Nama Pengirim</div>
                        <div className='col-6'>
                            <input type="text"
                                className="form-control"
                                placeholder="Nama Rekening"
                                defaultValue={this.state.accountName}
                                onBlur={this.handleChangeAccountName}
                            />
                        </div>
                        {
                            (this.state.errors.accountName) &&
                            <div className="col-12 pl-5 text-right">
                                <small className="form-text text-muted">
                                    {this.state.errors.accountName}
                                </small>
                            </div>
                        }
                    </div>
                    <div className={this.state.errors.accountNumber !== '' ? "row pt-2 pb-2 border-top has-error" : "row pt-2 pb-2 border-top"}>
                        <div className='col-6'>No. Rekening Pengirim</div>
                        <div className='col-6'>
                            <input type="text"
                                className="form-control"
                                placeholder="No. Rekening"
                                defaultValue={this.state.accountNumber}
                                onBlur={this.handleChangeAccountNumber}
                            />
                        </div>
                        {
                            (this.state.errors.accountNumber) &&
                            <div className="col-12 pl-5 text-right">
                                <small className="form-text text-muted">
                                    {this.state.errors.accountNumber}
                                </small>
                            </div>
                        }
                    </div>
                    <div className={this.state.errors.bankName !== '' ? "row pt-2 pb-2 border-top border-bottom has-error" : "row pt-2 pb-2 border-top border-bottom"}>
                        <div className='col-6'>Transfer dari Bank</div>
                        <div className='col-6'>
                            <input type="text"
                                className="form-control"
                                placeholder="Nama Bank"
                                defaultValue={this.state.bankName}
                                onBlur={this.handleChangeBankName}
                            />
                        </div>
                        {
                            (this.state.errors.bankName) &&
                            <div className="col-12 pl-5 text-right">
                                <small className="form-text text-muted">
                                    {this.state.errors.bankName}
                                </small>
                            </div>
                        }
                    </div>
                    <div className="row mt-4 mb-2">
                        <HozaruButton type="submit" className="btn btn-primary margin-center width-95percent" state={this.state.buttonState}>Kirimkan</HozaruButton>
                    </div>
                </div>
            </form>
        );
    }
};

export default PaymentConfirmation;
