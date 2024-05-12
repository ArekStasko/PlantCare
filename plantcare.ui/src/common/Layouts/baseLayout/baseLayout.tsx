import React, { FunctionComponent, ReactElement, useEffect, useState } from 'react';
import { Box, Container, Typography } from '@mui/material';
import Navbar from '../../compontents/navbar/navbar';
import styles from './baseLayout.styles';
import { useCheckTokenQuery } from '../../slices/checkTokenExpiration/checkTokenExpiration';
import { useNavigate } from 'react-router';
import RoutingConstants from '../../../app/routing/routingConstants';
import { DeleteUserData, GetUserData } from '../../services/CookieService';

type BaseLayoutProps = {
  children: ReactElement;
};

export const BaseLayout: FunctionComponent<BaseLayoutProps> = ({ children }) => {
  const navigate = useNavigate();
  const [token, setToken] = useState<string | undefined>(undefined);
  const { data: isTokenValid, refetch } = useCheckTokenQuery(token!, { skip: !token });

  useEffect(() => {
    console.log('pre interval');
    const userData = GetUserData();
    if (!userData) navigate(RoutingConstants.auth);
    setToken(userData?.token);
    const interval = setInterval(() => {
      console.log('interval');
      refetch();
      if (!isTokenValid && isTokenValid !== undefined) {
        DeleteUserData();
        navigate(RoutingConstants.auth);
      }
    }, 10000);
    return () => clearInterval(interval);
  }, []);

  return (
    <Box sx={styles.container}>
      <Navbar />
      <Box>{children}</Box>
    </Box>
  );
};

export default BaseLayout;
