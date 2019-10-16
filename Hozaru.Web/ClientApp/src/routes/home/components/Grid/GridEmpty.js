import React, { Component } from 'react';
import './Grid.css';

export class GridEmpty extends Component {

    render() {
        return (
            <div className="product-image empty">
                <div className="product-image-container">
                </div>
            </div>
        );
    }
}