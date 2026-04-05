import {
  CreatePlaceCommand, Place,
  UpdatePlaceCommand
} from "@arekstasko/plantcare-api-client";
import plantcareApi from '../../../app/api/plantcareApi';
import emptyApi from '../emptyApi';

const updatePlace = async (request: UpdatePlaceCommand) =>
  plantcareApi
    .placesPUT(request)
    .then((result) => ({
      data: result ?? false
    }))
    .catch((err) => ({
      error: err
    }));

const getPlaces = () =>
  plantcareApi
    .placesAll()
    .then((result) => ({
      data: result ?? ([] as Place[])
    }))
    .catch((err) => ({
      error: err
    }));

const deletePlace = (id: number) =>
  plantcareApi
    .placesDELETE(id)
    .then((result) => ({
      data: result ?? false
    }))
    .catch((err) => ({
      error: err
    }));

const createPlace = (request: CreatePlaceCommand) =>
  plantcareApi
    .placesPOST(request)
    .then((result) => ({
      data: result ?? false
    }))
    .catch((err) => ({
      error: err
    }));

export const PlaceApi = emptyApi.injectEndpoints({
  endpoints: (build) => ({
    updatePlace: build.mutation<boolean, UpdatePlaceCommand>({
      queryFn: updatePlace,
      invalidatesTags: ['Places']
    }),
    getPlaces: build.query<Place[], void>({
      queryFn: getPlaces,
      providesTags: ['Places']
    }),
    deletePlace: build.mutation<boolean, number>({
      queryFn: deletePlace,
      invalidatesTags: ['Places']
    }),
    createPlace: build.mutation<boolean, CreatePlaceCommand>({
      queryFn: createPlace,
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
