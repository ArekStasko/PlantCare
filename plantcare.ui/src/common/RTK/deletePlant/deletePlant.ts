import plantcareApi from '../../../app/api/plantcareApi';
import { DeletePlantData } from './deletePlantData';

export const deletePlantApi = plantcareApi.injectEndpoints({
  endpoints: (build) => ({
    DeletePlant: build.mutation<boolean, DeletePlantData>({
      query: ({ plantId }: DeletePlantData) => ({
        url: `/plants/Delete?id=${plantId}`,
        method: 'DELETE'
      }),
      invalidatesTags: ['Plants']
    })
  }),
  overrideExisting: false
});

export const { useDeletePlantMutation } = deletePlantApi;
