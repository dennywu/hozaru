/**
 * High level router.
 *
 * Note: It's recommended to compose related routes in internal router
 * components (e.g: `src/pages/auth/AuthPage`, `src/pages/home/HomePage`).
 */

import React, { Suspense } from "react";
import { Redirect, Route, Switch, withRouter } from "react-router-dom";
import { shallowEqual, useSelector } from "react-redux";
import { useLastLocation } from "react-router-last-location";
import AuthPage from "../pages/auth/AuthPage";
import ErrorsPage from "../pages/errors/ErrorsPage";
import LogoutPage from "../pages/auth/Logout";
import { LayoutContextProvider, LayoutSplashScreen } from "../../_metronic";
import * as routerHelpers from "../router/RouterHelpers";
import ProductPage from "../pages/product/ProductPage";
import OrderPage from "../pages/orders/OrderPage";

export const Routes = withRouter(({ Layout, history }) => {
    const lastLocation = useLastLocation();
    routerHelpers.saveLastLocation(lastLocation);
    const { isAuthorized, menuConfig, userLastLocation } = useSelector(
        ({ auth, urls, builder: { menuConfig } }) => ({
            menuConfig,
            isAuthorized: true,//auth.user != null,
            userLastLocation: routerHelpers.getLastLocation()
        }),
        shallowEqual
    );

    return (
        /* Create `LayoutContext` from current `history` and `menuConfig`. */
        <LayoutContextProvider history={history} menuConfig={menuConfig}>
            <Switch>
                {!isAuthorized ? (
                    /* Render auth page when user at `/auth` and not authorized. */
                    <Route path="/auth/login" component={AuthPage} />
                ) : (
                        /* Otherwise redirect to root page (`/`) */
                        <Redirect from="/auth" to={userLastLocation} />
                    )}

                <Route path="/error" component={ErrorsPage} />
                <Route path="/logout" component={LogoutPage} />

                {!isAuthorized ? (
                    /* Redirect to `/auth` when user is not authorized */
                    <Redirect to="/auth/login" />
                ) : (
                        <Layout>
                            <ProductPage userLastLocation={userLastLocation} />
                            <OrderPage userLastLocation={userLastLocation} />
                        </Layout>
                    )}
            </Switch>
        </LayoutContextProvider>
    );
});
