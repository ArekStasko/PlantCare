import plantcareApi from '../../../app/api/plantcareApi';
import { GetPlantResponse } from "@arekstasko/plantcare-api-client";

export const getPlantApi = plantcareApi.injectEndpoints({
  endpoints: (build) => ({
    GetPlant: build.query<GetPlantResponse, string>({
      query: (plantId: string) => ({
        url: `/plants/${plantId}`
      }),
      providesTags: ['Plants']
    })
  }),
  overrideExisting: false
});

export const { useGetPlantQuery } = getPlantApi;
