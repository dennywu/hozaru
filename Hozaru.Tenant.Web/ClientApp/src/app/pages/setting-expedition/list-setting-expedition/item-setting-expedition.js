import React, { Component } from "react";
import { withRouter } from "react-router-dom";
import PropTypes from "prop-types";
import { ListItem, ListItemIcon, ListItemText, ListItemSecondaryAction, Switch } from "@material-ui/core";
import { updateStatusTenantExpeditionService } from '../../../crud/expedition.crud';

class ItemSettingExpedition extends Component {
    constructor() {
        super();

        this.handleToggle = this.handleToggle.bind(this);
        this.state = {
            checked: false
        };
    }

    static propTypes = {
        tenantExpeditionService: PropTypes.object.isRequired
    };

    componentDidMount() {
        this.setState({ checked: this.props.tenantExpeditionService.isActive });
    }

    handleToggle() {
        var tenantExpeditionServiceId = this.props.tenantExpeditionService.id;
        var newStatus = !this.state.checked;
        updateStatusTenantExpeditionService(tenantExpeditionServiceId, newStatus)
            .then(res => {
                this.setState({ checked: res.data.isActive });
            });
    }

    render() {
        const { tenantExpeditionService } = this.props;
        return (
            <ListItem>
                <ListItemText id="switch-list-label-wifi" primary={tenantExpeditionService.expeditionService.originalFullName + " (" + tenantExpeditionService.expeditionService.name + ")"} />
                <ListItemSecondaryAction>
                    <Switch
                        edge="end"
                        onChange={this.handleToggle}
                        checked={this.state.checked}
                        inputProps={{ 'aria-labelledby': 'switch-list-label-' + tenantExpeditionService.expeditionService.originalFullName }}
                    />
                </ListItemSecondaryAction>
            </ListItem>
        );
    }
}

export default withRouter(ItemSettingExpedition);