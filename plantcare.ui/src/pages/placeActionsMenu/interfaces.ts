import { Place } from '../../common/models/Place';
import { ActionType } from '../../common/interfaces';

export interface ActionsMenuContext {
  action?: ActionType;
  place: Place;
}
