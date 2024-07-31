import React, { useEffect } from 'react';
import { Box, Button} from '@mui/material';
import { useNavigate, useParams } from 'react-router';
import routingConstants from '../../app/routing/routingConstants';
import styles from './authPage.styles';
import { useCheckTokenQuery } from '../../common/RTK/checkTokenExpiration/checkTokenExpiration';
import { useDispatch } from "react-redux";
import { login } from "../../common/slices/authSlice";
import RoutingConstants from "../../app/routing/routingConstants";

export const AuthPage = () => {
  const navigate = useNavigate();
  const { id, token } = useParams();
  const { data: isTokenValid, isLoading } = useCheckTokenQuery(token!, { skip: !token });
  const dispatch = useDispatch();

  useEffect(() => {
    if (!id || !token) return;
    if (!isTokenValid) return;
    dispatch(login({token: token}))
    navigate(RoutingConstants.root)
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
