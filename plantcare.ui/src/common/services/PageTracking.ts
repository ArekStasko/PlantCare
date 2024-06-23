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
    if (location.pathname == RoutingConstants.authBasic || isLoading) return;
    console.log('lets go');
    const userData = GetUserData();
    if (!userData || !userData.token) {
      navigate(RoutingConstants.authBasic);
    }
    const runRefreshToken = async () => await refreshToken(userData!.token).unwrap();
    runRefreshToken().catch(console.error);
  }, [location]);
};

export default usePageTracking;
