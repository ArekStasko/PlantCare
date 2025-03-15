import plantcareApi from '../../../app/api/plantcareApi';
import { Place } from '../../models/Place';

export const getPlacesApi = plantcareApi.injectEndpoints({
  endpoints: (build) => ({
    GetPlaces: build.query<Place[], void>({
      query: () => ({
        url: '/places/get'
      })
    })
  }),
  overrideExisting: false
});

export const { useGetPlacesQuery } = getPlacesApi;
