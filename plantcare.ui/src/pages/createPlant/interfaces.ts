import { PlantType } from '../../common/models/plantTypes';

export interface CreatePlantContext {
  name?: string;
  description?: string;
  type?: PlantType;
  place?: string;
  placeName?: string;
  module?: string;
}
