import plantcareApi from "../../../app/api/plantcareApi";
import { Place } from "@arekstasko/plantcare-api-client";
import emptyApi from "../emptyApi";


const getDistributors = () =>
  plantcareApi
    .d()
    .then((result) => ({
      data: result ?? ([] as Place[])
    }))
    .catch((err) => ({
      error: err
    }));


export const DistributorApi = emptyApi.injectEndpoints({
  endpoint: (build) => ({
    getDistributors: build.query<>()
  })
})