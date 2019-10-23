import React, { Component } from 'react';
import PropTypes from 'prop-types';
import { connect } from 'react-redux';
import DialogShippingOption from './DialogShippingOption';
import { default as NumberFormat } from 'react-number-format';
import { changeFreight } from '../../../../services/shopping-cart/actions';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faChevronRight } from '@fortawesome/free-solid-svg-icons';

class Shipping extends Component {
    constructor(props) {
        super(props);
        this.showDialogShippingOption = this.showDialogShippingOption.bind(this);
        this.closeDialogShippingOption = this.closeDialogShippingOption.bind(this);
        this.changeFreightOption = this.changeFreightOption.bind(this);
        this.updateFreight = this.updateFreight.bind(this);
        this.resetShippingRate = this.resetShippingRate.bind(this);

        this.state = {
            freights: [],
            selectedExpeditionCode: props.expeditionCode,
            showDialogShippingOption: false
        };
    }

    static propTypes = {
        customer: PropTypes.object.isRequired
    };

    componentDidMount() {
        this.populateFreight();
    }

    componentWillReceiveProps(nextProps) {
        if (this.props.customerToChange && this.props.customerToChange.districts !== nextProps.customerToChange.districts) {
            if (nextProps.customerToChange.districts === null) {
                this.resetShippingRate();
            } else {
                this.populateFreight();
            }

        }
        else if (this.props.quantityToChange && this.props.quantityToChange !== nextProps.quantityToChange) {
            this.populateFreight();
        }
        else if (this.props.productToRemove && this.props.productToRemove !== nextProps.productToRemove) {
            this.populateFreight();
        }
    }

    async populateFreight() {
        const { customer, shoppingCart } = this.props;
        if (customer.districts == null)
            return;

        var data = {
            city: customer.city.value,
            districts: customer.districts.value,
            items: []
        };
        shoppingCart.items.forEach(i => {
            var shoppingCartItem = { productId: i.product.id, quantity: i.quantity };
            data.items.push(shoppingCartItem);
        });

        const response = await fetch('/api/freight', {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(data)
        });
        const responseData = await response.json();
        this.setState({
            freights: responseData
        });

        if (responseData.length === 0) {
            this.resetShippingRate();
            return;
        }

        var selectedExpeditionCode = this.state.selectedExpeditionCode;
        if (selectedExpeditionCode === '') {
            this.setState({
                selectedExpeditionCode: responseData[0].expeditionCode
            });
        }

        if (selectedExpeditionCode !== '' && responseData.find(i => i.expeditionCode === selectedExpeditionCode) == null) {
            this.setState({
                selectedExpeditionCode: responseData[0].expeditionCode
            });
        }

        this.updateFreight();
    }

    updateFreight() {
        const selectedFreight = this.state.freights.find(i => i.expeditionCode === this.state.selectedExpeditionCode);
        this.props.changeFreight(selectedFreight.expeditionCode, selectedFreight.rate, selectedFreight.totalWeight);
    }

    resetShippingRate() {
        this.props.changeFreight('', 0);
    }

    changeFreightOption(expeditionCode) {
        this.setState({
            selectedExpeditionCode: expeditionCode
        }, () => this.updateFreight());
    }

    showDialogShippingOption() {
        this.setState({ showDialogShippingOption: true });
    }

    closeDialogShippingOption() {
        this.setState({
            showDialogShippingOption: false
        });
    }

    render() {
        const { customer } = this.props;
        const selectedFreight = this.state.freights.find(i => i.expeditionCode === this.state.selectedExpeditionCode);
        const expeditionName = selectedFreight ? selectedFreight.expeditionName : '';
        const expeditionFullName = selectedFreight ? selectedFreight.expeditionFullName : '';
        const description = selectedFreight ? selectedFreight.description : '';
        const rate = selectedFreight ? selectedFreight.rate : '';

        let contents = customer.districts && this.state.freights.length !== 0 ?
            <div className="row" onClick={this.showDialogShippingOption}>
                <div className="col-5">
                    <div className="font-weight-bolder">{expeditionName}</div>
                    <div className="font-weight-normal">{expeditionFullName}</div>
                </div>
                <div className="col-7">
                    <div className="font-weight-bolder text-right">
                        <NumberFormat value={rate} displayType={'text'} thousandSeparator={true} prefix={' Rp '} />
                        <FontAwesomeIcon icon={faChevronRight} className="ml-2" />
                    </div>
                </div>
                <div className="col-12 font-weight-light font-11px">{description}</div>
                {
                    this.state.showDialogShippingOption &&
                    <DialogShippingOption
                        isOpenDialog={this.state.showDialogShippingOption}
                        toggleDialog={this.closeDialogShippingOption}
                        freights={this.state.freights}
                        selectedFreight={this.state.selectedExpeditionCode}
                        changeFreightOption={this.changeFreightOption}
                    />
                }
            </div>
            :
            <div className="row">
                <div className="col-12 mb-2 color-orange">Jasa pengiriman tidak tersedia. Silahkan pilih Kota dan Kecamatan Anda.</div>
            </div>;

        return (
            <div className="opsi-pengiriman container mt-3 mb-2 border-top border-bottom">
                <div className="row">
                    <div className="col-12 pt-2 pb-2">
                        <span className="title">Opsi Pengiriman</span>
                        <hr className="mt-1 mb-1" />
                    </div>
                </div>
                {contents}
            </div>
        );
    }
}

const mapStateToProps = state => ({
    customer: state.customer,
    shoppingCart: state.shoppingCart,
    customerToChange: state.customer.customerToChange,
    quantityToChange: state.shoppingCart.quantityToChange,
    productToRemove: state.shoppingCart.productToRemove,
    expeditionCode: state.shoppingCart.freight.expeditionCode
});

export default connect(mapStateToProps, { changeFreight })(Shipping);