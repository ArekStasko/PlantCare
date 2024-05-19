import React, { useEffect, useState } from 'react';
import { Box, Button, Typography } from '@mui/material';
import { useNavigate, useParams } from 'react-router';
import { SaveUserData, UserData } from '../../common/services/CookieService';
import routingConstants from '../../app/routing/routingConstants';
import styles from './authPage.styles';
import { useCheckTokenQuery } from '../../common/slices/checkTokenExpiration/checkTokenExpiration';

export const AuthPage = () => {
  const navigate = useNavigate();
  const { id, token } = useParams();
  const { data: isTokenValid, isLoading } = useCheckTokenQuery(token!, { skip: !token });

  useEffect(() => {
    if (!id || !token) return;
    if (!isTokenValid) return;
    const userData = {
      id,
      token
    } as UserData;
    console.log(userData);
    SaveUserData(userData);
    navigate(routingConstants.root);
  }, [isTokenValid]);

  const authorize = () => {
    const site = btoa(routingConstants.site);
    const url = `${routingConstants.idp}/${site}`;
    window.location.href = url;
  };

  return (
    <Box sx={styles.authContainer}>
      {(!id || !token || isLoading!) && (
        <Box sx={styles.authNavbar}>
          <Button sx={styles.authBtn} variant="contained" onClick={() => authorize()}>
            Authorize
          </Button>
        </Box>
      )}
    </Box>
  );
};

export default AuthPage;
