import { PlantType } from "../../models/plantTypes";

export class SetModuleStatusRequest {
  moduleId!: number;
  status!: boolean;
}