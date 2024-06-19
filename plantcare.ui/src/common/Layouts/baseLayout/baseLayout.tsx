import React, { FunctionComponent, ReactElement, useEffect, useState } from 'react';
import { Box } from '@mui/material';
import Navbar from '../../compontents/navbar/navbar';
import styles from './baseLayout.styles';
import { useCheckTokenQuery } from '../../slices/checkTokenExpiration/checkTokenExpiration';
import { useNavigate } from 'react-router';
import RoutingConstants from '../../../app/routing/routingConstants';
import { DeleteUserData, GetUserData } from '../../services/CookieService';
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
    const userData = GetUserData();
    if (!userData || !userData?.id || !userData?.token) navigate(RoutingConstants.authBasic);
    setToken(userData?.token);
    const interval = setInterval(() => {
      refetch();
    }, 30000);
    return () => clearInterval(interval);
  }, []);

  useEffect(() => {
    console.log('CHECK TOKEN USE EFFECT');
    console.log(isTokenValid);
    console.log('---');
    if (!isTokenValid && isTokenValid !== undefined) {
      DeleteUserData();
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
