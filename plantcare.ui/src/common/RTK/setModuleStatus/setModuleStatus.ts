import { SetModuleStatusRequest } from './setModuleStatusRequest';
import plantcareApi from '../../../app/api/plantcareApi';

export const setModuleStatusApi = plantcareApi.injectEndpoints({
  endpoints: (build) => ({
    SetModuleStatus: build.mutation<boolean, SetModuleStatusRequest>({
      query: ({ ...data }) => ({
        url: '/modules/status',
        method: 'POST',
        body: data
      })
    })
  }),
  overrideExisting: false
});

export const { useSetModuleStatusMutation } = setModuleStatusApi;
