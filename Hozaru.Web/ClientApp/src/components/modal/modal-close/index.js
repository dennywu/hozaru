import React, { Component } from 'react';
import './ModalClose.css';

export class ModalClose extends Component {
    render() {
        return (
            <div className="modal-header modal-without-title">
                <button type="button" onClick={this.props.toggle} className="close" aria-label="Close">
                    <span aria-hidden="true">×</span>
                </button>
            </div>
        );
    }
}