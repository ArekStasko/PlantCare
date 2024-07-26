import { configureStore } from '@reduxjs/toolkit';
import emptyApi from '../api/emptyApi';
import routeReducer from '../../common/RTK/routeSlice/routeSlice';
import idpApi from '../api/idpApi';

const store = configureStore({
  reducer: {
    [emptyApi.reducerPath]: emptyApi.reducer,
    [idpApi.reducerPath]: idpApi.reducer,
    route: routeReducer
  },
  middleware: (getDefaultMiddleware) =>
    getDefaultMiddleware().concat(emptyApi.middleware, idpApi.middleware)
});

export default store;
