import {PlantType} from "../../common/models/plantTypes";

export interface IPlantDetails{
    name: string,
    description: string,
    placeId: string,
    type: PlantType,
}

export interface ICreatePlantState{
    state: IPlantDetails
}