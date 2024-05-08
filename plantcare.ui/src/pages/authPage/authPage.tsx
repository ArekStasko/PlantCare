import React, { useEffect } from 'react';
import { Box, Button, Typography } from '@mui/material';
import { useNavigate, useParams } from 'react-router';
import { SaveUserData, UserData } from '../../common/services/CookieService';
import routingConstants from '../../app/routing/routingConstants';
import styles from './authPage.styles';

export const AuthPage = () => {
  const navigate = useNavigate();
  const { id, token } = useParams();

  useEffect(() => {
    if (!id || !token) return;
    const userData = {
      id,
      token
    } as UserData;
    SaveUserData(userData);
    navigate(routingConstants.root);
  }, []);

  const authorize = () => {
    const site = btoa(routingConstants.site);
    const url = `${routingConstants.idp}/${site}`;
    window.location.href = url;
  };

  return (
    <Box sx={styles.authContainer}>
      {(!id || !token) && (
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
