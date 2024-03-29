import emptyApi from '../../../app/api/emptyApi';

export const deletePlantApi = emptyApi.injectEndpoints({
  endpoints: (build) => ({
    DeletePlant: build.mutation<boolean, number>({
      query: (plantId: number) => ({
        url: `/plants/Delete?id=${plantId}`,
        method: 'DELETE'
      })
    })
  }),
  overrideExisting: false
});

export const { useDeletePlantMutation } = deletePlantApi;
