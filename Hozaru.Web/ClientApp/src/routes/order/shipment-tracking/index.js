import React, { Component } from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faShippingFast } from '@fortawesome/free-solid-svg-icons';
import { dateTimeFormat } from '../../../utils/DateUtil';
import Loading from '../../../components/loading';
import axios from 'axios';

class ShipmentTracking extends Component {
    constructor() {
        super();
        this.renderContent = this.renderContent.bind(this);
        this.populateDetailShipmentTracking = this.populateDetailShipmentTracking.bind(this);

        this.state = {
            orderId: '',
            loading: true,
            order: {}
        };
    }

    componentDidMount() {
        let orderId = this.props.match.params.id;
        this.setState({
            orderId: orderId
        }, () => {
            this.populateDetailShipmentTracking();
        });
    }

    async populateDetailShipmentTracking() {
        axios.get('/api/orders/' + this.state.orderId + '/tracking')
            .then(res => {
                const order = res.data;
                this.setState({ order: order, loading: false });
            });
    }

    renderContent(order) {
        return (
            <>
                <div className="container background-info mt-minus-5px">
                    <div className="row">
                        <div className="col-1 color-white font-16px mt-2">
                            <FontAwesomeIcon icon={faShippingFast} />
                        </div>
                        <div className="col-11 color-white mt-2 mb-2">
                            <div className="font-16px font-weight-500">{order.expeditionService.fullName}</div>
                            <span>
                                No. Resi: {order.airWayBill}
                            </span>
                        </div>
                    </div>
                </div>

                <div className="container mt-3">
                    <div className="row border-top border-bottom p-2">
                        <div className="col-12 font-16px font-weight-500">
                            Lacak Pesanan
                        </div>
                    </div>

                    {
                        order.shipmentTrackings.map((tracking, index) => (
                            <div key={"shipmentTracking"+index}>
                                <div className={(index == 0) ? "row p-3 color-info" : "row p-3"}>
                                    <div className="col-12">
                                        {tracking.description}
                                        <div className="font-12px">{dateTimeFormat(tracking.trackingDate)}</div>
                                    </div>
                                </div>
                                <hr className="mt-1 mb-1" />
                            </div>
                        ))
                    }

                </div>
            </>
        );
    }

    render() {
        let contents = this.state.loading
            ? <Loading />
            : this.renderContent(this.state.order);

        return (
            <>
                {contents}
            </>
        );
    }
}


export default ShipmentTracking;
