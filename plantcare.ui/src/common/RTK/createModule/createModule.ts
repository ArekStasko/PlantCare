import plantcareApi from '../../../app/api/plantcareApi';

export interface CreateModuleRequest {
  name: string;
}

export const createModuleApi = plantcareApi.injectEndpoints({
  endpoints: (build) => ({
    CreateModule: build.mutation<number, CreateModuleRequest>({
      query: ({ ...data }) => ({
        url: '/modules/create',
        method: 'POST',
        body: data
      })
    })
  }),
  overrideExisting: false
});

export const { useCreateModuleMutation } = createModuleApi;
