import React, { Component } from 'react';
import './NavMenu.css';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faArrowLeft } from '@fortawesome/free-solid-svg-icons';
import { Link } from 'react-router-dom';

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
        backUrl: '/'
    }];

    constructor(props) {
        super(props);

        this.toggleNavbar = this.toggleNavbar.bind(this);
        this.setNagivationInfo = this.setNagivationInfo.bind(this);
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
        var navigationInfo = NavMenu.navigations.find(nav => nav.path === path);
        this.setState({
            title: navigationInfo.title,
            hasBackButton: navigationInfo.hasBackButton,
            backUrl: navigationInfo.backUrl
        });
    }

    toggleNavbar() {
        this.setState({
            collapsed: !this.state.collapsed
        });
    }

    render() {
        var backButtonElement = this.state.hasBackButton &&
            <Link className="backButton btn btn-link" to={this.state.backUrl}>
                <FontAwesomeIcon icon={faArrowLeft} />
            </Link>
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
