export enum PlaceFlowType {
  UPDATE = 'UPDATE',
  CREATE = 'CREATE'
}

export interface PlaceContext {
  flowType: PlaceFlowType;
  id?: number;
  name?: string;
}
