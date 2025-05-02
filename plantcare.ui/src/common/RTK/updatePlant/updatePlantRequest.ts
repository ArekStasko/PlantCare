import { PlantType } from '../../models/plantTypes';

export class UpdatePlantRequest {
  id!: number;
  name!: string;
  description!: string;
  type!: PlantType;
  placeId!: string;
  moduleId?: string;
}
