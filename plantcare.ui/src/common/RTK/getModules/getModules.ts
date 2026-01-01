import plantcareApi from '../../../app/api/plantcareApi';
import { Module } from '../../models/Module';
export const getModulesApi = plantcareApi.injectEndpoints({
  endpoints: (build) => ({
    GetModules: build.query<Module[], void>({
      query: () => ({
        url: `/modules/get`
      }),
      providesTags: ['Modules']
    })
  }),
  overrideExisting: false
});

export const { useGetModulesQuery } = getModulesApi;
