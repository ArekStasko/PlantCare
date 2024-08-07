import { configureStore } from '@reduxjs/toolkit';
import plantcareApi from '../api/plantcareApi';
import routeReducer from '../../common/RTK/routeSlice/routeSlice';

const store = configureStore({
  reducer: {
    [plantcareApi.reducerPath]: plantcareApi.reducer,
    route: routeReducer,
  },
  middleware: (getDefaultMiddleware) =>
    getDefaultMiddleware().concat(plantcareApi.middleware)
});

export default store;
