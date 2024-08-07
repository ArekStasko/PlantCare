import React, { useEffect } from 'react';
import { Box, Button} from '@mui/material';
import { useNavigate, useParams } from 'react-router';
import routingConstants from '../../app/routing/routingConstants';
import styles from './authPage.styles';
import { useDispatch } from "react-redux";
import RoutingConstants from "../../app/routing/routingConstants";

export const AuthPage = () => {
  const navigate = useNavigate();
  const { id, token } = useParams();
  const dispatch = useDispatch();

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
