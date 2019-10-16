import React, { Component } from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faDollarSign } from '@fortawesome/free-solid-svg-icons';

class Payment extends Component {
    render() {
        return (
            <div className="container section-voucher mt-3 mb-3">
                <div className="row">
                    <div className="col-6">
                        <span className="font-weight-normal color-orange">
                            <FontAwesomeIcon icon={faDollarSign} /> Metode Pembayaran
                        </span>
                    </div>
                    <div className="col-6 text-right">
                        <span className="font-weight-bolder">Transfer Bank BCA</span>
                        <span> > </span>
                    </div>
                </div>
            </div>
        );
    }
}

export default Payment;