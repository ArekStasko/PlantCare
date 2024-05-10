import { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import RoutingConstants from '../../app/routing/routingConstants';
import { useCheckTokenQuery } from '../slices/checkTokenExpiration/checkTokenExpiration';
import { GetUserData } from './CookieService';

const useBackgroundService = () => {
  const navigate = useNavigate();
  const [isTokenValid, setIsTokenValid] = useState<boolean>(true);

  useEffect(() => {
    const bgService = setInterval(async () => {
      const userData = GetUserData();
      if (!userData) {
        clearInterval(bgService);
        navigate(RoutingConstants.auth);
        return;
      }

      const { data: isValid } = useCheckTokenQuery(userData.token);
      setIsTokenValid(isValid ?? false);

      if (!isValid) {
        clearInterval(bgService);
        navigate(RoutingConstants.auth);
      }
    }, 120000);

    return () => clearInterval(bgService);
  }, [navigate]);

  return isTokenValid;
};

export default useBackgroundService;
