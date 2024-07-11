import React, { FunctionComponent, ReactElement, useEffect, useState } from 'react';
import { Box } from '@mui/material';
import Navbar from '../../compontents/navbar/navbar';
import styles from './baseLayout.styles';
import { useCheckTokenQuery } from '../../slices/checkTokenExpiration/checkTokenExpiration';
import { useNavigate } from 'react-router';
import RoutingConstants from '../../../app/routing/routingConstants';
import { DeleteToken, GetToken } from '../../services/CookieService';
import usePageTracking from '../../services/PageTracking';

type BaseLayoutProps = {
  children: ReactElement;
};

export const BaseLayout: FunctionComponent<BaseLayoutProps> = ({ children }) => {
  const navigate = useNavigate();
  const [token, setToken] = useState<string | undefined>(undefined);
  const { data: isTokenValid, isFetching, refetch } = useCheckTokenQuery(token!, { skip: !token });
  usePageTracking();

  useEffect(() => {
    const token = GetToken();
    if (!token) navigate(RoutingConstants.authBasic);
    setToken(token);
    const interval = setInterval(() => {
      refetch();
    }, 30000);
    return () => clearInterval(interval);
  }, []);

  useEffect(() => {
    if (!isTokenValid && isTokenValid !== undefined) {
      DeleteToken();
      navigate(RoutingConstants.authBasic);
    }
  }, [isFetching]);

  return (
    <Box sx={styles.container}>
      <Navbar />
      <Box>{children}</Box>
    </Box>
  );
};

export default BaseLayout;
