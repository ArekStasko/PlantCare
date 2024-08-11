import { configureStore } from '@reduxjs/toolkit';
import plantcareApi from '../api/plantcareApi';

const store = configureStore({
  reducer: {
    [plantcareApi.reducerPath]: plantcareApi.reducer,
  },
  middleware: (getDefaultMiddleware) =>
    getDefaultMiddleware().concat(plantcareApi.middleware)
});

export default store;
