import { configureStore } from '@reduxjs/toolkit';
import emptyApi from '../api/emptyApi';
import routeReducer from '../../common/slices/routeSlice/routeSlice';

const store = configureStore({
  reducer: {
    [emptyApi.reducerPath]: emptyApi.reducer,
    route: routeReducer
  },
  middleware: (getDefaultMiddleware) => getDefaultMiddleware().concat(emptyApi.middleware)
});

export default store;
