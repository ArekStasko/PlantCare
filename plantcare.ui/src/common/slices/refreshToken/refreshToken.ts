import idpApi from '../../../app/api/idpApi';
import { GetToken } from '../../services/CookieService';

export const refreshTokenApi = idpApi.injectEndpoints({
  endpoints: (build) => ({
    RefreshToken: build.mutation<boolean, void>({
      query: () => ({
        url: `/token/refreshToken?token=${GetToken()}`,
        method: 'POST',
        body: {}
      })
    })
  }),
  overrideExisting: false
});

export const { useRefreshTokenMutation } = refreshTokenApi;
