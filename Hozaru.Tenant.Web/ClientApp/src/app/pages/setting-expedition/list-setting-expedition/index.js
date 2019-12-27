import React, { Component } from "react";
import { Link } from "react-router-dom";
import { Portlet, PortletBody, PortletHeader, PortletHeaderToolbar } from "../../../partials/content/Portlet";
import { Row, Col } from "react-bootstrap";
import { getTenantExpeditionServices } from '../../../crud/expedition.crud';
import ItemSettingExpedition from "./item-setting-expedition";
import { List } from "@material-ui/core";


class ListSettingExpedition extends Component {
    constructor() {
        super();
        this.syncTenantExpeditionServices = this.syncTenantExpeditionServices.bind(this);

        this.state = {
            loading: true,
            tenantExpeditionServices: []
        };
    }

    componentDidMount() {
        this.syncTenantExpeditionServices();
    }

    syncTenantExpeditionServices() {
        getTenantExpeditionServices()
            .then(res => {
                this.setState({ tenantExpeditionServices: res.data, loading: false });
            });
    }

    render() {
        const { tenantExpeditionServices, loading } = this.state;
        if (loading) {
            return (<></>);
        }
        else {
            return (
                <div className="kt-form kt-form--label-right order-page">
                    <Portlet>
                        <PortletBody>
                            <div className="container">
                                <List>
                                    <Row>
                                        {
                                            tenantExpeditionServices.map(tenantExpeditionService => (
                                                <Col xs={12} sm={6} key={tenantExpeditionService.id}>
                                                    <ItemSettingExpedition tenantExpeditionService={tenantExpeditionService} />
                                                </Col>
                                            ))
                                        }
                                    </Row>
                                </List>
                            </div>
                        </PortletBody>
                    </Portlet>
                </div>
            );
        }
    }
}

export default ListSettingExpedition;