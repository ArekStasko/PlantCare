import plantcareApi from '../../../app/api/plantcareApi';
import emptyApi from '../emptyApi';
import { CreateModuleRequest, Module } from '@arekstasko/plantcare-api-client';

const getModules = async () =>
  plantcareApi
    .modulesAll()
    .then((result) => ({
      data: result
    }))
    .catch((err) => ({
      error: err
    }));

const getModule = async (id: number) =>
  plantcareApi
    .modulesGET(id)
    .then((result) => ({
      data: result
    }))
    .catch((err) => ({
      error: err
    }));

const createModule = async (request: CreateModuleRequest) =>
  plantcareApi
    .modulesPOST(request)
    .then((result) => ({
      data: result ?? false
    }))
    .catch((err) => ({
      error: err
    }));

const getBatteryLevel = async (id: number) =>
  plantcareApi
    .batteryLevel(id)
    .then((result) => ({
      data: result
    }))
    .catch((err) => ({
      error: err
    }));

export const ModulesApi = emptyApi.injectEndpoints({
  endpoints: (build) => ({
    getModules: build.query<Module[], void>({
      queryFn: getModules,
      providesTags: ['Modules']
    }),
    getModule: build.query<Module, number>({
      queryFn: getModule,
      providesTags: ['Modules']
    }),
    getBatteryLevel: build.query<number, number>({
      queryFn: getBatteryLevel,
      providesTags: ['Modules']
    }),
    createModule: build.mutation<boolean, CreateModuleRequest>({
      queryFn: createModule,
      invalidatesTags: ['Modules']
    })
  }),
  overrideExisting: false
});

export const { useGetModulesQuery, useGetModuleQuery, useCreateModuleMutation, useGetBatteryLevelQuery } = ModulesApi;
