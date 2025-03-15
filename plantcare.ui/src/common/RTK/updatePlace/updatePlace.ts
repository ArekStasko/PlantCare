import plantcareApi from '../../../app/api/plantcareApi';
import { UpdatePlaceRequest } from './updatePlaceRequest';

export const updatePlaceApi = plantcareApi.injectEndpoints({
  endpoints: (build) => ({
    UpdatePlace: build.mutation<boolean, UpdatePlaceRequest>({
      query: ({ ...data }) => ({
        url: '/places/Update',
        method: 'POST',
        body: data
      })
    })
  }),
  overrideExisting: false
});

export const { useUpdatePlaceMutation } = updatePlaceApi;
