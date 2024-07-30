import { configureStore } from '@reduxjs/toolkit';
import plantcareApi from '../api/plantcareApi';
import routeReducer from '../../common/RTK/routeSlice/routeSlice';
import idpApi from '../api/idpApi';

const store = configureStore({
  reducer: {
    [plantcareApi.reducerPath]: plantcareApi.reducer,
    [idpApi.reducerPath]: idpApi.reducer,
    route: routeReducer
  },
  middleware: (getDefaultMiddleware) =>
    getDefaultMiddleware().concat(plantcareApi.middleware, idpApi.middleware)
});

export default store;
