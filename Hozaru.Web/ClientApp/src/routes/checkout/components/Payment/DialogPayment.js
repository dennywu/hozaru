import React, { Component } from 'react';
import { Modal, ModalHeader, ModalBody, ModalFooter } from 'reactstrap';
import './style.css';
import "bootstrap/dist/css/bootstrap.css";

class DialogPayment extends Component {
    constructor(props) {
        super(props);
        this.handleSubmit = this.handleSubmit.bind(this);
        this.handleClickPaymentRow = this.handleClickPaymentRow.bind(this);

        this.state = {
            selectedPaymentType: props.selectedPaymentType
        };
    }

    handleClickPaymentRow(paymentType) {
        this.setState({
            selectedPaymentType: paymentType.code
        });
    }

    handleSubmit(event) {
        event.preventDefault();
        event.stopPropagation();
        this.props.changePaymentTypeOption(this.state.selectedPaymentType);
        this.props.toggleDialog();
    }

    render() {
        const { paymentTypes } = this.props;
        var paymentTypeOptionElements = [];
        paymentTypes.forEach(paymentType => {
            paymentTypeOptionElements.push(
                <div key={paymentType.code} >
                    <div className="row" onClick={this.handleClickPaymentRow.bind(this, paymentType)}>
                        <div className="col-12 custom-control custom-radio">
                            <input type="radio"
                                className="custom-control-input"
                                value={this.state.selectedPaymentType}
                                checked={this.state.selectedPaymentType === paymentType.code}
                                onChange={this.handleClickPaymentRow}
                            />
                            <label className="custom-control-label">
                                <div className="row">
                                    <div className="col-3 pl-30px">
                                        <img
                                            className='logobank'
                                            alt={paymentType.bankName}
                                            srcSet={paymentType.imageUrl}
                                        />
                                    </div>
                                    <div className="col-9">
                                        <div>{paymentType.name}</div>
                                        <div className="font-weight-light font-12px">
                                            Hanya menerima dari Bank {paymentType.bankName}
                                        </div>
                                        <div className="font-weight-light font-12px">
                                            {paymentType.isManualConfirmation && "Perlu upload bukti transfer"}
                                        </div>
                                    </div>
                                </div>
                            </label>
                        </div>
                    </div>
                    <hr className="mt-2 mb-2"></hr>
                </div>
            )
        });

        return (
            <Modal isOpen={this.props.isOpenDialog}
                fade={true}
                toggle={this.props.toggleDialog}
                backdrop={true}
                className="m-0 fixed-bottom"
            >
                <ModalHeader toggle={this.props.toggleDialog}>Metode Pembayaran</ModalHeader>
                <form onSubmit={this.handleSubmit}>
                    <ModalBody>
                        <div className="container">
                            <div className="form-group">

                                {paymentTypeOptionElements}

                            </div>
                        </div>
                    </ModalBody>
                    <ModalFooter className='p-0'>
                        <button type="submit" className="btn btn-primary button-full">Konfirmasi</button>
                    </ModalFooter>
                </form >
            </Modal >
        );
    }
}

export default DialogPayment;