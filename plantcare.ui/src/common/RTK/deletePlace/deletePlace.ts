import plantcareApi from '../../../app/api/plantcareApi';
import { DeletePlaceData } from './deletePlaceData';

export const deletePlaceApi = plantcareApi.injectEndpoints({
  endpoints: (build) => ({
    DeletePlace: build.mutation<boolean, DeletePlaceData>({
      query: ({ placeId }: DeletePlaceData) => ({
        url: `/places/Delete?id=${placeId}`,
        method: 'DELETE',
      })
    })
  }),
  overrideExisting: false
});

export const { useDeletePlaceMutation } = deletePlaceApi;
