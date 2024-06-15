import emptyApi from '../../../app/api/emptyApi';
import { Place } from '../../models/Place';

export const getPlacesApi = emptyApi.injectEndpoints({
  endpoints: (build) => ({
    GetPlaces: build.query<Place[], string>({
      query: (userId: string) => `/places/get?userId=${userId}`
    })
  }),
  overrideExisting: false
});

export const { useGetPlacesQuery } = getPlacesApi;
