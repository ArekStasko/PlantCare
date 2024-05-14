import idpApi from '../../../app/api/idpApi';

export const refreshTokenApi = idpApi.injectEndpoints({
  endpoints: (build) => ({
    RefreshToken: build.mutation<boolean, string>({
      query: (token) => ({
        url: `/token/refreshToken?token=${token}`,
        method: 'POST',
        body: null
      })
    })
  }),
  overrideExisting: false
});

export const { useRefreshTokenMutation } = refreshTokenApi;
