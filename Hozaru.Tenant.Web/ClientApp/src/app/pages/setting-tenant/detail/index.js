import React, { Component } from "react";
import { Container, Row, Col, Form } from "react-bootstrap";
import { Portlet, PortletBody, PortletHeader, PortletHeaderToolbar } from "../../../partials/content/Portlet";
import { getTenant, editTenant } from "../../../crud/tenant.crud";
import { getCities } from "../../../crud/city.crud";
import { getDistrictses } from "../../../crud/districts.crud";
import AsyncSelect from 'react-select/async';
import HozaruButton from "../../../partials/layout/hozaru-button";
import Dropzone from "../../../partials/content/dropzone";
import Alert from "../../../partials/layout/Alert";

class DetailSettingTenant extends Component {
    constructor() {
        super();
        this.syncTenant = this.syncTenant.bind(this);
        this.loadDistrictses = this.loadDistrictses.bind(this);
        this.populateDistrictses = this.populateDistrictses.bind(this);
        this.handleChangeDistrict = this.handleChangeDistrict.bind(this);
        this.loadCities = this.loadCities.bind(this);
        this.populateCities = this.populateCities.bind(this);
        this.handleChangeCity = this.handleChangeCity.bind(this);
        this.handleChangeTenantName = this.handleChangeTenantName.bind(this);
        this.handleChangeWhatsappNumber = this.handleChangeWhatsappNumber.bind(this);
        this.handleChangePhone = this.handleChangePhone.bind(this);
        this.handleChangeAddress = this.handleChangeAddress.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
        this.removeBrandImage = this.removeBrandImage.bind(this);
        this.handleAddImage = this.handleAddImage.bind(this);
        this.handleDeleteImage = this.handleDeleteImage.bind(this);

        this.state = {
            loading: true,
            buttonState: '',
            city: undefined,
            district: undefined,
            tenancyName: '',
            name: '',
            brandUrl: '',
            whatsappNumber: '',
            phone: '',
            address: ''
        };
    }

    componentDidMount() {
        this.syncTenant();
    }

    syncTenant() {
        this.setState({ loading: true });
        getTenant()
            .then(res => {
                this.setState({
                    tenancyName: res.data.tenancyName,
                    name: res.data.name,
                    brandUrl: res.data.brandUrl,
                    whatsappNumber: res.data.whatsappNumber,
                    phone: res.data.phone,
                    address: res.data.address,
                    city: { label: res.data.district.city.name, value: res.data.district.city.id },
                    district: { label: res.data.district.name, value: res.data.district.id },
                    loading: false
                });
            });
    }

    loadDistrictses = inputValue =>
        new Promise(resolve => {
            resolve(this.populateDistrictses(inputValue));
        });

    async populateDistrictses(searchKey) {
        if (this.state.city === undefined) {
            return;
        }

        var res = await getDistrictses(this.state.city.value, searchKey);
        var result = [];
        res.data.forEach(item => {
            result.push({ label: item.name, value: item.id });
        });
        return result;
    }

    handleChangeDistrict(ditrict) {
        this.setState({
            district: ditrict
        });
    }

    loadCities = inputValue =>
        new Promise(resolve => {
            resolve(this.populateCities(inputValue));
        });

    async populateCities(searchKey) {
        searchKey = searchKey || "";
        var res = await getCities(searchKey);
        var result = [];
        res.data.forEach(item => {
            result.push({ label: item.name, value: item.id });
        });
        return result;
    }

    handleChangeCity(city) {
        this.setState({
            city: city,
            district: undefined
        });
    }


    handleChangeTenantName(ev) {
        let tenantName = ev.target.value;
        this.setState({ name: tenantName });
    }

    handleChangeWhatsappNumber(ev) {
        let whatsappNumber = ev.target.value;
        this.setState({ whatsappNumber: whatsappNumber });
    }

    handleChangePhone(ev) {
        let phone = ev.target.value;
        this.setState({ phone: phone });
    }

    handleChangeAddress(ev) {
        let address = ev.target.value;
        this.setState({ address: address });
    }

    removeBrandImage(ev) {
        ev.preventDefault();
        this.setState({
            brandUrl: ''
        });
    }

    handleAddImage(files) {
        var self = this;
        var reader = new FileReader();
        this.setState({ image: files[0] });
        reader.onload = (function (aImg) {
            self.setState({
                imageData: aImg.target.result
            });
        });

        reader.readAsDataURL(files[0]);
    }

    handleDeleteImage(ev) {
        ev.preventDefault();
        this.setState({
            imageData: ''
        });
    }

    handleSubmit(ev) {
        ev.preventDefault();
        this.setState({ buttonState: 'loading' });
        const formData = new FormData();
        formData.append('id', this.state.id);
        formData.append('name', this.state.name);
        formData.append('whatsappNumber', this.state.whatsappNumber || '');
        formData.append('address', this.state.address || '');
        formData.append('phone', this.state.phone || '');
        formData.append('districtId', this.state.district.value);
        formData.append('image', this.state.image);

        const config = {
            headers: {
                'content-type': 'multipart/form-data',
            },
        };

        const self = this;
        editTenant(formData, config)
            .then(res => {
                Alert.success("Berhasil Simpan.");
            })
            .finally(() => {
                this.setState({ buttonState: '' });
            });
    }

