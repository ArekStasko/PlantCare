import { Plant } from '../../common/models/Plant';
import { ActionType } from '../../common/interfaces';

export interface ActionsMenuContext {
  action?: ActionType;
  plant: Plant;
}
