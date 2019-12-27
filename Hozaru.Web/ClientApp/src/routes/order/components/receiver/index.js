import React, { Component } from 'react';
import PropTypes from 'prop-types';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faMapMarkerAlt } from '@fortawesome/free-solid-svg-icons';

class OrderReceiver extends Component {
    static propTypes = {
        order: PropTypes.object.isRequired
    };

    render() {
        const { order } = this.props;
        return (
            <div className="row">
                <div className="col-1 font-16px mt-2">
                    <FontAwesomeIcon icon={faMapMarkerAlt} className="color-info" />
                </div>
                <div className="col-11 ml-minus-10px mt-2 mb-2">
                    <div className="font-16px font-weight-500">Alamat Pengiriman</div>
                    <div>{order.customer.customerName}</div>
                    <div>{order.customer.whatsapp}</div>
                    <div>{order.customer.address}</div>
                </div>
            </div>
        );
    }
}

export default OrderReceiver;