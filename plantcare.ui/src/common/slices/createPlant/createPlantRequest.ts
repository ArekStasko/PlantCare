import { PlantType } from '../../models/plantTypes';

export class CreatePlantRequest {
  name!: string;
  userId!: number;
  description!: string;
  type!: PlantType;
  placeId!: string;
  moduleId!: string;
}
