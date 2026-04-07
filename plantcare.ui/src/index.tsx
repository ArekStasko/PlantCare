import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';
import App from './App';
import reportWebVitals from './reportWebVitals';
import { BrowserRouter } from 'react-router-dom';
import RoutingConstants from './app/routing/routingConstants';
import ExpirationBanner from './pages/expirationBanner/expirationBanner';
import IdpClient from 'identity-provider-client';
import emptyApi from "./common/RTK/emptyApi";

const root = ReactDOM.createRoot(document.getElementById('root') as HTMLElement);
root.render(
  <React.StrictMode>
    <BrowserRouter>
      <IdpClient
        clientApi={emptyApi}
        authBaseRoute={RoutingConstants.authBasic}
        dashboardRoute={RoutingConstants.root}
        expirationBanner={ExpirationBanner}
      >
        <App />
      </IdpClient>
    </BrowserRouter>
  </React.StrictMode>
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
