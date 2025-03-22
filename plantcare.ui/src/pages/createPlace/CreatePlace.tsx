import Wizard from '../../common/wizard/Wizard';
import { CreatePlaceContext } from './interfaces';
import { WizardController, WizardStep } from '../../common/wizard/interfaces';
import Details from './steps/details/Details';
import Summary from './steps/summary/Summary';

const CreatePlace = () => {
  const initialContext: CreatePlaceContext = {};

  const steps = [
    {
      order: 0,
      title: 'Details',
      getStep: (wizardController: WizardController<CreatePlaceContext>) => (
        <Details wizardController={wizardController} />
      )
    } as WizardStep<CreatePlaceContext>,
    {
      order: 1,
      title: 'Summary',
      getStep: (wizardController: WizardController<CreatePlaceContext>) => (
        <Summary wizardController={wizardController} />
      )
    } as WizardStep<CreatePlaceContext>
  ];

  return <Wizard<CreatePlaceContext> initialContext={initialContext} steps={steps} />;
};

export default CreatePlace;
