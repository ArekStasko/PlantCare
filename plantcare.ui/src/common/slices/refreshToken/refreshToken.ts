import idpApi from '../../../app/api/idpApi';

export const refreshTokenApi = idpApi.injectEndpoints({
  endpoints: (build) => ({
    RefreshToken: build.mutation<boolean, void>({
      query: () => ({
        url: `/token/refreshToken?token=${''}`,
        method: 'POST',
        body: {}
      })
    })
  }),
  overrideExisting: false
});

export const { useRefreshTokenMutation } = refreshTokenApi;
