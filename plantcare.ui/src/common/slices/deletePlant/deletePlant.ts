import emptyApi from '../../../app/api/emptyApi';
import { DeletePlantData } from './deletePlantData';
import { GetToken } from '../../services/CookieService';

export const deletePlantApi = emptyApi.injectEndpoints({
  endpoints: (build) => ({
    DeletePlant: build.mutation<boolean, DeletePlantData>({
      query: ({ plantId }: DeletePlantData) => ({
        url: `/plants/Delete?id=${plantId}`,
        method: 'DELETE',
        headers: { Authorization: GetToken() }
      })
    })
  }),
  overrideExisting: false
});

export const { useDeletePlantMutation } = deletePlantApi;
