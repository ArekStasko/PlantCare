import emptyApi from '../../../app/api/emptyApi';
import { Place } from '../../models/Place';

export const getPlacesApi = emptyApi.injectEndpoints({
  endpoints: (build) => ({
    GetPlaces: build.query<Place[], void>({
      query: () => ({
        url : '/places/get'
      })
    })
  }),
  overrideExisting: false
});

export const { useGetPlacesQuery } = getPlacesApi;
