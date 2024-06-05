import emptyApi from '../../../app/api/emptyApi';
import { DeletePlaceData } from './deletePlaceData';

export const deletePlaceApi = emptyApi.injectEndpoints({
  endpoints: (build) => ({
    DeletePlace: build.mutation<boolean, DeletePlaceData>({
      query: ({ placeId, userId }: DeletePlaceData) => ({
        url: `/places/Delete?id=${placeId}&userId=${userId}`,
        method: 'DELETE'
      })
    })
  }),
  overrideExisting: false
});

export const { useDeletePlaceMutation } = deletePlaceApi;
