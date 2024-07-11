import emptyApi from '../../../app/api/emptyApi';
import { Module } from '../../models/Module';
export const getModulesApi = emptyApi.injectEndpoints({
  endpoints: (build) => ({
    GetModules: build.query<Module[], void>({
      query: () => `/modules/get`
    })
  }),
  overrideExisting: false
});

export const { useGetModulesQuery } = getModulesApi;
