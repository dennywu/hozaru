import React, { Component } from 'react';
import './NavMenu.css';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faArrowLeft } from '@fortawesome/free-solid-svg-icons';
import { connect } from 'react-redux';

const navigations = [{
    path: '/',
    title: 'hozaru',
    hasBackButton: false,
    backUrl: '/'
}, {
    path: '/checkout',
    title: 'Keranjang',
    hasBackButton: true,
    backUrl: 'before'
}, {
    path: '/payment/:id',
    title: 'Info Pembayaran',
    hasBackButton: true,
    backUrl: '/'
}, {
    path: '/order/:id',
    title: 'Pesanan Anda',
    hasBackButton: true,
    backUrl: '/'
}, {
    path: '/order/:id/tracking',
    title: 'Status Pengiriman',
    hasBackButton: true,
    backUrl: 'before'
}, {
    path: '/payment-confirmation/:id',
    title: 'Konfirmasi Pembayaran',
    hasBackButton: true,
    backUrl: 'before'
}];

class NavMenu extends Component {

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

    componentWillReceiveProps(nextProp) {
        var homeTitle = navigations.find(i => i.path == "/");
        homeTitle.title = nextProp.tenant.name.toLowerCase() || homeTitle.title;

        this.setNagivationInfo();
    }

    setNagivationInfo() {
        var path = window.location.pathname;
        //if (path.split('/').length > 2) {
        //    pathName = '/' + path.split('/')[1];
        //}

        //var navigationInfo = navigations.find(nav => nav.path === pathName);

        var navigationInfo = this.findNagivation(path) || this.findAdvanceNagivation(path);
        this.setState({
            title: navigationInfo.title,
            hasBackButton: navigationInfo.hasBackButton,
            backUrl: navigationInfo.backUrl
        });
    }

    findNagivation(currentPage) {
        for (const itemNav of navigations) {
            // Return `item` if it's `page` matches `currentPage`
            if (currentPage === itemNav.path) {
                return itemNav;
            }
        }
    }

    findAdvanceNagivation(currentPage) {
        var currentPage = currentPage[0] === "/" ? currentPage.slice(1) : currentPage;
        for (const itemNav of navigations) {
            var firstPath = currentPage.split('/')[0];
            var secondPath = currentPage.split('/')[1];
            var thirdPath = currentPage.split('/')[2];

            if (itemNav.path && itemNav.path.includes(":id")) {
                const itemPage = itemNav.path[0] === "/" ? itemNav.path.slice(1) : itemNav.page;
                var firstItemPage = itemPage.split('/')[0];
                var secondItemPage = itemPage.split('/')[1];
                var thirdItemPage = itemPage.split('/')[2];

                // Return `item` if it's `page` matches `currentPage`
                if (!thirdPath) {
                    if (firstPath === firstItemPage) {
                        return itemNav;
                    }
                } else {
                    if (firstPath === firstItemPage && thirdPath === thirdItemPage) {
                        return itemNav;
                    }
                }
            }
        }
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
                        <div style={{ display: "none" }}>{this.props.tenant.name}</div>
                    </div>
                </div>
            </header>
        );
    }
}

const mapStateToProps = state => ({
    tenant: state.tenant
});

export default connect(mapStateToProps, {})(NavMenu);
