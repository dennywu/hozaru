import React, { Component } from 'react';
import { Modal, ModalHeader, ModalBody, ModalFooter } from 'reactstrap';
import { Collapse, CardBody, Card, CardHeader } from 'reactstrap';
import { default as NumberFormat } from 'react-number-format';
import './style.css';
import "bootstrap/dist/css/bootstrap.css";
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faCaretUp, faCaretDown } from '@fortawesome/free-solid-svg-icons';

class DialogShippingOption extends Component {
    constructor(props) {
        super();
        this.clickFreightRow = this.clickFreightRow.bind(this);
        this.handleChangeFreight = this.handleChangeFreight.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
        this.collapseToogle = this.collapseToogle.bind(this);

        this.state = {
            selectedFreight: props.selectedFreight,
            collapse: ''
        };
    }

    componentDidMount() {
        var selectedGroupName = this.props.freights.find(i => i.expeditionServiceId === this.state.selectedFreight).expeditionServiceGroupName;
        this.setState({ collapse: selectedGroupName });
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

    collapseToogle(e) {
        let event = e.target.dataset.event;
        this.setState({ collapse: this.state.collapse === event ? null : event });
    }

    handleSubmit(event) {
        event.preventDefault();
        event.stopPropagation();
        this.props.changeFreightOption(this.state.selectedFreight);
        this.props.toggleDialog();
    }

    render() {
        const { collapse } = this.state;
        const { freights } = this.props;
        var shippingOptionElements = [];
        var index = 0;

        let group = freights.reduce((result, freight) => {
            result[freight.expeditionServiceGroupName] = [...result[freight.expeditionServiceGroupName] || [], freight];
            return result;
        }, {});
        var groups = Object.entries(group);

        groups.forEach(group => {
            var elOptions = [];
            var groupName = group[0];
            var expeditions = group[1];
            expeditions.forEach(freight => {
                var elOption = <div key={freight.expeditionServiceId} >
                    <div className="row" onClick={this.clickFreightRow}>
                        <div className="col-12 custom-control custom-radio">
                            <input type="radio" name="freight"
                                className="custom-control-input"
                                value={freight.expeditionServiceId}
                                checked={this.state.selectedFreight === freight.expeditionServiceId}
                                onChange={this.handleChangeFreight}
                            />
                            <label className="custom-control-label ml-2 pl-2">
                                <div>{freight.expeditionFullName}
                                    <span className="color-orange">
                                        <NumberFormat value={freight.cost} displayType={'text'} thousandSeparator={true} prefix={' Rp '} />
                                    </span>
                                </div>
                                <div className="font-weight-light font-12px">{freight.description}</div>
                            </label>
                        </div>
                    </div>
                    <hr className="mt-2 mb-2"></hr>
                </div>;
                elOptions.push(elOption);
            });

            shippingOptionElements.push(
                <Card key={index} style={{ border: "0px" }}>
                    <CardHeader className="expeditionServiceGroupHeader" style={{ backgroundColor: "transparent" }} onClick={this.collapseToogle} data-event={groupName}>
                        Pengiriman <b>{groupName}</b>
                        {
                            (collapse === groupName) ? <FontAwesomeIcon icon={faCaretUp} /> : <FontAwesomeIcon icon={faCaretDown} />
                        }
                    </CardHeader>
                    <Collapse isOpen={collapse === groupName}>
                        <CardBody className="pt-2 pb-2" style={{ borderBottom: "1px solid rgba(0, 0, 0, 0.125)" }}>
                            {elOptions}
                        </CardBody>
                    </Collapse>
                </Card>
            );
            index++;
        });

        return (
            <Modal isOpen={this.props.isOpenDialog}
                fade={true}
                toggle={this.props.toggleDialog}
                backdrop={true}
                className="m-0 fixed-bottom"
                style={{ overflowY: "initial !important" }}
            >
                <ModalHeader toggle={this.props.toggleDialog}>Pilih Jasa Pengiriman</ModalHeader>
                <form onSubmit={this.handleSubmit}>
                    <ModalBody className="p-0" style={{ height: "315px", overflowY: "auto" }}>
                        {shippingOptionElements}
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