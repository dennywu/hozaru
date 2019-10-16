import React, { Component } from 'react';

class Shipping extends Component {
    render() {
        return (
            <div className="opsi-pengiriman container mt-3 mb-2 border-top border-bottom">
                <div className="row">
                    <div className="col-12 pt-2 pb-2">
                        <span className="title">Opsi Pengiriman</span>
                        <hr className="mt-1 mb-1" />
                    </div>
                </div>
                <div className="row">
                    <div className="col-8">
                        <div className="font-weight-bolder">Reguler</div>
                        <div className="font-weight-normal">JNE REG</div>
                        <div className="font-weight-light font-12px">Akan diterima pada tanggal 12 Okt - 16 Okt</div>
                    </div>
                    <div className="col-4">
                        <div>&nbsp;</div>
                        <div className="font-weight-bolder text-right">
                            Rp 34.000
                <span className="font-weight-normal"> > </span>
                        </div>
                        <div>&nbsp;</div>
                    </div>
                </div>
            </div>
        );
    }
}

export default Shipping;