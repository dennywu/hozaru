import React, { Component } from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faDollarSign, faChevronRight } from '@fortawesome/free-solid-svg-icons';
import DialogPayment from './DialogPayment';
import { connect } from 'react-redux';
import { changePaymentType } from '../../../../services/shopping-cart/actions';
import './style.css';

class Payment extends Component {
    constructor() {
        super();
        this.showDialogPayment = this.showDialogPayment.bind(this);
        this.closeDialogPayment = this.closeDialogPayment.bind(this);
        this.changePaymentOption = this.changePaymentOption.bind(this);

        this.state = {
            showDialogPayment: false,
            paymentTypes: []
        };
    }

    componentDidMount() {
        this.populatePaymentTypes();
    }

    showDialogPayment() {
        this.setState({ showDialogPayment: true });
    }

    closeDialogPayment() {
        this.setState({ showDialogPayment: false });
    }

    changePaymentOption(paymentTypeCode) {
        this.props.changePaymentType(paymentTypeCode);
    }

    async populatePaymentTypes() {
        const response = await fetch('/api/paymenttype', {
            method: 'GET',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            }
        });
        const responseData = await response.json();
        this.setState({
            paymentTypes: responseData
        });
    }

    render() {
        const paymentType = this.state.paymentTypes.find(i => i.code === this.props.selectedPaymentType);
        var paymentTypeElement = paymentType ?
            <span className="font-weight-bold">{paymentType.name}</span>
            : <span className="font-weight-normal color-orange">Pilih Metode Pembayaran</span>;

        return (
            <div className="container section-voucher mt-3 mb-3" onClick={this.showDialogPayment}>
                <div className="row">
                    <div className="col-6">
                        <span className="font-weight-normal">
                            <FontAwesomeIcon icon={faDollarSign} className="color-orange" /> Metode Pembayaran
                        </span>
                    </div>
                    <div className="col-6 text-right pr-2em">
                        {paymentTypeElement}
                    </div>

                    <div className="bank-arrow">
                        <FontAwesomeIcon icon={faChevronRight}/>
                    </div>
                </div>
                {
                    this.state.showDialogPayment &&
                    <DialogPayment
                        isOpenDialog={this.state.showDialogPayment}
                        toggleDialog={this.closeDialogPayment}
                        selectedPaymentType={this.props.selectedPaymentType}
                        paymentTypes={this.state.paymentTypes}
                        changePaymentTypeOption={this.changePaymentOption}
                    />
                }
            </div>
        );
    }
}


const mapStateToProps = state => ({
    selectedPaymentType: state.shoppingCart.paymentType
});

export default connect(mapStateToProps, { changePaymentType })(Payment);