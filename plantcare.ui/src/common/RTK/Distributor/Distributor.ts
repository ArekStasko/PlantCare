import plantcareApi from '../../../app/api/plantcareApi';
import { CreateDistributorRequest, Distributor, Place } from '@arekstasko/plantcare-api-client';
import emptyApi from '../emptyApi';

export type DistributorPlantRequest = {
  id: string;
  plantId: string;
};

const getDistributors = () =>
  plantcareApi
    .distributor()
    .then((result) => ({
      data: result ?? ([] as Distributor[])
    }))
    .catch((err) => ({
      error: err
    }));

const getDistributor = (id: number) =>
  plantcareApi
    .getDistributor(id)
    .then((result) => ({
      data: result
    }))
    .catch((err) => ({
      error: err
    }));

const createDistributor = (request: CreateDistributorRequest) =>
  plantcareApi
    .createDistributor(request)
    .then((result) => ({
      data: result
    }))
    .catch((err) => ({
      error: err
    }));

const waterSupply = (request: DistributorPlantRequest) =>
  plantcareApi
    .waterSupply(request.id, request.plantId)
    .then((result) => ({
      data: result
    }))
    .catch((err) => ({
      error: err
    }));

const addPlantToDistributor = (request: DistributorPlantRequest) =>
  plantcareApi
    .addPlantToDistributor(+request.id, +request.plantId)
    .then((result) => ({
      data: result
    }))
    .catch((err) => ({
      error: err
    }));

export const DistributorApi = emptyApi.injectEndpoints({
  endpoints: (build) => ({
    getDistributors: build.query<Distributor[], void>({
      queryFn: getDistributors,
      providesTags: ['Distributors']
    }),
    getDistributor: build.query<Distributor, number>({
      queryFn: getDistributor,
      providesTags: ['Distributors']
    }),
    createDistributor: build.mutation<number, CreateDistributorRequest>({
      queryFn: createDistributor,
      invalidatesTags: ['Distributors']
    }),
    waterSupply: build.mutation<boolean, DistributorPlantRequest>({
      queryFn: waterSupply,
      invalidatesTags: ['Distributors']
    }),
    addPlantToDistributor: build.mutation<boolean, DistributorPlantRequest>({
      queryFn: addPlantToDistributor,
      invalidatesTags: ['Plants', 'Distributors']
    })
  }),
  overrideExisting: false
});

export const {
  useGetDistributorsQuery,
  useGetDistributorQuery,
  useCreateDistributorMutation,
  useWaterSupplyMutation,
  useAddPlantToDistributorMutation
} = DistributorApi;
