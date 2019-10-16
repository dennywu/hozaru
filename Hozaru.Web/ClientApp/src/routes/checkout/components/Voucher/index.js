import React, { Component } from 'react';

class Voucher extends Component {
    render() {
        return (
            <div className="container section-voucher mt-2 mb-2" >
                <div className="row">
                    <div className="col-12 pt-2 pb-2">
                        <div className="input-group">
                            <div className="input-group-prepend">
                                <span className="input-group-text">
                                    <i className="fas fa-tags"></i>&nbsp; Voucher
                    </span>
                            </div>
                            <input className="form-control" placeholder="Gunakan / masukan kode voucher" />
                        </div>
                    </div>
                </div>
            </div >
        );
    }
}

export default Voucher;