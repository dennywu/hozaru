import React, { Component, Suspense } from 'react';
import { Switch, Route, Redirect } from 'react-router-dom';
import { LayoutSplashScreen } from '../../../_metronic';
import ListSettingExpedition from './list-setting-expedition';

class SettingExpeditionPage extends Component {
    render() {
        return (
            <Suspense fallback={<LayoutSplashScreen />}>
                <Switch>
                    <Route exact path="/settingexpeditionservices" component={ListSettingExpedition} />
                </Switch>
            </Suspense>
        );
    }
}

export default SettingExpeditionPage;