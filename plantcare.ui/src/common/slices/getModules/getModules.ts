import emptyApi from '../../../app/api/emptyApi';
import { Module } from '../../models/Module';
import { GetToken } from '../../services/CookieService';
export const getModulesApi = emptyApi.injectEndpoints({
  endpoints: (build) => ({
    GetModules: build.query<Module[], void>({
      query: () => ({
        url: `/modules/get`,
        headers: { Authorization: GetToken() }
      })
    })
  }),
  overrideExisting: false
});

export const { useGetModulesQuery } = getModulesApi;
