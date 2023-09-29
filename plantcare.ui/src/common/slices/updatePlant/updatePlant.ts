import emptyApi from "../../../app/api/emptyApi";

export const updatePlantApi = emptyApi.injectEndpoints({
    endpoints: build => ({
        UpdatePlant: build.mutation({
            query: ({ ...rest }) => ({
                url: "/plants/Edit",
                method: "POST",
                body: rest,
            })
        })
    }),
    overrideExisting: false
});

export const {useUpdatePlantMutation} = updatePlantApi;