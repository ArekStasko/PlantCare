import { SetModuleStatusCommand } from "@arekstasko/plantcare-api-client";
import plantcareApi from '../../../app/api/plantcareApi';

export const setModuleStatusApi = plantcareApi.injectEndpoints({
  endpoints: (build) => ({
    SetModuleStatus: build.mutation<boolean, SetModuleStatusCommand>({
      query: ({ ...data }) => ({
        url: '/modules/status',
        method: 'POST',
        body: data
      }),
      invalidatesTags: ['Modules']
    })
  }),
  overrideExisting: false
});

export const { useSetModuleStatusMutation } = setModuleStatusApi;
