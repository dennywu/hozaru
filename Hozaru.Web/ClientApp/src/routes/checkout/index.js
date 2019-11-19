import React, { Component } from 'react';
import Receiver from './components/Receiver';
import ShoppingCart from './components/ShoppingCart';
import Shipping from './components/Shipping';
import Note from './components/Note';
import SubTotal from './components/SubTotal';
import Payment from './components/Payment';
import Summary from './components/Summary';
import { connect } from 'react-redux';
import { validateCustomerInfo } from '../../services/customer/actions';
import { resetShoppingCart } from '../../services/shopping-cart/actions';
import axios from 'axios';
import HozaruButton from '../../components/hozaru-button';

class Checkout extends Component {
    constructor(props) {
        super();
        this.handleSubmit = this.handleSubmit.bind(this);
        this.fetchToServer = this.fetchToServer.bind(this);
        this.state = {
            buttonState: ''
        };
    }

    componentDidMount() {
        const { shoppingCart } = this.props;
        if (shoppingCart.items.length === 0) {
            this.props.history.push('/');
        }
    }

    handleSubmit(event) {
        event.preventDefault();
        const { customer, shoppingCart } = this.props;

        this.props.validateCustomerInfo();
        if (customer.errors.hasError) {
            window.scrollTo(0, 0);
            return;
        }

        if (shoppingCart.freight.expeditionCode === '') {
            alert("Silahkan pilih Opsi Pengiriman.");
            return;
        }

        if (shoppingCart.paymentType === '') {
            alert("Silahkan pilih metode pembayaran.");
            return;
        }

        if (shoppingCart.summary.totalQuantity === 0) {
            alert("Tidak ada produk yang Anda pilih.");
            return;
        }

        this.fetchToServer();
    }

    async fetchToServer() {
        this.setState({ buttonState: 'loading' });

        const { customer, shoppingCart } = this.props;
        let data = {
            name: customer.name,
            whatsapp: customer.whatsapp,
            email: customer.email,
            cityCode: customer.city.value,
            districtCode: customer.districts.value,
            address: customer.address,
            paymentTypeCode: shoppingCart.paymentType,
            expeditionCode: shoppingCart.freight.expeditionCode,
            note: shoppingCart.note,
            items: []
        };

        shoppingCart.items.forEach(i => {
            var shoppingCartItem = { productId: i.product.id, quantity: i.quantity, note: i.note };
            data.items.push(shoppingCartItem);
        });

        axios.post('/api/order', data)
            .then(res => {
                this.props.resetShoppingCart();
                this.props.history.push('/payment/' + res.data.id);
            })
            .finally(() => {
                this.setState({ buttonState: '' });
            });
    }

    render() {
        const { shoppingCart } = this.props;
        if (shoppingCart.items.length === 0) {
            return <></>;
        }
        return (
            <form onSubmit={this.handleSubmit}>
                <div className="container section-checkout">
                    <div className="row mb-3">
                        <div className="col-12">
                            <Receiver />
                        </div>
                    </div>
                    <ShoppingCart />
                </div>
                <Shipping />
                <Note />
                <hr className="mt-1 mb-1" />
                <SubTotal />
                <hr className="mt-1 mb-1" />
                {
                    //<Voucher />
                    //<hr className="mt-1 mb-1" />
                }
                <Payment />
                <hr className="mt-1 mb-1" />
                <Summary />
                <HozaruButton type="submit" className="btn btn-primary button-full" state={this.state.buttonState}>Bayar Sekarang</HozaruButton>
            </form>
        );
    }
}

const mapStateToProps = state => ({
    customer: state.customer,
    shoppingCart: state.shoppingCart
});

export default connect(mapStateToProps, { validateCustomerInfo, resetShoppingCart })(Checkout);