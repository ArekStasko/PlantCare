import emptyApi from '../emptyApi';
import { CreatePlantCommand, Plant, UpdatePlantCommand } from '@arekstasko/plantcare-api-client';
import plantcareApi from '../../../app/api/plantcareApi';

const createPlant = async (request: CreatePlantCommand) =>
  plantcareApi
    .plantsPOST(request)
    .then((result) => ({
      data: result ?? false
    }))
    .catch((err) => ({
      error: err
    }));

const deletePlant = async (id: number) =>
  plantcareApi
    .plantsDELETE(id)
    .then((result) => ({
      data: result ?? false
    }))
    .catch((err) => ({
      error: err
    }));

const getPlant = async (id: number) =>
  plantcareApi
    .plantsGET(id)
    .then((result) => ({
      data: result ?? false
    }))
    .catch((err) => ({
      error: err
    }));

const getPlants = async () =>
  plantcareApi
    .plantsAll()
    .then((result) => ({
      data: result ?? false
    }))
    .catch((err) => ({
      error: err
    }));

const updatePlant = async (request: UpdatePlantCommand) =>
  plantcareApi
    .plantsPUT(request)
    .then((result) => ({
      data: result ?? false
    }))
    .catch((err) => ({
      error: err
    }));

export const PlantApi = emptyApi.injectEndpoints({
  endpoints: (build) => ({
    createPlant: build.mutation<boolean, CreatePlantCommand>({
      queryFn: createPlant,
      invalidatesTags: ['Plants']
    }),
    deletePlant: build.mutation<boolean, number>({
      queryFn: deletePlant,
      invalidatesTags: ['Plants']
    }),
    getPlant: build.query<Plant, number>({
      queryFn: getPlant,
      providesTags: ['Plants']
    }),
    getPlants: build.query<Plant[], void>({
      queryFn: getPlants,
      providesTags: ['Plants']
    }),
    updatePlant: build.mutation<boolean, UpdatePlantCommand>({
      queryFn: updatePlant,
      invalidatesTags: ['Plants', 'Modules']
    })
  }),
  overrideExisting: false
});

export const {
  useCreatePlantMutation,
  useDeletePlantMutation,
  useGetPlantQuery,
  useGetPlantsQuery,
  useUpdatePlantMutation
} = PlantApi;
