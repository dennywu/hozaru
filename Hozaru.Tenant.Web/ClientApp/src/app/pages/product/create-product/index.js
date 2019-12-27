import React, { Component } from 'react';
import { withRouter } from "react-router-dom";
import { Portlet, PortletBody } from '../../../partials/content/Portlet';
import { Form, Row, Col } from 'react-bootstrap';
import Dropzone from "../../../partials/content/dropzone";
import { createProduct } from "../../../crud/product.crud";
import HozaruButton from "../../../partials/layout/hozaru-button";

class CreateProduct extends Component {
    constructor() {
        super();
        this.handleChangeProductName = this.handleChangeProductName.bind(this);
        this.handleChangeSKU = this.handleChangeSKU.bind(this);
        this.handleChangeProductDescription = this.handleChangeProductDescription.bind(this);
        this.handleChangeProductPrice = this.handleChangeProductPrice.bind(this);
        this.handleChangeProductWeight = this.handleChangeProductWeight.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
        this.handleAddImage = this.handleAddImage.bind(this);
        this.handleDeleteImage = this.handleDeleteImage.bind(this);
        this.state = {
            name: '',
            sku: '',
            description: '',
            price: '',
            weight: '',
            images: [],
            imagesData: [],
            buttonState: ''
        };
    }

    handleSubmit(ev) {
        ev.preventDefault();
        this.setState({ buttonState: 'loading' });
        const formData = new FormData();
        formData.append('name', this.state.name);
        formData.append('sku', this.state.sku || "");
        formData.append('description', this.state.description);
        formData.append('weight', this.state.weight || 0);
        formData.append('price', this.state.price || 0);

        for (var i = 0; i < this.state.images.length; i++) {
            formData.append('images[' + i + '].priority', this.state.images[i].priority);
            formData.append('images[' + i + '].image', this.state.images[i].image);
        }

        const config = {
            headers: {
                'content-type': 'multipart/form-data',
            },
        };

        const self = this;
        createProduct(formData, config)
            .then(res => {
                self.props.history.push("/products");
            })
            .finally(() => {
                this.setState({ buttonState: '' });
            });
    }

    handleChangeProductName(ev) {
        let productName = ev.target.value;
        this.setState({ name: productName });
    }

    handleChangeSKU(ev) {
        let sku = ev.target.value;
        this.setState({ sku: sku });
    }

    handleChangeProductDescription(ev) {
        let productDescription = ev.target.value;
        this.setState({ description: productDescription });
    }

    handleChangeProductPrice(ev) {
        let productPrice = ev.target.value;
        this.setState({ price: productPrice });
    }

    handleChangeProductWeight(ev) {
        let productWeight = ev.target.value;
        this.setState({ weight: productWeight });
    }

    handleAddImage(files, data) {
        var existImages = this.state.images;
        var existImage = existImages.find(i => i.priority === data.priority);
        if (existImage) {
            existImage.image = files[0]
        } else {
            existImages.push({
                priority: data.priority,
                image: files[0]
            });
        }

        this.setState({ images: existImages });

        var self = this;
        var reader = new FileReader();
        var imagesData = this.state.imagesData;
        reader.onload = (function (aImg) {
            imagesData.push({ priority: data.priority, data: aImg.target.result });
            self.setState({
                imagesData: imagesData
            });
        });

        reader.readAsDataURL(files[0]);
    }

    handleDeleteImage(priority, ev) {
        ev.preventDefault();

        this.setState({
            images: this.state.images.filter(function (image) {
                return image.priority !== priority
            }),
            imagesData: this.state.imagesData.filter(function (imageData) {
                return imageData.priority !== priority
            })
        });
    }

    render() {
        var dropzoneImages = [];
        for (let i = 1; i <= 6; i++) {
            let content = "";
            let imageData = this.state.imagesData.find(index => index.priority === i);
            if (imageData) {
                content =
                    <Col sm={2} xs={6} key={imageData.id} className="mb-10px">
                        <img src={imageData.data}
                            alt="Upload Gambar Produk"
                            className="margin-center"
                            style={{ width: "auto", height: "auto", maxHeight: "100px", margin: "0 auto", display: "block" }} />
                        <div className="mt-2 hand text-center text-underline">
                            <a onClick={this.handleDeleteImage.bind(this, imageData.priority)}>Hapus gambar</a>
                        </div>
                    </Col>
            } else {
                content =
                    <Col sm={2} xs={6} className="mb-10px">
                        <Dropzone onFilesAdded={this.handleAddImage} data={{ priority: i }} />
                    </Col>
            }
            dropzoneImages.push(content);
        }

        return (
            <div className="kt-form kt-form--label-right">
                <Portlet>
                    <PortletBody>
                        <Form onSubmit={this.handleSubmit}>
                            <Form.Group>
                                <Form.Label>Nama Produk</Form.Label>
                                <Form.Control type="text" placeholder="Nama Produk" defaultValue={this.state.name} onBlur={this.handleChangeProductName} />
                            </Form.Group>
                            <Form.Group>
                                <Form.Label>SKU Produk</Form.Label>
                                <Form.Control type="text" placeholder="SKU atau Kode Produk" defaultValue={this.state.sku} onBlur={this.handleChangeSKU} />
                                <Form.Text className="text-muted">
                                    Ini boleh tidak diisi. Anda bisa masukkan SKU, Kode produk atau Barcode
                                </Form.Text>
                            </Form.Group>
                            <Form.Group>
                                <Form.Label>Deskripsi Produk</Form.Label>
                                <Form.Control as="textarea" rows="9" placeholder="Deskripsi Produk" defaultValue={this.state.description} onBlur={this.handleChangeProductDescription} />
                            </Form.Group>
                            <Form.Group as={Row}>
                                <Col>
                                    <Form.Label>Harga</Form.Label>
                                    <Form.Control type="text" placeholder="Rp" defaultValue={this.state.price} onBlur={this.handleChangeProductPrice} />
                                </Col>
                                <Col>
                                    <Form.Label>Berat</Form.Label>
                                    <Form.Control type="text" placeholder="Gram" defaultValue={this.state.weight} onBlur={this.handleChangeProductWeight} />
                                </Col>
                            </Form.Group>
                            <Form.Group as={Row}>
                                {dropzoneImages}
                            </Form.Group>
                            <Form.Group as={Row}>
                                <Col>
                                    <HozaruButton type="submit" className="btn btn-primary" state={this.state.buttonState}>Simpan</HozaruButton>
                                </Col>
                            </Form.Group>
                        </Form>
                    </PortletBody>
                </Portlet>
            </div >
        );
    }
}

export default withRouter(CreateProduct);