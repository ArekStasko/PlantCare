import { useEffect } from 'react';
import { useLocation, useNavigate } from 'react-router';
import { useRefreshTokenMutation } from '../slices/refreshToken/refreshToken';
import RoutingConstants from '../../app/routing/routingConstants';
import { GetUserData, UserData } from './CookieService';

const usePageTracking = () => {
  const location = useLocation();
  const navigate = useNavigate();
  const [refreshToken, { isLoading }] = useRefreshTokenMutation();

  useEffect(() => {
    if (location.pathname == RoutingConstants.authBasic || isLoading) return;
    const userData = GetUserData();
    if (!userData) {
      navigate(RoutingConstants.authBasic);
    }
    refresh(userData!);
  }, [location]);

  const refresh = async (userData: UserData) => {
    await refreshToken(userData!.token).unwrap();
  };
};

export default usePageTracking;
