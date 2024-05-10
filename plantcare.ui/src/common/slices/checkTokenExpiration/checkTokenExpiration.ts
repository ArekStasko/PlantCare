import emptyApi from '../../../app/api/emptyApi';
import { Module } from '../../models/Module';
export const checkTokenExpirationApi = emptyApi.injectEndpoints({
  endpoints: (build) => ({
    checkToken: build.query<boolean, string>({
      query: (token: string) => `/token/checkTokenExp?=${token}`
    })
  }),
  overrideExisting: false
});

export const { useCheckTokenQuery } = checkTokenExpirationApi;
