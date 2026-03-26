import plantcareApi from "../../../app/api/plantcareApi";
import { HumidityMeasurement } from "../../models/HumidityMeasurement";
import emptyApi from "../emptyApi";
import { AverageHumidity } from "@arekstasko/plantcare-api-client";

export class GetHumidityMeasurementsApiParameters {
  moduleId!: number;
  fromDate!: Date | undefined;
  toDate!: Date | undefined;
}

const getHumidityMeasurements = async (params: GetHumidityMeasurementsApiParameters) =>
  plantcareApi
    .humidityMeasurementsAll(params.moduleId, params.fromDate, params.toDate)
    .then((result) => ({
      data: result
    }))
    .catch((error) => ({
      error: error
    }));

const getAverageHumidityMeasurements = async (params: GetHumidityMeasurementsApiParameters) =>
  plantcareApi
    .average(params.moduleId, params.fromDate, params.toDate)
    .then((result) => ({
      data: result
    }))
    .catch((error) => ({
      error: error
    }));

export const HumidityMeasurementsApi = emptyApi.injectEndpoints({
  endpoints: (build) => ({
    GetHumidityMeasurements: build.query<
      HumidityMeasurement[],
      GetHumidityMeasurementsApiParameters
    >({
      queryFn: getHumidityMeasurements
    }),
    GetAverageHumidityMeasurements: build.query<
      AverageHumidity[],
      GetHumidityMeasurementsApiParameters
    >({
      queryFn: getAverageHumidityMeasurements
    })
  }),
  overrideExisting: false
});

export const { useGetHumidityMeasurementsQuery, useGetAverageHumidityMeasurementsQuery } = HumidityMeasurementsApi;
