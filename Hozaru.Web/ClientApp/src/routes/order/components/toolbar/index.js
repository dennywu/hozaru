import React, { Component } from 'react';
import PropTypes from 'prop-types';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faDollarSign } from '@fortawesome/free-solid-svg-icons';

class Toolbar extends Component {
    constructor() {
        super();
        this.handleClickToPaymentPage = this.handleClickToPaymentPage.bind(this);
        this.renderToolbar = this.renderToolbar.bind(this);
    }

    static propTypes = {
        order: PropTypes.object.isRequired
    };

    renderToolbar(order) {
        let content = '';
        switch (order.statusText) {
            case "DRAFT":
                content =
                    <>
                    <div className="col-12">
                        <button className="btn btn-primary width-100percent" onClick={this.handleClickToPaymentPage}>
                            <FontAwesomeIcon icon={faDollarSign} /> Bayar Sekarang
                        </button>
                    </div>
                    </>;
                break;
            case "REVIEW":
                content =
                    <>
                        
                    </>;
                break;
            case "PACKAGING":
                content =
                    <>
                        
                    </>;
                break;
            case "SHIPPING":
                content =
                    <>
                        
                    </>;
                break;
            case "DONE":
                content =
                    <>
                        
                    </>;
                break;
            default:
                content = <></>
        }
        return content;
    }

    handleClickToPaymentPage(event) {
        event.preventDefault();
        window.location = '/payment/' + this.props.order.id;
    }

    render() {
        const { order } = this.props;
        let content = this.renderToolbar(order);
        return (
            <div className="row mb-3">
                {content}
            </div>
        );
    }
}

export default Toolbar;