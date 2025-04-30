import plantcareApi from '../../../app/api/plantcareApi';
import { UpdatePlantRequest } from './updatePlantRequest';

export const updatePlantApi = plantcareApi.injectEndpoints({
  endpoints: (build) => ({
    UpdatePlant: build.mutation<boolean, UpdatePlantRequest>({
      query: ({ ...data }) => ({
        url: '/plants/Update',
        method: 'POST',
        body: data
      }),
      invalidatesTags: ['Plants']
    })
  }),
  overrideExisting: false
});

export const { useUpdatePlantMutation } = updatePlantApi;
