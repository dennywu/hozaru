import React, { Component } from "react";
import Notice from "../../../partials/content/Notice";
import PropTypes from "prop-types";
import { getStatusIndonesia } from "../status-order";

class StatusOrder extends Component {
    static propTypes = {
        order: PropTypes.object.isRequired
    };

    render() {
        const { order } = this.props;
        return (
            <Notice icon="flaticon-warning kt-font-primary">
                <div className="font-weight-bold">{getStatusIndonesia(order.statusText)}</div>
            </Notice>
        );
    }
}

export default StatusOrder;