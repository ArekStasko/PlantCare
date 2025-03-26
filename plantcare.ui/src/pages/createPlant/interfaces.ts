import { PlantType } from "../../common/models/plantTypes";

export interface CreatePlantContext {
  name?: string,
  description?: string,
  type?: string,
  place?: string,
  module?: string
}
