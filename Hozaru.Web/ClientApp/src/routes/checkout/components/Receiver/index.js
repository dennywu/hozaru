import React, { Component } from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faUser, faEnvelope, faGlobeAsia, faCity, faAt } from '@fortawesome/free-solid-svg-icons';
import { faWhatsapp } from '@fortawesome/free-brands-svg-icons';
import PropTypes from 'prop-types';
import { connect } from 'react-redux';
import { changeCustomerInfo } from '../../../../services/customer/actions';
import AsyncSelect from 'react-select/async';

class Receiver extends Component {
    constructor(props) {
        super(props);
        this.handleChangeCustomerName = this.handleChangeCustomerName.bind(this);
        this.handleChangeWhatsapp = this.handleChangeWhatsapp.bind(this);
        this.handleChangeEmail = this.handleChangeEmail.bind(this);
        this.handleChangeAddress = this.handleChangeAddress.bind(this);
        this.loadCities = this.loadCities.bind(this);
        this.populateCities = this.populateCities.bind(this);
        this.loadDistrictses = this.loadDistrictses.bind(this);
        this.populateDistrictses = this.populateDistrictses.bind(this);
        this.handleChangeCity = this.handleChangeCity.bind(this);
        this.handleChangeDistrict = this.handleChangeDistrict.bind(this);

        this.state = {
            errorMessages: {
                name: '',
                whatsapp: '',
                email: '',
                address: '',
                city: '',
                districts: ''
            }
        };
    }

    static propTypes = {
        customer: PropTypes.object.isRequired
    };

    handleChangeCustomerName(event) {
        this.props.customer.name = event.target.value;
        this.props.changeCustomerInfo(this.props.customer);
    }

    handleChangeWhatsapp(event) {
        this.props.customer.whatsapp = event.target.value;
        this.props.changeCustomerInfo(this.props.customer);
    }

    handleChangeEmail(event) {
        this.props.customer.email = event.target.value;
        this.props.changeCustomerInfo(this.props.customer);
    }

    handleChangeAddress(event) {
        this.props.customer.address = event.target.value;
        this.props.changeCustomerInfo(this.props.customer);
    }

    handleChangeCity(city) {
        this.props.customer.city = city;
        this.props.customer.districts = null;
        this.props.changeCustomerInfo(this.props.customer);
    }

    handleChangeDistrict(districts) {
        this.props.customer.districts = districts;
        this.props.changeCustomerInfo(this.props.customer);
    }

    loadCities = inputValue =>
        new Promise(resolve => {
            resolve(this.populateCities(inputValue));
        });

    loadDistrictses = inputValue =>
        new Promise(resolve => {
            resolve(this.populateDistrictses(inputValue));
        });

    async populateCities(searchKey) {
        let response = await fetch('/api/city?searchKey=' + searchKey);
        let data = await response.json();
        var result = [];
        data.forEach(item => {
            result.push({ label: item.name, value: item.name });
        });
        return result;
    }

    async populateDistrictses(searchKey) {
        searchKey = searchKey || "";
        if (this.props.customer.city === undefined) {
            return;
        }

        let response = await fetch('/api/district?cityCode=' + this.props.customer.city.value + '&searchKey=' + searchKey);
        let data = await response.json();
        var result = [];
        data.forEach(item => {
            result.push({ label: item.name, value: item.name });
        });
        return result;
    }

