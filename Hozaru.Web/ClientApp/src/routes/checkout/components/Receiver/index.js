import React, { Component } from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faUser, faEnvelope, faGlobeAsia, faAt } from '@fortawesome/free-solid-svg-icons';
import { faWhatsapp } from '@fortawesome/free-brands-svg-icons';

class Receiver extends Component {
    render() {
        return (
            <div className="card w-100 pt-3 pl-3 pr-3 mb-1">
                <h5 className="card-title">DATA PENERIMA</h5>
                <div className="card-body">
                    <div className="row">
                        <div className="input-group mb-2">
                            <div className="input-group-prepend">
                                <span className="input-group-text" id="basic-addon1">
                                    <FontAwesomeIcon icon={faUser} />
                                </span>
                            </div>
                            <input type="text" className="form-control" placeholder="Nama Lengkap" aria-label="Username" aria-describedby="basic-addon1" />
                        </div>
                        <div className="input-group mb-2">
                            <div className="input-group-prepend">
                                <span className="input-group-text" id="basic-addon1">
                                    <FontAwesomeIcon icon={faWhatsapp} />
                                </span>
                            </div>
                            <input type="text" className="form-control" placeholder="No. WhatsApp" aria-label="Username" aria-describedby="basic-addon1" />
                        </div>
                        <div className="input-group mb-2">
                            <div className="input-group-prepend">
                                <span className="input-group-text" id="basic-addon1">
                                    <FontAwesomeIcon icon={faEnvelope} />
                                </span>
                            </div>
                            <input type="email" className="form-control" placeholder="Email Aktif" aria-label="Username" aria-describedby="basic-addon1" />
                        </div>
                        <div className="input-group mb-2">
                            <div className="input-group-prepend">
                                <span className="input-group-text" id="basic-addon1">
                                    <FontAwesomeIcon icon={faGlobeAsia} />
                                </span>
                            </div>
                            <input type="text" className="form-control" placeholder="Tujuan Pengiriman" aria-label="Username" aria-describedby="basic-addon1" />
                        </div>
                        <div className="input-group">
                            <div className="input-group-prepend">
                                <span className="input-group-text" id="basic-addon1">
                                    <FontAwesomeIcon icon={faAt} />
                                </span>
                            </div>
                            <textarea className="form-control" id="" rows="2" placeholder="Alamat Lengkap"></textarea>
                        </div>
                    </div>
                </div>
            </div>
        );
    }
}

export default Receiver;