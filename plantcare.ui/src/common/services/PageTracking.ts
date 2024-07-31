import { useEffect } from 'react';
import { useLocation, useNavigate } from 'react-router';
import { useRefreshTokenMutation } from '../RTK/refreshToken/refreshToken';
import RoutingConstants from '../../app/routing/routingConstants';
import { GetToken } from './CookieService';
import { useDispatch, useSelector } from "react-redux";
import { RootState } from "../../app/store/store";

const usePageTracking = () => {
  const location = useLocation();
  const navigate = useNavigate();
  const [refreshToken, { isLoading }] = useRefreshTokenMutation();
  const isAuth = useSelector((state: RootState) => {
    console.log(state)
    return state.auth.isAuthenticated;
  });

  useEffect(() => {
    console.log(isAuth)
    if (location.pathname == RoutingConstants.authBasic || isLoading) return;
    if(!isAuth) {
      navigate(RoutingConstants.authBasic);
      return;
    }
    const token = GetToken();
    if (!token) {
      navigate(RoutingConstants.authBasic);
      return;
    }
    const runRefreshToken = async () => await refreshToken().unwrap();
    runRefreshToken().catch(console.error);
  }, [location]);
};

export default usePageTracking;
