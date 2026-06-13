import plantcareApi from '../../../app/api/plantcareApi';
import { CreateDistributorRequest, Distributor, Place } from '@arekstasko/plantcare-api-client';
import emptyApi from '../emptyApi';

type DistributorPlantRequest = {
  id: string;
  plantId: string;
};

const getDistributors = () =>
  plantcareApi
    .distributorAll()
    .then((result) => ({
      data: result ?? ([] as Distributor[])
    }))
    .catch((err) => ({
      error: err
    }));

const getDistributor = (id: number) =>
  plantcareApi
    .distributorGET(id)
    .then((result) => ({
      data: result
    }))
    .catch((err) => ({
      error: err
    }));

const createDistributor = (request: CreateDistributorRequest) =>
  plantcareApi
    .distributorPOST(request)
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
    createDistributor: build.mutation<boolean, CreateDistributorRequest>({
      queryFn: createDistributor,
      invalidatesTags: ['Distributors']
    }),
    waterSupply: build.mutation<boolean, DistributorPlantRequest>({
      queryFn: waterSupply,
      invalidatesTags: ['Distributors']
    })
  }),
  overrideExisting: false
});

export const {
  useGetDistributorsQuery,
  useGetDistributorQuery,
  useCreateDistributorMutation,
  useWaterSupplyMutation
} = DistributorApi;
