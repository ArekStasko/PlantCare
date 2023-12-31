import { HumidityMeasurement } from './HumidityMeasurement';

export class HumidityData {
  x!: number | string;
  y!: number;
}

export class HumidityStatistics {
  id!: string;
  data!: HumidityData[];
}
