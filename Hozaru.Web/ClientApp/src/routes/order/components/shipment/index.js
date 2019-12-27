import React, { Component } from 'react';
import PropTypes from 'prop-types';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faShippingFast } from '@fortawesome/free-solid-svg-icons';
import { dateTimeFormat } from '../../../../utils/DateUtil';
import { withRouter } from 'react-router';

class OrderShipment extends Component {
    constructor() {
        super();
        this.handleClickTracking = this.handleClickTracking.bind(this);
    }

    static propTypes = {
        order: PropTypes.object.isRequired
    };

    handleClickTracking(ev) {
        ev.preventDefault();
        this.props.history.push("/order/" + this.props.order.id + "/tracking");
    }

    render() {
        const { order } = this.props;
        return (
            <div className="row">
                <div className="col-1 font-16px mt-2">
                    <FontAwesomeIcon icon={faShippingFast} className="color-info" />
                </div>
                <div className="col-11 ml-minus-10px mt-2 mb-2 pr-0px">
                    <div>
                        <span className="font-16px font-weight-500">Status Pengiriman</span>
                        <div className="float-right">
                            <button type="button" className="btn btn-link color-info" onClick={this.handleClickTracking}>LACAK</button>
                        </div>
                    </div>
                    <div className="mt-5px color-info">{order.shipment.lastShipmentTracking.description}</div>
                    <div className="mt-3px font-12px">{order.shipment.expeditionService.fullName}: {order.shipment.airWayBill}</div>
                    <div className="font-12px">{dateTimeFormat(order.shipment.lastShipmentTracking.trackingDate)}</div>
                </div>
            </div>
        );
    }
}

export default withRouter(OrderShipment);