import { PlantType } from "@arekstasko/plantcare-api-client";

export enum PlantFlowType {
  UPDATE = 'UPDATE',
  CREATE = 'CREATE'
}

export interface PlantContext {
  flowType: PlantFlowType;
  name?: string;
  description?: string;
  type?: PlantType;
  place?: string;
  placeName?: string;
  module?: string;
  plantId?: number;
  moduleName?: string;
  currentModule: string;
}
