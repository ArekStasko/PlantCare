import emptyApi from '../../../app/api/emptyApi';
import { Module } from '../../models/Module';
export const getModulesApi = emptyApi.injectEndpoints({
  endpoints: (build) => ({
    GetModules: build.query<Module[], string>({
      query: (userId) => `/modules/get?userId=${userId}`
    })
  }),
  overrideExisting: false
});

export const { useGetModulesQuery } = getModulesApi;
