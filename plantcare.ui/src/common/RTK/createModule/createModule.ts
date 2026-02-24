import { CreateModuleRequest } from '@arekstasko/plantcare-api-client';
import plantcareApi from '../../../app/api/plantcareApi';

export const createModuleApi = plantcareApi.injectEndpoints({
  endpoints: (build) => ({
    CreateModule: build.mutation<number, CreateModuleRequest>({
      query: ({ ...data }) => ({
        url: '/modules',
        method: 'POST',
        body: data
      }),
      invalidatesTags: ['Modules']
    })
  }),
  overrideExisting: false
});

export const { useCreateModuleMutation } = createModuleApi;
