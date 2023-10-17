import {PlantType} from "./plantTypes";


export class Plant {
    id!: number;
    placeId!: number;
    name!: string;
    description!: string;
    type!: PlantType;
    criticalMoistureLevel!: number;
    requiredMoistureLevel!: number;
    moistureLevel!: number;
    moduleId?: string | undefined;
}