import React from "react";
import WizardContext from "../../common/Layouts/Wizard/WizardContext/wizardContext";
import PlantDetails from "./Steps/PlantDetails/plantDetails";
import {IWizardStep} from "../../common/Layouts/Wizard/interfaces";
import PlaceSelect from "./Steps/PlaceSelect/placeSelect";
import PlantSummary from "./Steps/PlantSummary/plantSummary";
import {IPlantDetails} from "./interfaces";
import {PlantType} from "../../common/models/plantTypes";

export const CreatePlant = () => {
    const state : IPlantDetails = {
            name: "",
            description: "",
            placeId: "",
            type: PlantType.Fruit,
    }

    const steps : IWizardStep[] = [
        {
            title: "Plant Details",
            component: <PlantDetails state={state}/>,
            order: 0
        },
        {
            title: "Place Select",
            component: <PlaceSelect state={state} />,
            order: 1
        },
        {
            title: "Plant Summary",
            component: <PlantSummary state={state} />,
            order: 2
        }
    ]

    return(
       <WizardContext steps={steps}/>
    )
}

export default CreatePlant;