import {PlantType} from "../../models/plantTypes";

export class CreatePlantRequest {
    name!: string;
    description!: string;
    type!: PlantType;
    criticalMoistureLevel!: number;
    requiredMoistureLevel!: number;
    moduleId?: string;
}