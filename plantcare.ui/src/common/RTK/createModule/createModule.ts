import plantcareApi from '../../../app/api/plantcareApi';

export interface CreateModuleRequest {
  name: string
}

export const createModuleApi = plantcareApi.injectEndpoints({
  endpoints: (build) => ({
    CreateModule: build.mutation<number, CreateModuleRequest>({
      query: () => ({
        url: '/modules/create',
        method: 'POST'
      })
    })
  }),
  overrideExisting: false
});

export const { useCreateModuleMutation } = createModuleApi;
