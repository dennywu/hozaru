import React, { Component } from 'react';
import './NavMenu.css';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faArrowLeft } from '@fortawesome/free-solid-svg-icons';

export class NavMenu extends Component {
    static displayName = NavMenu.name;
    static navigations = [{
        path: '/',
        title: 'mumubeautyhouse.id',
        hasBackButton: false,
        backUrl: '/'
    }, {
        path: '/checkout',
        title: 'Keranjang',
        hasBackButton: true,
        backUrl: 'before'
    }, {
        path: '/payment',
        title: 'Info Pembayaran',
        hasBackButton: true,
        backUrl: '/'
    }, {
        path: '/order',
        title: 'Pesanan Anda',
        hasBackButton: true,
        backUrl: '/'
    }, {
        path: '/payment-confirmation',
        title: 'Konfirmasi Pembayaran',
        hasBackButton: true,
        backUrl: 'before'
    }];

    constructor(props) {
        super(props);

        this.toggleNavbar = this.toggleNavbar.bind(this);
        this.setNagivationInfo = this.setNagivationInfo.bind(this);
        this.handleBackButton = this.handleBackButton.bind(this);
        this.state = {
            collapsed: true,
            title: '',
            hasBackButton: '',
            backUrl: ''
        };
    }

    componentDidMount() {
        this.setNagivationInfo();
    }

    componentWillReceiveProps() {
        this.setNagivationInfo();
    }

    setNagivationInfo() {
        var path = window.location.pathname;
        var pathName = path;
        if (path.split('/').length > 2) {
            pathName = '/' + path.split('/')[1];
        }

        var navigationInfo = NavMenu.navigations.find(nav => nav.path === pathName);
        this.setState({
            title: navigationInfo.title,
            hasBackButton: navigationInfo.hasBackButton,
            backUrl: navigationInfo.backUrl
        });
    }

    handleBackButton() {
        if (this.state.backUrl === 'before') {
            window.history.back();
        }
        else {
            window.location = this.state.backUrl;
        }
    }

    toggleNavbar() {
        this.setState({
            collapsed: !this.state.collapsed
        });
    }

    render() {
        var backButtonElement = this.state.hasBackButton &&
            <button className="backButton btn btn-link" onClick={this.handleBackButton}>
                <FontAwesomeIcon icon={faArrowLeft} />
            </button>
        return (
            <header>
                <div className="navbar navbar-expand-sm navbar-toggleable-sm navbar-light bg-white border-bottom box-shadow mb-3 fixed-top">
                    {backButtonElement}
                    <div className="container">
                        <div className="navbar-brand margin-center">{this.state.title}</div>
                    </div>
                </div>
            </header>
        );
    }
}