    render() {
        return (
            <div className="card w-100 pt-3 pl-3 pr-3 mb-1">
                <h5 className="card-title">DATA PENERIMA</h5>
                <div className="card-body">
                    <div className="row">
                        <div className={(this.props.errors && this.props.errors.name.hasError) ? 'input-group mb-2 has-error' : 'input-group mb-2'}>
                            <div className="input-group-prepend">
                                <span className="input-group-text">
                                    <FontAwesomeIcon icon={faUser} />
                                </span>
                            </div>
                            <input type="text"
                                className="form-control"
                                placeholder="Nama Lengkap"
                                defaultValue={this.props.customer.name}
                                onBlur={this.handleChangeCustomerName}
                            />
                            {
                                (this.props.errors && this.props.errors.name.hasError) &&
                                <div className="col-12 pl-5">
                                    <small className="form-text text-muted">
                                        {
                                            this.props.errors && this.props.errors.name.message
                                        }
                                    </small>
                                </div>
                            }
                        </div>

                        <div className={(this.props.errors && this.props.errors.whatsapp.hasError) ? 'input-group mb-2 has-error' : 'input-group mb-2'}>
                            <div className="input-group-prepend">
                                <span className="input-group-text" id="basic-addon1">
                                    <FontAwesomeIcon icon={faWhatsapp} />
                                </span>
                            </div>
                            <input type="text"
                                className="form-control"
                                placeholder="No. WhatsApp"
                                defaultValue={this.props.customer.whatsapp}
                                onBlur={this.handleChangeWhatsapp}
                            />
                            {
                                (this.props.errors && this.props.errors.whatsapp.hasError) &&
                                <div className="col-12 pl-5">
                                    <small className="form-text text-muted">
                                        {this.props.errors && this.props.errors.whatsapp.message}
                                    </small>
                                </div>
                            }
                        </div>
                        <div className={(this.props.errors && this.props.errors.email.hasError) ? 'input-group mb-2 has-error' : 'input-group mb-2'}>
                            <div className="input-group-prepend">
                                <span className="input-group-text" id="basic-addon1">
                                    <FontAwesomeIcon icon={faEnvelope} />
                                </span>
                            </div>
                            <input type="email"
                                className="form-control"
                                placeholder="Email Aktif"
                                autoComplete="disabled"
                                defaultValue={this.props.customer.email}
                                onBlur={this.handleChangeEmail}
                            />
                            {
                                (this.props.errors && this.props.errors.email.hasError) &&
                                <div className="col-12 pl-5">
                                    <small className="form-text text-muted">
                                        {this.props.errors && this.props.errors.email.message}
                                    </small>
                                </div>
                            }
                        </div>
                        <div className={(this.props.errors && this.props.errors.city.hasError) ? 'input-group mb-2 has-error' : 'input-group mb-2'}>
                            <div className="input-group-prepend">
                                <span className="input-group-text" id="basic-addon1">
                                    <FontAwesomeIcon icon={faGlobeAsia} />
                                </span>
                            </div>
                            <AsyncSelect className="react-select"
                                placeholder="Kota"
                                defaultOptions
                                loadOptions={this.loadCities}
                                value={this.props.customer.city}
                                onChange={this.handleChangeCity}
                            />
                            {
                                (this.props.errors && this.props.errors.city.hasError) &&
                                <div className="col-12 pl-5">
                                    <small className="form-text text-muted">
                                        {this.props.errors && this.props.errors.city.message}
                                    </small>
                                </div>
                            }
                        </div>
                        <div className={(this.props.errors && this.props.errors.districts.hasError) ? 'input-group mb-2 has-error' : 'input-group mb-2'}>
                            <div className="input-group-prepend">
                                <span className="input-group-text" id="basic-addon1">
                                    <FontAwesomeIcon icon={faCity} />
                                </span>
                            </div>
                            <AsyncSelect className="react-select"
                                key={JSON.stringify(this.props.customer.city)}
                                placeholder="Kecamatan"
                                defaultOptions
                                value={this.props.customer.districts}
                                loadOptions={this.loadDistrictses}
                                onChange={this.handleChangeDistrict}
                            />
                            {
                                (this.props.errors && this.props.errors.districts.hasError) &&
                                <div className="col-12 pl-5">
                                    <small className="form-text text-muted">
                                        {this.props.errors && this.props.errors.districts.message}
                                    </small>
                                </div>
                            }
                        </div>
                        <div className={(this.props.errors && this.props.errors.address.hasError) ? 'input-group has-error' : 'input-group'}>
                            <div className="input-group-prepend">
                                <span className="input-group-text" id="basic-addon1">
                                    <FontAwesomeIcon icon={faAt} />
                                </span>
                            </div>
                            <textarea
                                className="form-control"
                                rows="2"
                                placeholder="Alamat Lengkap"
                                defaultValue={this.props.customer.address}
                                onBlur={this.handleChangeAddress}
                            >
                            </textarea>
                            {
                                (this.props.errors && this.props.errors.address.hasError) &&
                                <div className="col-12 pl-5">
                                    <small className="form-text text-muted">
                                        {this.props.errors && this.props.errors.address.message}
                                    </small>
                                </div>
                            }
                        </div>
                    </div>
                </div>
            </div>
        );
    }
}

const mapStateToProps = state => ({
    customer: state.customer,
    errors: state.customer.errors
});

export default connect(mapStateToProps, { changeCustomerInfo })(Receiver);