import React, { Component } from 'react';
import PropTypes from 'prop-types';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faFileInvoice } from '@fortawesome/free-solid-svg-icons';
import { dateTimeFormat } from '../../../../utils/DateUtil';
import { default as NumberFormat } from 'react-number-format';
import { StatusOrder } from "../../OrderStatus";

class OrderStatus extends Component {
    static propTypes = {
        order: PropTypes.object.isRequired
    };

    renderStatus(order) {
        let content = '';
        switch (order.statusText) {
            case StatusOrder.DRAFT:
                content =
                    <>
                        <div className="font-16px font-weight-500">Belum Bayar</div>
                        <span>
                            Mohon melakukan pembayaran sebelum {dateTimeFormat(order.dueDateConfirmation)} melalui {order.payment.paymentMethod.name}.
                            Bila tidak, pesanan ini akan dibatalkan secara otomatis.
                        </span>
                        <div className="font-12px">Waktu Pemesanan: {dateTimeFormat(order.transactionDate)}</div>
                    </>;
                break;
            case StatusOrder.WAITINGFORPAYMENT:
                content =
                    <>
                        <div className="font-16px font-weight-500">Proses Verifikasi Pembayaran</div>
                        <span>
                            Kami sudah menerima Konfirmasi Pembayaran Anda sebesar <NumberFormat value={order.summary.netTotal} displayType={'text'} thousandSeparator={true} prefix={'Rp '} />.
                            Kami akan menginformasikan melalui Whatsapp {order.customer.whatsappNumber} setelah Orderan Anda terverifikasi.
                        </span>
                        <div className="font-12px">Waktu Pembayaran: {dateTimeFormat(order.payment.lastPayment.paymentDate)}</div>
                    </>;
                break;
            case StatusOrder.PAYMENTREJECTED:
                content =
                    <>
                        <div className="font-16px font-weight-500">Pembayaran Gagal</div>
                        <span>
                            Kami sudah menerima Konfirmasi Pembayaran Anda.
                            Tetapi kami tidak bisa verifikasi Pembayaran Anda dikarenakan Pembayaran Anda tidak diterima di rekening kami.
                            Silahkan lakukan Pembayaran dan upload Bukti Pembayaran Anda.
                        </span>
                        <div className="font-12px">Waktu Pembayaran: {dateTimeFormat(order.payment.lastPaymentDate)}</div>
                    </>;
                break;
            case StatusOrder.PACKAGING:
                content =
                    <>
                        <div className="font-16px font-weight-500">Sedang Dikemas</div>
                        <span>
                            Orderan Anda sedang dikemas.
                            Kami akan menginformasikan melalui Whatsapp {order.customer.whatsappNumber} setelah Orderan Anda dikirim.
                        </span>
                    </>;
                break;
            case StatusOrder.SHIPPING:
                content =
                    <>
                        <div className="font-16px font-weight-500">Sedang dalam Pengiriman</div>
                        <span>
                            Orderan Anda sedang dalam pengiriman oleh Kurir. Lacak orderan Anda dengan klik LACAK dibawah ini.
                        </span>
                    <div className="font-12px">Waktu Pengiriman: {dateTimeFormat(order.shipment.shipmentDate)}</div>
                    <div className="font-12px">{order.shipment.estimatedTimeDeliverySentence}</div>
                    </>;
                break;
            case StatusOrder.DONE:
                content =
                    <>
                        <div className="font-16px font-weight-500">Selesai</div>
                        <span>
                            Pesanan sudah selesai.
                        </span>
                    <div className="font-12px">Waktu Pesanan Selesai: {dateTimeFormat(order.shipment.proofOfDeliveryDate)}</div>
                    </>;
                break;
            case StatusOrder.VOID:
                content =
                    <>
                        <div className="font-16px font-weight-500">Dibatalkan</div>
                        <span>
                            Pesanan sudah dibatalkan.
                        </span>
                    </>;
                break;
            default:
                content = <></>
        }
        return content;
    }

    render() {
        const { order } = this.props;
        let content = this.renderStatus(order);
        return (
            <div className="container background-info mt-minus-5px">
                <div className="row">
                    <div className="col-1 color-white font-16px mt-2">
                        <FontAwesomeIcon icon={faFileInvoice} />
                    </div>
                    <div className="col-11 ml-minus-10px color-white mt-2 mb-2">
                        {content}
                    </div>
                </div>
            </div>
        );
    }
}

export default OrderStatus;