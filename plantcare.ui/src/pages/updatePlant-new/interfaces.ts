import { Plant } from '../../common/models/Plant';

export enum UpdatePlantActionType {
  UPDATE = 'UPDATE',
  DELETE = 'DELETE'
}

export interface UpdatePlantContext {
  action?: UpdatePlantActionType;
  plant: Plant;
}
