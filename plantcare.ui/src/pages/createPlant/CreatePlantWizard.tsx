import { CreatePlantContext } from './interfaces';
import Wizard from '../../common/wizard/Wizard';
import { WizardController, WizardStep } from '../../common/wizard/interfaces';
import Details from './steps/details/Details';
import Place from './steps/place/Place';
import Module from './steps/module/Module';
import Summary from './steps/summary/Summary';

const CreatePlantWizard = () => {
  const initialContext: CreatePlantContext = {};

  const steps = [
    {
      order: 0,
      title: 'Details',
      getStep: (wizardController: WizardController<CreatePlantContext>) => (
        <Details wizardController={wizardController} />
      )
    } as WizardStep<CreatePlantContext>,
    {
      order: 1,
      title: 'Place',
      getStep: (wizardController: WizardController<CreatePlantContext>) => (
        <Place wizardController={wizardController} />
      )
    } as WizardStep<CreatePlantContext>,
    {
      order: 2,
      title: 'Module',
      getStep: (wizardController: WizardController<CreatePlantContext>) => (
        <Module wizardController={wizardController} />
      )
    } as WizardStep<CreatePlantContext>,
    {
      order: 3,
      title: 'Summary',
      getStep: (wizardController: WizardController<CreatePlantContext>) => (
        <Summary wizardController={wizardController} />
      )
    } as WizardStep<CreatePlantContext>
  ];

  return <Wizard<CreatePlantContext> initialContext={initialContext} steps={steps} />;
};

export default CreatePlantWizard;
