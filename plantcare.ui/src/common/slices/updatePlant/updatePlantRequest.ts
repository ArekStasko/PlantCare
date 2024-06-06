import { PlantType } from '../../models/plantTypes';

export class UpdatePlantRequest {
  id!: number;
  userId!: number;
  name!: string;
  description!: string;
  type!: PlantType;
  placeId!: string;
  criticalMoistureLevel?: number;
  requiredMoistureLevel?: number;
  moduleId?: string;
}
