import emptyApi from '../../../app/api/emptyApi';
import { UpdatePlantRequest } from './updatePlantRequest';

export const updatePlantApi = emptyApi.injectEndpoints({
  endpoints: (build) => ({
    UpdatePlant: build.mutation<boolean, UpdatePlantRequest>({
      query: ({ ...data }) => ({
        url: '/plants/Update',
        method: 'POST',
        body: data
      })
    })
  }),
  overrideExisting: false
});

export const { useUpdatePlantMutation } = updatePlantApi;
