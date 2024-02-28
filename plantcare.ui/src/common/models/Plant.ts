import { PlantType } from './plantTypes';

export class Plant {
  id!: number;
  placeId!: number;
  name!: string;
  description!: string;
  type!: PlantType;
  moduleId?: string | undefined;
}
