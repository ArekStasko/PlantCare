import Wizard from "../../common/wizard/Wizard";
import { CreatePlaceContext } from "./interfaces";
import { WizardController, WizardStep } from "../../common/wizard/interfaces";
import Details from "./steps/Details";
import Summary from "./steps/Summary";


const CreatePlace = () => {
  const initialContext: CreatePlaceContext = {};

  const steps = [
    {
      order: 0,
      isFinal: false,
      title: ,
      getStep: (wizardController: WizardController<CreatePlaceContext>) => <Details wizardController={wizardController} />
    } as WizardStep<CreatePlaceContext>,
    {
      order: 1,
      isFinal: true,
      title: "Summary",
      getStep: (wizardController: WizardController<CreatePlaceContext>) => <Summary wizardController={wizardController} />
    } as WizardStep<CreatePlaceContext>
  ]

  const onSubmit = (context: CreatePlaceContext) => {
    console.log(context)
  }

  return (
    <Wizard<CreatePlaceContext> initialContext={initialContext} steps={steps} onSubmit={onSubmit} />
  )
}

export default CreatePlace