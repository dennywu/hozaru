import React, { Component } from 'react';
import './Grid.css';

export class GridLoading extends Component {

    render() {
        return (
            <div className="product-image empty">
                <div className="product-image-container">
                    <svg class="spinner" viewBox="0 0 50 50">
                        <circle
                            class="path"
                            cx="25"
                            cy="25"
                            r="20"
                            fill="none"
                            stroke-width="5"
                        ></circle>
                    </svg>
                </div>
            </div>
        );
    }
}