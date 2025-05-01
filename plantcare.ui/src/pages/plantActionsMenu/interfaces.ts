import { Plant } from '../../common/models/Plant';

export enum ActionType {
  UPDATE = 'UPDATE',
  DELETE = 'DELETE'
}

export interface ActionsMenuContext {
  action?: ActionType;
  plant: Plant;
}
