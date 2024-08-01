import { createSlice } from "@reduxjs/toolkit";
import routingConstants from "../../app/routing/routingConstants";
import { DeleteToken, SaveToken } from "../services/CookieService";

export type AuthSliceState = { isAuthenticated: boolean};
const initialState = {isAuthenticated: false} as AuthSliceState;

const authSlice = createSlice({
  name: "auth",
  initialState,
  reducers: {
    login(state, action) {
      SaveToken(action.payload.token);
      state.isAuthenticated = true;
    },
    logout(state) {
      DeleteToken();
      state.isAuthenticated = false;
      window.location.pathname = routingConstants.authBasic
    }
  }
})

export const {login, logout} = authSlice.actions;
export default authSlice.reducer