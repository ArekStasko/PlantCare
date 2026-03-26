import emptyApi from '../emptyApi';
import {
  CreatePlantCommand,
  GetPlantResponse,
  UpdatePlantCommand
} from '@arekstasko/plantcare-api-client';
import plantcareApi from '../../../app/api/plantcareApi';
import { request } from 'node:http';

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
    CreatePlant: build.mutation<boolean, CreatePlantCommand>({
      queryFn: createPlant,
      invalidatesTags: ['Plants']
    }),
    DeletePlant: build.mutation<boolean, number>({
      queryFn: deletePlant,
      invalidatesTags: ['Plants']
    }),
    GetPlant: build.query<GetPlantResponse, number>({
      queryFn: getPlant,
      providesTags: ['Plants']
    }),
    GetPlants: build.query<GetPlantResponse[], void>({
      queryFn: getPlants,
      providesTags: ['Plants']
    }),
    UpdatePlant: build.mutation<boolean, UpdatePlantCommand>({
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
  useUpdatePlantQuery
} = PlantApi;
