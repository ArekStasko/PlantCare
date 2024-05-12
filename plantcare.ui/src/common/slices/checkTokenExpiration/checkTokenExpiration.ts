import emptyApi from '../../../app/api/emptyApi';
import { Module } from '../../models/Module';
import idpApi from '../../../app/api/idpApi';
export const checkTokenExpirationApi = idpApi.injectEndpoints({
  endpoints: (build) => ({
    checkToken: build.query<boolean, string>({
      query: (token: string) => `/token/checkTokenExp?token=${token}`
    })
  }),
  overrideExisting: false
});

export const { useCheckTokenQuery } = checkTokenExpirationApi;
