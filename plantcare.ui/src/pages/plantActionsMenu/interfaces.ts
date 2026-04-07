import { ActionType } from '../../common/interfaces';
import { Plant } from '@arekstasko/plantcare-api-client';

export interface ActionsMenuContext {
  action?: ActionType;
  plant: Plant;
}
