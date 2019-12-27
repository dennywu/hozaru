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
            selectedPaymentMethod: props.selectedPaymentMethod
        };
    }

    handleClickPaymentRow(paymentMethod) {
        this.setState({
            selectedPaymentMethod: paymentMethod.code
        });
    }

    handleSubmit(event) {
        event.preventDefault();
        event.stopPropagation();
        this.props.changePaymentMethodOption(this.state.selectedPaymentMethod);
        this.props.toggleDialog();
    }

    render() {
        const { paymentMethods } = this.props;
        var paymentMethodOptionElements = [];
        paymentMethods.forEach(paymentMethod => {
            paymentMethodOptionElements.push(
                <div key={paymentMethod.code} >
                    <div className="row" onClick={this.handleClickPaymentRow.bind(this, paymentMethod)}>
                        <div className="col-12 custom-control custom-radio">
                            <input type="radio"
                                className="custom-control-input"
                                value={this.state.selectedPaymentMethod}
                                checked={this.state.selectedPaymentMethod === paymentMethod.code}
                                onChange={this.handleClickPaymentRow}
                            />
                            <label className="custom-control-label">
                                <div className="row">
                                    <div className="col-3 pl-30px">
                                        <img
                                            className='logobank'
                                            alt={paymentMethod.bankName}
                                            srcSet={paymentMethod.imageUrl}
                                        />
                                    </div>
                                    <div className="col-9">
                                        <div>{paymentMethod.name}</div>
                                        <div className="font-weight-light font-12px">
                                            Hanya menerima dari Bank {paymentMethod.bankName}
                                        </div>
                                        <div className="font-weight-light font-12px">
                                            {paymentMethod.isManualConfirmation && "Perlu upload bukti transfer"}
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
                style={{ overflowY: "initial !important" }}
            >
                <ModalHeader toggle={this.props.toggleDialog}>Pilih Metode Pembayaran</ModalHeader>
                <form onSubmit={this.handleSubmit}>
                    <ModalBody style={{ maxHeight: "275px", overflowY: "auto" }}>
                        <div className="container">
                            <div className="form-group">

                                {paymentMethodOptionElements}

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