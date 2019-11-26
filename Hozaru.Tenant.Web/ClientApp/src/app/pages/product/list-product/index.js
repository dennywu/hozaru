import React, { Component } from 'react';
import { Link } from "react-router-dom";
import { Portlet, PortletBody, PortletHeader, PortletHeaderToolbar } from "../../../partials/content/Portlet";
import './style.css';
import { getProducts } from '../../../crud/product.crud';
import ProductCard from './product-card';

class Product extends Component {
    constructor() {
        super();
        this.handleChangeTab = this.handleChangeTab.bind(this);
        this.state = {
            products: []
        };
    }

    componentDidMount() {
        getProducts()
            .then(res => {
                this.setState({ products: res.data });
            });
    }

    handleChangeTab(_, nextTab) {
        this.setState({ activeTab: nextTab });
    }

    render() {
        const { products } = this.state;
        let productCards = [];
        for (var i = 0; i < products.length; i++) {
            var product = products[i];
            productCards.push(
                <div className="col-xl-2 col-lg-2 col-md-2 col-sm-4 col-6" key={product.id}>
                    <ProductCard product={product} />
                </div>
            );
        }

        return (
            <div className="kt-form kt-form--label-right">
                <Portlet>
                    {
                        <PortletHeader
                            toolbar={
                                <PortletHeaderToolbar>
                                    <Link to="/products/new" className="btn btn-primary btn-sm kt-btn">
                                        <i className="fa fa-plus-circle" />
                                        Tambah Produk Baru
                                    </Link>
                                </PortletHeaderToolbar>
                            }
                        />
                    }
                    <PortletBody>
                        <div className="row">
                            {productCards}
                        </div>
                    </PortletBody>
                </Portlet>
            </div>
        );
    }
}

export default Product;