import plantcareApi from '../../../app/api/plantcareApi';
import { GetModuleResponse } from '@arekstasko/plantcare-api-client';

export const getModuleApi = plantcareApi.injectEndpoints({
  endpoints: (build) => ({
    GetModule: build.query<GetModuleResponse, string>({
      query: (moduleId: string) => ({
        url: `/modules/${moduleId}`
      }),
      providesTags: ['Modules']
    })
  }),
  overrideExisting: false
});

export const { useGetModuleQuery } = getModuleApi;
