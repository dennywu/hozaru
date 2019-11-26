import React, { Component } from 'react';
import { Card } from 'react-bootstrap';
import { Link } from "react-router-dom";
import { default as NumberFormat } from 'react-number-format';
import Icon from '@material-ui/core/Icon';
import './style.css'

class ProductCard extends Component {
    render() {
        const { product } = this.props;
        return (
            <Card className="product-card">
                <div className="product-archive">
                    <Card.Img variant="top" src={product.firstProductImage.url} />
                    {product.status === 20 &&
                        <div className="overlay">
                            <Icon className={'fa fa-lock'} />
                            <div className="text">Telah diarsipkan</div>
                        </div>
                    }
                </div>
                <Card.Body>
                    <Card.Title className="crop-text-2 crop-text-2" style={{ fontSize: 12 }}>{product.name}</Card.Title>
                    <Card.Text>
                        <NumberFormat value={product.price} displayType={'text'} thousandSeparator={true} prefix={'Rp '} />
                    </Card.Text>
                    <Link to={"/products/" + product.id + "/edit"} className="btn btn-primary btn-sm kt-btn">
                        <i className="fa fa-edit" />
                        Edit
                    </Link>
                </Card.Body>
            </Card>
        );
    }
}

export default ProductCard;