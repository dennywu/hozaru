import React, { Component } from 'react';

class Note extends Component {
    render() {
        return (
            <div className="container mt-2 mb-2 ">
                <div className="row">
                    <div className="col-12 pt-2 pb-2">
                        <div className="input-group">
                            <div className="input-group-prepend">
                                <span className="input-group-text">Pesan</span>
                            </div>
                            <textarea className="form-control font-12px" rows="1" placeholder="Silakan tinggalkan pesan (jika ada)" aria-label="Optional"></textarea>
                        </div>
                    </div>
                </div>
            </div>
        );
    }
}

export default Note;