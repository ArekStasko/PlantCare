import { CreatePlaceCommand, UpdatePlaceCommand } from '@arekstasko/plantcare-api-client';
import plantcareApi from '../../../app/api/plantcareApi';
import emptyApi from '../emptyApi';
import { Place } from '../../models/Place';

const updatePlace = async (request: UpdatePlaceCommand) =>
  plantcareApi
    .placesPUT(request)
    .then((result) => ({
      data: result,
      error: null
    }))
    .catch((err) => ({
      data: null,
      error: err
    }));

const getPlaces = () =>
  plantcareApi
    .placesAll()
    .then((result) => ({
      data: result,
      error: null
    }))
    .catch((err) => ({
      data: null,
      error: err
    }));

const deletePlace = (id: number) =>
  plantcareApi
    .placesDELETE(id)
    .then((result) => ({
      data: result,
      error: null
    }))
    .catch((err) => ({
      data: null,
      error: err
    }));

const createPlace = (request: CreatePlaceCommand) =>
  plantcareApi
    .placesPOST(request)
    .then((result) => ({
      data: result,
      error: null
    }))
    .catch((err) => ({
      data: null,
      error: err
    }));

export const PlaceApi = emptyApi.injectEndpoints({
  endpoints: (build) => ({
    UpdatePlace: build.mutation<boolean, UpdatePlaceCommand>({
      queryFn: updatePlace,
      invalidatesTags: ['Places']
    }),
    GetPlaces: build.query<Place[], void>({
      query: getPlaces,
      providesTags: ['Places']
    }),
    DeletePlaces: build.mutation<boolean, number>({
      query: deletePlace,
      invalidatesTags: ['Places']
    }),
    CreatePlace: build.mutation<boolean, CreatePlaceCommand>({
      query: createPlace,
      invalidatesTags: ['Places']
    })
  }),
  overrideExisting: false
});

export const {
  useUpdatePlaceMutation,
  useGetPlacesQuery,
  useDeletePlaceMutation,
  useCreatePlaceMutation
} = PlaceApi;
