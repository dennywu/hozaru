import React, { Component } from 'react';
import FadeIn from "react-fade-in";
import ReactLoading from 'react-loading';
import "bootstrap/dist/css/bootstrap.css";
import './index.css';

export default class Loading extends Component {
    render() {
        return (
            <div className="loading-overlay">
                <FadeIn className="loading-contain">
                    <div className="d-flex justify-content-center align-items-center">
                        <ReactLoading type={"bars"} color={"#000"} />
                    </div>
                </FadeIn>
            </div>
        );
    }
}