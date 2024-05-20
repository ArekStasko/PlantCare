import { useEffect } from 'react';
import { useLocation, useNavigate } from 'react-router';
import { useRefreshTokenMutation } from '../slices/refreshToken/refreshToken';
import RoutingConstants from '../../app/routing/routingConstants';
import { GetUserData, SaveUserData, UserData } from './CookieService';

const usePageTracking = () => {
  const location = useLocation();
  const navigate = useNavigate();
  const [refreshToken, { isLoading }] = useRefreshTokenMutation();

  useEffect(() => {
    console.log(location);
    if (location.pathname == RoutingConstants.authBasic || isLoading) return;
    const userData = GetUserData();
    if (!userData) {
      navigate(RoutingConstants.authBasic);
    }
    const runRefreshToken = async () => {
      const data = await refreshToken(userData!.token);
    };
    runRefreshToken().catch();
  }, [location]);
};

export default usePageTracking;