    render() {
        const tenant = this.state;
        if (this.state.loading)
            return (<></>);
        else {
            let brandImageContent = "";
            let imageData = this.state.imageData;
            let brandUrl = this.state.brandUrl;
            if (imageData) {
                brandImageContent =
                    <Col xs={12}>
                        <img src={imageData}
                            alt="Upload Gambar Produk"
                            className="margin-center"
                            style={{ width: "auto", height: "auto", maxHeight: "100px", margin: "0 auto", display: "block" }} />
                        <div className="mt-2 hand text-center text-underline" onClick={this.handleDeleteImage}>
                            <a>Ganti Gambar</a>
                        </div>
                    </Col>
            }
            else if (brandUrl) {
                brandImageContent =
                    <Col xs={12}>
                        <img src={brandUrl}
                            alt="Upload Gambar Produk"
                            className="margin-center"
                            style={{ width: "auto", height: "auto", maxHeight: "100px", margin: "0 auto", display: "block" }} />
                        <div className="mt-2 hand text-center color-orange text-underline" onClick={this.removeBrandImage}>
                            <a>Ganti Gambar</a>
                        </div>
                    </Col>
            } else {
                brandImageContent =
                    <Col xs={12} className="mb-10px">
                        <Dropzone onFilesAdded={this.handleAddImage} />
                    </Col>
            }

            return (
                <div className="kt-form kt-form--label-right setting-tenant-page">
                    <Portlet>
                        <PortletBody>
                            <Container>
                                <Form onSubmit={this.handleSubmit}>
                                    <Row>
                                        <Col xs={6}>
                                            <Form.Group as={Row}>
                                                <Col xs={12}>
                                                    <Form.Label>ID Toko</Form.Label>
                                                    <Form.Control type="text" disabled={true} placeholder="ID Toko" defaultValue={tenant.tenancyName} />
                                                </Col>
                                            </Form.Group>
                                            <Form.Group as={Row}>
                                                <Col xs={12}>
                                                    <Form.Label>Nama Toko</Form.Label>
                                                    <Form.Control type="text" placeholder="Nama Toko" defaultValue={tenant.name} onBlur={this.handleChangeTenantName} />
                                                </Col>
                                            </Form.Group>
                                        </Col>
                                        <Col xs={6}>
                                            <Form.Group as={Row}>
                                                {brandImageContent}
                                            </Form.Group>
                                        </Col>
                                    </Row>
                                    <Form.Group as={Row}>
                                        <Col xs={12} sm={6}>
                                            <Form.Label>No. Whatsapp</Form.Label>
                                            <Form.Control type="text" placeholder="No. Whatsapp" defaultValue={tenant.whatsappNumber} onBlur={this.handleChangeWhatsappNumber} />
                                        </Col>
                                        <Col xs={12} sm={6}>
                                            <Form.Label>No. Telpon</Form.Label>
                                            <Form.Control type="text" placeholder="No. Telpon" defaultValue={tenant.phone} onBlur={this.handleChangePhone} />
                                        </Col>
                                    </Form.Group>

                                    <Form.Group as={Row}>
                                        <Col xs={12} sm={6}>
                                            <Form.Label>Kota</Form.Label>
                                            <AsyncSelect className="react-select"
                                                placeholder="Pilih Kota"
                                                defaultOptions
                                                loadOptions={this.loadCities}
                                                value={this.state.city}
                                                onChange={this.handleChangeCity}
                                            />
                                        </Col>
                                        <Col xs={12} sm={6}>
                                            <Form.Label>Kecamatan</Form.Label>
                                            <AsyncSelect className="react-select"
                                                key={JSON.stringify(this.state.city)}
                                                placeholder="Pilih Kecamatan"
                                                defaultOptions
                                                loadOptions={this.loadDistrictses}
                                                value={this.state.district}
                                                onChange={this.handleChangeDistrict}
                                            />
                                        </Col>
                                    </Form.Group>

                                    <Form.Group as={Row}>
                                        <Col xs={6}>
                                            <Form.Label>Alamat</Form.Label>
                                            <Form.Control as="textarea" rows="4" placeholder="Alamat Toko" defaultValue={tenant.address} onBlur={this.handleChangeAddress} />
                                        </Col>
                                    </Form.Group>

                                    <Form.Group as={Row}>
                                        <Col>
                                            <HozaruButton type="submit" className="btn btn-primary" state={this.state.buttonState}>Simpan</HozaruButton>
                                        </Col>
                                    </Form.Group>
                                </Form>
                            </Container>
                        </PortletBody>
                    </Portlet>
                </div>
            );
        }
    }
}

export default DetailSettingTenant;