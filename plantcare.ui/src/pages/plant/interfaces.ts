import { PlantType } from '../../common/models/plantTypes';

export enum PlantFlowType {
  UPDATE = 'UPDATE',
  CREATE = 'CREATE'
}

export interface PlantContext {
  flowType: PlantFlowType;
  name?: string;
  description?: string;
  type?: PlantType;
  place?: string;
  placeName?: string;
  module?: string;
}
