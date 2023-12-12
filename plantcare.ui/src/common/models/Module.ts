import { HumidityMeasurement } from './HumidityMeasurement';

export class Module {
  id!: string;
  requiredMoistureLevel!: number;
  criticalMoistureLevel!: number;
  humidityMeasurements!: HumidityMeasurement[];
}
