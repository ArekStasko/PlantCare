import {configureStore} from "@reduxjs/toolkit";
import emptyApi from "../api/emptyApi";

const store = configureStore({
    reducer: {
        [emptyApi.reducerPath]: emptyApi.reducer,
    }
})

export default store;