import emptyApi from "../../../app/api/emptyApi";

export const deletePlantApi = emptyApi.injectEndpoints({
    endpoints: build => ({
        DeletePlant: build.mutation({
            query: ({ plantId, ...rest }) => ({
                url: `/plants/Delete?id=${plantId}`,
                method: "DELETE"
            })
        })
    }),
    overrideExisting: false
})

export const { useDeletePlantMutation } = deletePlantApi;