import { Box } from "@mui/material";
import Wizard from "../../common/wizard/Wizard";
import { CreatePlaceContext } from "./interfaces";
import { WizardStep } from "../../common/wizard/interfaces";
import Details from "./steps/Details";
import Summary from "./steps/Summary";


const CreatePlace = () => {
  const initialContext: CreatePlaceContext = {};

  const steps = [
    {
      order: 0,
      isFinal: false,
      title: "Details",
      component: <Details />,
    } as WizardStep<CreatePlaceContext>,
    {
      order: 1,
      isFinal: true,
      title: "Summary",
      component: <Summary />,
    } as WizardStep<CreatePlaceContext>
  ]

  return (
    <Wizard<CreatePlaceContext> initialContext={initialContext} steps={steps} />
  )
}

export default CreatePlace