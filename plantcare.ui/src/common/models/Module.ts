import { HumidityMeasurement } from './HumidityMeasurement';
import { Plant } from './Plant';

export class Module {
  id!: string;
  userId!: number;
  name!: string;
  plant?: Plant;
  isMonitoring?: boolean;
}
