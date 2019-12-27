import React, { Component } from 'react';
import NavMenu from './components/navigation';
import { connect } from 'react-redux';
import { changeTenantInfo } from './services/tenant/actions';
import { storeApiKey } from './services/api-key/actions';
import axios from 'axios';
import { API_URL } from "./configuration";

class Layout extends Component {
    constructor() {
        super();
        this.populateApiKey = this.populateApiKey.bind(this);
        this.populateTenantInformation = this.populateTenantInformation.bind(this);
        this.changeFavicon = this.changeFavicon.bind(this);
        this.changePageTitle = this.changePageTitle.bind(this);
        this.state = { fetchedApiKey: false };
    }

    componentDidMount() {
        this.populateApiKey();
    }

    async populateApiKey() {
        await axios.get('/ApiKey', { baseURL: "/" })
            .then(res => {
                window.ApiKey = res.data;
                this.populateTenantInformation();
                this.setState({ fetchedApiKey: true });
            });
    }

    async populateTenantInformation() {
        axios.get('/api/tenants/info')
            .then(res => {
                this.props.changeTenantInfo(res.data);
                this.changeFavicon(res.data);
                this.changePageTitle(res.data);
            });
    }

    changeFavicon(tenant) {
        const favicon = document.getElementById("favicon");
        var faviconUrl = API_URL + "/api/tenants/favicon/" + tenant.tenancyName;
        favicon.href = faviconUrl;
    }

    changePageTitle(tenant) {
        const favicon = document.getElementById("pageTitle");
        var tenantName = tenant.name.charAt(0).toUpperCase() + tenant.name.substring(1);
        favicon.innerText = tenantName + " Official Store";
    }

    render() {
        if (!this.state.fetchedApiKey) {
            return <></>;
        }
        return (
            <>
                <NavMenu />
                {this.props.children}
            </>
        );
    }
}


const mapStateToProps = state => ({
    tenant: state.tenant
});

export default connect(mapStateToProps, { changeTenantInfo, storeApiKey })(Layout);
