import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import RoutingConstants from '../../../app/routing/routingConstants';

interface routeState {
  currentRoute: string;
}

const initialState: routeState = {
  currentRoute: RoutingConstants.root
};

const routeSlice = createSlice({
  name: 'route',
  initialState,
  reducers: {
    update: (state, action: PayloadAction<string>) => {
      state.currentRoute = action.payload;
    }
  }
});

export const { update } = routeSlice.actions;
export default routeSlice.reducer;
