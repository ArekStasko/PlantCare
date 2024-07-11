import { PlantType } from '../../models/plantTypes';

export class CreatePlantRequest {
  name!: string;
  description!: string;
  type!: PlantType;
  placeId!: string;
  moduleId!: string;
}
