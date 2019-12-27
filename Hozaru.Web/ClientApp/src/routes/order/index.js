import React, { Component } from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faFileInvoiceDollar, faDollarSign, faStore, faComments } from '@fortawesome/free-solid-svg-icons';
import { default as NumberFormat } from 'react-number-format';
import { dateTimeFormat } from '../../utils/DateUtil';
import { StatusOrder } from './OrderStatus';
import OrderStatus from './components/status';
import OrderReceiver from './components/receiver';
import OrderShipment from './components/shipment';
import ListOrderItem from './components/list-order-item';
import Toolbar from './components/toolbar';
import Loading from '../../components/loading';
import axios from 'axios';
import { connect } from 'react-redux';

class Order extends Component {
    constructor() {
        super();
        this.renderContent = this.renderContent.bind(this);
        this.handleClickToHomepage = this.handleClickToHomepage.bind(this);
        this.handleClickContactTenant = this.handleClickContactTenant.bind(this);

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
            this.populateOrder();
        });
    }

    async populateOrder() {
        axios.get('/api/orders/' + this.state.orderId)
            .then(res => {
                const order = res.data;
                this.setState({ order: order, loading: false });
            });
    }

    handleClickToHomepage() {
        alert('Untuk melihat pesanan Anda, silahkan cek whatsapp Anda.');
        this.props.history.push('/');
    }

    handleClickContactTenant(ev) {
        ev.preventDefault();
        window.open(this.props.tenant.whatsappUrl);
    }

    renderContent(order) {
        return (
            <>
                <OrderStatus order={order} />
                <div className="container">
                    <OrderReceiver order={order} />
                    <hr className="mt-1 mb-1" />

                    {
                        (order.statusText == StatusOrder.SHIPPING || order.statusText == StatusOrder.DONE) ?
                            <>
                                <OrderShipment order={order} />
                                <hr className="mt-1 mb-1" />
                            </>
                            :
                            <></>
                    }


                    <div className="row">
                        <div className="col-1 font-16px mt-2">
                            <FontAwesomeIcon icon={faFileInvoiceDollar} className="color-info" />
                        </div>
                        <div className="col-11 ml-minus-10px mt-2 mb-2">
                            <div className="font-16px font-weight-500">Informasi Pembayaran</div>
                            <div className="row">
                                <div className="col-6">Total Pesanan</div>
                                <div className="col-6 text-right">
                                    <NumberFormat value={order.summary.subTotal} displayType={'text'} thousandSeparator={true} prefix={'Rp '} />
                                </div>
                            </div>
                            <div className="row mt-1">
                                <div className="col-6 font-weight-500">Ongkos Kirim</div>
                                <div className="col-6 text-right">
                                    <NumberFormat value={order.summary.totalShippingCost} displayType={'text'} thousandSeparator={true} prefix={'Rp '} />
                                </div>
                            </div>
                            <div className="row mt-1">
                                <div className="col-6 font-weight-500">Jumlah Harus Dibayar</div>
                                <div className="col-6 color-orange text-right">
                                    <NumberFormat value={order.summary.netTotal} displayType={'text'} thousandSeparator={true} prefix={'Rp '} />
                                </div>
                            </div>
                        </div>
                    </div>

                    <hr className="mt-1 mb-1" />

                    <div className="row">
                        <div className="col-12 mt-2 mb-2">
                            <div className="font-16px font-weight-500">Metode Pembayaran</div>
                            <div>
                                <FontAwesomeIcon icon={faDollarSign} className="color-orange" /> {order.payment.paymentMethod.name}
                            </div>
                        </div>
                    </div>

                    <hr className="mt-1 mb-1" />

                    <div className="row">
                        <div className="col-12">
                            <div className="font-16px font-weight-500">Produk Pesanan</div>
                        </div>
                    </div>
                    <ListOrderItem order={order} />

                    <hr className="mt-1 mb-1" />

                    <div className="row">
                        <div className="col-12 mt-2 mb-2">
                            <div className="font-16px font-weight-500">Ongkos Kirim</div>
                            <div>
                                {order.shipment.expeditionService.fullName} <NumberFormat value={order.summary.totalShippingCost} displayType={'text'} thousandSeparator={true} prefix={'Rp '} className='color-orange' />
                            </div>
                        </div>
                    </div>

                    <hr className="mt-1 mb-1" />

                    <div className="row mt-2">
                        <div className="col-5">No. Pesanan</div>
                        <div className="col-7 text-right">{order.orderNumber || '-'}</div>
                    </div>
                    <div className="row">
                        <div className="col-5">Waktu Pesanan</div>
                        <div className="col-7 text-right">{dateTimeFormat(order.transactionDate)}</div>
                    </div>
                    {
                        order.statusText !== StatusOrder.DRAFT &&
                        <div className="row">
                            <div className="col-5">Waktu Pembayaran</div>
                            <div className="col-7 text-right">{dateTimeFormat(order.payment.lastPayment.paymentDate)}</div>
                        </div>
                    }
                    {
                        (order.statusText === StatusOrder.SHIPPING || order.statusText === StatusOrder.DONE) ?
                            <div className="row">
                                <div className="col-5">Waktu Pengiriman</div>
                                <div className="col-7 text-right">{dateTimeFormat(order.shipment.shipmentDate)}</div>
                            </div>
                            :
                            <></>
                    }
                    {
                        order.statusText === StatusOrder.DONE &&
                        <div className="row">
                            <div className="col-5">Waktu Pesanan Selesai</div>
                            <div className="col-7 text-right">{dateTimeFormat(order.shipment.proofOfDeliveryDate)}</div>
                        </div>
                    }

                    <hr className="mt-2 mb-1" />

                    <div className="row mt-3 mb-3">
                        <div className="col-6">
                            <button className="btn btn-transparent width-100percent" onClick={this.handleClickContactTenant}>
                                <FontAwesomeIcon icon={faComments} /> Hubungi Penjual
                            </button>
                        </div>
                        <div className="col-6">
                            <button className="btn btn-transparent width-100percent" onClick={this.handleClickToHomepage}>
                                <FontAwesomeIcon icon={faStore} /> Kunjungi Toko
                            </button>
                        </div>
                    </div>

                    <Toolbar order={order} />
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


const mapStateToProps = state => ({
    tenant: state.tenant
});

export default connect(mapStateToProps, {})(Order);
