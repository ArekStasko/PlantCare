import React from 'react';
import { Box, Button } from '@mui/material';
import routingConstants from '../../app/routing/routingConstants';
import styles from './authPage.styles';

export const AuthPage = () => {
  const authorize = () => {
    const site = btoa(routingConstants.site);
    const url = `${routingConstants.idp}/${site}`;
    window.location.href = url;
  };

  return (
    <Box sx={styles.authContainer}>
      <Box sx={styles.authNavbar}>
        <Button sx={styles.authBtn} variant="contained" onClick={() => authorize()}>
          Authorize
        </Button>
      </Box>
    </Box>
  );
};

export default AuthPage;
