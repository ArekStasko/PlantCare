import { configureStore } from '@reduxjs/toolkit';
import plantcareApi from '../api/plantcareApi';
import routeReducer from '../../common/RTK/routeSlice/routeSlice';
import authReducer, { AuthSliceState } from "../../common/slices/authSlice";
import idpApi from '../api/idpApi';

export interface RootState {
  auth: AuthSliceState
}

const store = configureStore({
  reducer: {
    [plantcareApi.reducerPath]: plantcareApi.reducer,
    [idpApi.reducerPath]: idpApi.reducer,
    route: routeReducer,
    auth: authReducer
  },
  middleware: (getDefaultMiddleware) =>
    getDefaultMiddleware().concat(plantcareApi.middleware, idpApi.middleware)
});

export default store;
