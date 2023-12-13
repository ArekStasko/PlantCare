import { HumidityMeasurement } from './HumidityMeasurement';
import { Plant } from './Plant';

export class Module {
  id!: string;
  requiredMoistureLevel!: number;
  criticalMoistureLevel!: number;
  plant?: Plant;
}
