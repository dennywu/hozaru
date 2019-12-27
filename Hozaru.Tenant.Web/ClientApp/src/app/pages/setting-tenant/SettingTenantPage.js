import React, { Component, Suspense } from 'react';
import { Switch, Route, Redirect } from 'react-router-dom';
import { LayoutSplashScreen } from '../../../_metronic';
import DetailSettingTenant from './detail';

class SettingExpeditionPage extends Component {
    render() {
        return (
            <Suspense fallback={<LayoutSplashScreen />}>
                <Switch>
                    <Route exact path="/setting-tenant" component={DetailSettingTenant} />
                </Switch>
            </Suspense>
        );
    }
}

export default SettingExpeditionPage;