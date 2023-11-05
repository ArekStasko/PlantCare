import emptyApi from '../../../app/api/emptyApi';
import { CreatePlantRequest } from './createPlantRequest';

export const createPlantApi = emptyApi.injectEndpoints({
  endpoints: (build) => ({
    CreatePlant: build.mutation<boolean, CreatePlantRequest>({
      query: ({ ...data }) => ({
        url: '/plants/Create',
        method: 'POST',
        body: data
      })
    })
  }),
  overrideExisting: false
});

export const { useCreatePlantMutation } = createPlantApi;
