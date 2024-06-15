import emptyApi from '../../../app/api/emptyApi';
import { DeletePlantData } from './deletePlantData';

export const deletePlantApi = emptyApi.injectEndpoints({
  endpoints: (build) => ({
    DeletePlant: build.mutation<boolean, DeletePlantData>({
      query: ({ plantId, userId }: DeletePlantData) => ({
        url: `/plants/Delete?id=${plantId}&userId=${userId}`,
        method: 'DELETE'
      })
    })
  }),
  overrideExisting: false
});

export const { useDeletePlantMutation } = deletePlantApi;
