import emptyApi from "../../../app/api/emptyApi";
import {UpdatePlantRequest} from "./updatePlantQuery";

export const updatePlantApi = emptyApi.injectEndpoints({
    endpoints: build => ({
        UpdatePlant: build.mutation<boolean, UpdatePlantRequest>({
            query: ({ ...data }) => ({
                url: "/plants/Edit",
                method: "POST",
                body: data,
            })
        })
    }),
    overrideExisting: false
});

export const {useUpdatePlantMutation} = updatePlantApi;