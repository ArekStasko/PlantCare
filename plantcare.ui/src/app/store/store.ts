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

export type RootState = ReturnType<typeof store.getState>;

export type AppDispatch = typeof store.dispatch;

export default store;
