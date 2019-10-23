import React, { Component } from 'react';
import { Modal, ModalHeader, ModalBody, ModalFooter } from 'reactstrap';
import { default as NumberFormat } from 'react-number-format';
import './style.css';


class DialogShippingOption extends Component {
    constructor(props) {
        super();
        this.clickFreightRow = this.clickFreightRow.bind(this);
        this.handleChangeFreight = this.handleChangeFreight.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);

        this.state = {
            selectedFreight: props.selectedFreight
        };
    }

    clickFreightRow(event) {
        var freight = event.currentTarget.getElementsByTagName("input")[0].value;
        this.setState({
            selectedFreight: freight
        });
    }

    handleChangeFreight(changeEvent) {
        var freight = changeEvent.target.value;
        this.setState({
            selectedFreight: freight
        });
    }

    handleSubmit(event) {
        event.preventDefault();
        this.props.changeFreightOption(this.state.selectedFreight);
        this.props.toggleDialog();
    }

    render() {
        const { freights } = this.props;
        var shippingOptionElements = [];
        freights.forEach(freight =>
            shippingOptionElements.push(
                <div key={freight.id} >
                    <div className="row" onClick={this.clickFreightRow}>
                        <div className="col-1">
                            <input type="radio" name="freight"
                                className="form-control"
                                value={freight.expeditionCode}
                                checked={this.state.selectedFreight === freight.expeditionCode}
                                onChange={this.handleChangeFreight}
                            />
                        </div>
                        <div className="col-10 pl-4">
                            <div>{freight.expeditionFullName}
                                <span className="color-orange">
                                    <NumberFormat value={freight.rate} displayType={'text'} thousandSeparator={true} prefix={' Rp '} />
                                </span>
                            </div>
                            <div className="font-weight-light font-12px">{freight.description}</div>
                        </div>
                    </div>
                    <hr className="mt-2 mb-2"></hr>
                </div>
            )
        );

        return (
            <Modal isOpen={this.props.isOpenDialog}
                fade={true}
                toggle={this.props.toggleDialog}
                backdrop={true}
                className="m-0 fixed-bottom"
            >
                <ModalHeader toggle={this.props.toggleDialog}>Opsi Pengiriman</ModalHeader>
                <form onSubmit={this.handleSubmit}>
                    <ModalBody>
                        <div className="container">
                            <div className="form-group">
                                {shippingOptionElements}
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

export default DialogShippingOption;