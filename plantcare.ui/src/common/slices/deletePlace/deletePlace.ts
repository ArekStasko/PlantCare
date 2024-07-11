import emptyApi from '../../../app/api/emptyApi';
import { DeletePlaceData } from './deletePlaceData';
import { GetToken } from '../../services/CookieService';

export const deletePlaceApi = emptyApi.injectEndpoints({
  endpoints: (build) => ({
    DeletePlace: build.mutation<boolean, DeletePlaceData>({
      query: ({ placeId }: DeletePlaceData) => ({
        url: `/places/Delete?id=${placeId}`,
        method: 'DELETE',
        headers: { Authorization: GetToken() }
      })
    })
  }),
  overrideExisting: false
});

export const { useDeletePlaceMutation } = deletePlaceApi;
