import emptyApi from '../emptyApi';
import { CreatePlantCommand } from '@arekstasko/plantcare-api-client';
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

export const PlantApi = emptyApi.injectEndpoints({
  endpoints: (build) => ({
    CreatePlant: build.mutation<boolean, CreatePlantCommand>({
      queryFn: createPlant,
      invalidatesTags: ['Plants']
    }),
    DeletePlant: build.mutation<boolean, number>({
      queryFn: deletePlant,
      invalidatesTags: ['Plants']
    })
  }),
  overrideExisting: false
});

export const { useCreatePlantMutation, useDeletePlantMutation } = PlantApi;
