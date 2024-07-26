import { useEffect } from 'react';
import { useLocation, useNavigate } from 'react-router';
import { useRefreshTokenMutation } from '../RTK/refreshToken/refreshToken';
import RoutingConstants from '../../app/routing/routingConstants';
import { GetToken } from './CookieService';

const usePageTracking = () => {
  const location = useLocation();
  const navigate = useNavigate();
  const [refreshToken, { isLoading }] = useRefreshTokenMutation();

  useEffect(() => {
    if (location.pathname == RoutingConstants.authBasic || isLoading) return;
    const token = GetToken();
    if (!token) {
      navigate(RoutingConstants.authBasic);
    }
    const runRefreshToken = async () => await refreshToken().unwrap();
    runRefreshToken().catch(console.error);
  }, [location]);
};

export default usePageTracking;
