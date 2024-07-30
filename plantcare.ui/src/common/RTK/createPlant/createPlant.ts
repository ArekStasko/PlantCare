import plantcareApi from '../../../app/api/plantcareApi';
import { CreatePlantRequest } from './createPlantRequest';

export const createPlantApi = plantcareApi.injectEndpoints({
  endpoints: (build) => ({
    CreatePlant: build.mutation<boolean, CreatePlantRequest>({
      query: ({ ...data }) => ({
        url: '/plants/Create',
        method: 'POST',
        body: data,
      })
    })
  }),
  overrideExisting: false
});

export const { useCreatePlantMutation } = createPlantApi;
