import React, { Component } from 'react';
import { Portlet, PortletBody } from '../../../partials/content/Portlet';
import { Form, Row, Col } from 'react-bootstrap';
import Dropzone from "../../../partials/content/dropzone";
import { getProduct, editProduct, archiveProduct, activateProduct } from "../../../crud/product.crud";
import HozaruButton from "../../../partials/layout/hozaru-button";
import Notice from "../../../partials/content/Notice";

class EditProduct extends Component {
    constructor() {
        super();
        this.syncProduct = this.syncProduct.bind(this);
        this.handleChangeProductName = this.handleChangeProductName.bind(this);
        this.handleChangeProductDescription = this.handleChangeProductDescription.bind(this);
        this.handleChangeProductPrice = this.handleChangeProductPrice.bind(this);
        this.handleChangeProductWeight = this.handleChangeProductWeight.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
        this.handleAddImage = this.handleAddImage.bind(this);
        this.handleDeleteImage = this.handleDeleteImage.bind(this);
        this.removeOriginImage = this.removeOriginImage.bind(this);
        this.handleCancel = this.handleCancel.bind(this);
        this.archiveProduct = this.archiveProduct.bind(this);
        this.activateProduct = this.activateProduct.bind(this);
        this.state = {
            id: '',
            name: '',
            description: '',
            price: '',
            weight: '',
            status: '',
            images: [],
            imagesData: [],
            imagesUrls: [],
            deletedImages: [],
            buttonState: '',
            buttonArchiveState: '',
            buttonActivateState: '',
            buttonCancelState: ''
        };
    }

    componentDidMount() {
        this.syncProduct();
    }

    syncProduct() {
        let productId = this.props.match.params.id;
        getProduct(productId)
            .then(res => {
                this.setState({
                    id: res.data.id,
                    name: res.data.name,
                    description: res.data.description,
                    price: res.data.price,
                    weight: res.data.weight,
                    status: res.data.status
                });
                var imagesUrls = [];
                for (const image of res.data.images) {
                    imagesUrls.push({ priority: image.priority, url: image.url });
                }
                this.setState({
                    imagesUrls: imagesUrls
                });
            });
    }

    removeOriginImage(priority, ev) {
        ev.preventDefault();
        var deletedImages = this.state.deletedImages;
        deletedImages.push(priority);
        this.setState({
            imagesUrls: this.state.imagesUrls.filter(function (imageUrl) {
                return imageUrl.priority !== priority
            }),
            deletedImages: deletedImages
        });
    }

    handleChangeProductName(ev) {
        let productName = ev.target.value;
        this.setState({ name: productName });
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

    handleSubmit(ev) {
        ev.preventDefault();
        this.setState({ buttonState: 'loading' });
        const formData = new FormData();
        formData.append('id', this.state.id);
        formData.append('name', this.state.name);
        formData.append('description', this.state.description);
        formData.append('weight', this.state.weight);
        formData.append('price', this.state.price);

        for (var i = 0; i < this.state.deletedImages.length; i++) {
            formData.append('deletedImagesByPriority', this.state.deletedImages[i]);
        }

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
        editProduct(formData, config)
            .then(res => {
                self.props.history.push("/products");
            })
            .finally(() => {
                this.setState({ buttonState: '' });
            });
    }

    handleCancel(ev) {
        ev.preventDefault();
        this.props.history.push("/products");
    }

    archiveProduct(ev) {
        ev.preventDefault();
        this.setState({ buttonArchiveState: 'loading' });
        archiveProduct(this.state.id)
            .then(res => {
                this.props.history.push("/products");
            })
            .finally(() => {
                this.setState({ buttonArchiveState: '' });
            });
    }

    activateProduct(ev) {
        ev.preventDefault();
        this.setState({ buttonActivateState: 'loading' });
        activateProduct(this.state.id)
            .then(res => {
                this.props.history.push("/products");
            })
            .finally(() => {
                this.setState({ buttonActivateState: '' });
            });
    }

    render() {
        var dropzoneImages = [];
        for (let i = 1; i <= 6; i++) {
            let content = "";
            let imageData = this.state.imagesData.find(index => index.priority === i);
            let imageUrl = this.state.imagesUrls.find(index => index.priority === i);
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
            }
            else if (imageUrl) {
                content =
                    <Col sm={2} xs={6} key={imageUrl.priority} className="mb-10px">
                        <img src={imageUrl.url}
                            alt="Upload Gambar Produk"
                            className="margin-center"
                            style={{ width: "auto", height:"auto", maxHeight: "100px", margin: "0 auto", display: "block" }} />
                        <div className="mt-2 hand text-center color-orange text-underline" onClick={this.removeOriginImage.bind(this, imageUrl.priority)}>
                            <a>Hapus gambar</a>
                        </div>
                    </Col>
            } else {
                content =
                    <Col sm={2} xs={6} key={i} className="mb-10px">
                        <Dropzone onFilesAdded={this.handleAddImage} data={{ priority: i }} />
                    </Col>
            }
            dropzoneImages.push(content);
        }

        let status = "";
        if (this.state.status === 20) {
            status =
                <Notice icon="flaticon-warning kt-font-primary">
                    Kamu telah mengarsipkan produkmu.
                </Notice>
        }

        return (
            <div className="kt-form kt-form--label-right">
                {status}
                <Portlet>
                    <PortletBody>
                        <Form onSubmit={this.handleSubmit}>
                            <Form.Group>
                                <Form.Label>Nama Produk</Form.Label>
                                <Form.Control type="text" placeholder="Nama Produk" defaultValue={this.state.name} onBlur={this.handleChangeProductName} />
                                <Form.Text className="text-muted">
                                    We'll never share your email with anyone else.
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
                                <Col sm={6}>
                                    <HozaruButton className="btn btn-secondary" state={this.state.buttonCancelState} onClick={this.handleCancel}>Batal</HozaruButton>
                                    <HozaruButton type="submit" className="btn btn-primary" state={this.state.buttonState}>Simpan</HozaruButton>
                                </Col>
                                <Col sm={6} className="text-right">
                                    {this.state.status === 10 &&
                                        <HozaruButton type="button" className="btn btn-warning" state={this.state.buttonArchiveState} onClick={this.archiveProduct}>Arsipkan Produk</HozaruButton>
                                    }
                                    {this.state.status === 20 &&
                                        <HozaruButton type="button" className="btn btn-primary" state={this.state.buttonActivateState} onClick={this.activateProduct}>Aktifkan Produk</HozaruButton>
                                    }
                                </Col>
                            </Form.Group>
                        </Form>
                    </PortletBody>
                </Portlet>
            </div >
        );
    }
}

export default EditProduct;