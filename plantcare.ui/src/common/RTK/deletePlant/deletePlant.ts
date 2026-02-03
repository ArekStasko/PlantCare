import plantcareApi from '../../../app/api/plantcareApi';

export const deletePlantApi = plantcareApi.injectEndpoints({
  endpoints: (build) => ({
    DeletePlant: build.mutation<boolean, number>({
      query: ({ plantId }: number) => ({
        url: `/plants?id=${plantId}`,
        method: 'DELETE'
      }),
      invalidatesTags: ['Plants']
    })
  }),
  overrideExisting: false
});

export const { useDeletePlantMutation } = deletePlantApi;
