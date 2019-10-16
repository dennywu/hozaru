import React, { Component } from 'react';
import Receiver from './components/Receiver';
import ShoppingCart from './components/ShoppingCart';
import Shipping from './components/Shipping';
import Note from './components/Note';
import SubTotal from './components/SubTotal';
import Voucher from './components/Voucher';
import Payment from './components/Payment';
import Summary from './components/Summary';

class Checkout extends Component {

    render() {
        return (
            <>
                <div className="container section-checkout">
                    <div className="row mb-3">
                        <div className="col-12">
                            <Receiver />
                        </div>
                    </div>
                    <ShoppingCart />
                </div>
                <Shipping />
                <hr className="mt-1 mb-1" />
                <Note />
                <SubTotal />
                <Voucher />
                <Payment />
                <Summary />
                <div className="row">
                    <div className="col-12">
                        <button type="button" className="btn btn-primary button-full">Bayar Sekarang</button>
                    </div>
                </div>
            </>
        );
    }
}

export default Checkout;