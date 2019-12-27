import React, { Component } from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faDollarSign, faChevronRight } from '@fortawesome/free-solid-svg-icons';
import DialogPayment from './DialogPayment';
import { connect } from 'react-redux';
import { changePaymentMethod } from '../../../../services/shopping-cart/actions';
import './style.css';
import axios from 'axios';

class Payment extends Component {
    constructor() {
        super();
        this.showDialogPayment = this.showDialogPayment.bind(this);
        this.closeDialogPayment = this.closeDialogPayment.bind(this);
        this.changePaymentOption = this.changePaymentOption.bind(this);

        this.state = {
            showDialogPayment: false,
            paymentMethods: []
        };
    }

    componentDidMount() {
        this.populatePaymentMethods();
    }

    showDialogPayment() {
        this.setState({ showDialogPayment: true });
    }

    closeDialogPayment() {
        this.setState({ showDialogPayment: false });
    }

    changePaymentOption(paymentMethodCode) {
        this.props.changePaymentMethod(paymentMethodCode);
    }

    async populatePaymentMethods() {
        axios.get('/api/paymentmethods')
            .then(res => {
                var data = res.data;
                this.setState({
                    paymentMethods: data
                });
            });
    }

    render() {
        const paymentMethod = this.state.paymentMethods.find(i => i.code === this.props.selectedPaymentMethod);
        var paymentMethodElement = paymentMethod ?
            <span className="font-weight-bold">{paymentMethod.name} <FontAwesomeIcon className='btn-more color-black' icon={faChevronRight} /></span>
            : <span className="font-weight-normal color-orange">Pilih Metode Pembayaran <FontAwesomeIcon className='btn-more color-black' icon={faChevronRight} /></span>;

        return (
            <div className="container section-voucher mt-3 mb-3 cursor-pointer" onClick={this.showDialogPayment}>
                <div className="row">
                    <div className="col-6">
                        <span className="font-weight-normal">
                            <FontAwesomeIcon icon={faDollarSign} className="color-orange" /> Metode Pembayaran
                        </span>
                    </div>
                    <div className="col-6 text-right payment-type-value">
                        {paymentMethodElement}
                    </div>

                    <div className="bank-arrow">
                        <FontAwesomeIcon icon={faChevronRight} />
                    </div>
                </div>
                {
                    this.state.showDialogPayment &&
                    <DialogPayment
                        isOpenDialog={this.state.showDialogPayment}
                        toggleDialog={this.closeDialogPayment}
                        selectedPaymentMethod={this.props.selectedPaymentMethod}
                        paymentMethods={this.state.paymentMethods}
                        changePaymentMethodOption={this.changePaymentOption}
                    />
                }
            </div>
        );
    }
}


const mapStateToProps = state => ({
    selectedPaymentMethod: state.shoppingCart.paymentMethod
});

export default connect(mapStateToProps, { changePaymentMethod })(Payment);