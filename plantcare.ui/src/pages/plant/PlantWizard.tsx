import { PlantContext, PlantFlowType } from './interfaces';
import Wizard from '../../common/wizard/Wizard';
import { WizardController, WizardStep } from '../../common/wizard/interfaces';
import Details from './steps/details/Details';
import Place from './steps/place/Place';
import Module from './steps/module/Module';
import Summary from './steps/summary/Summary';
import { useLocation } from 'react-router';
import { useMemo } from 'react';

const PlantWizard = () => {
  const location = useLocation();

  const initialContext = useMemo(() => {
    if (location.state) {
      return location.state as PlantContext;
    }

    return {
      flowType: PlantFlowType.CREATE
    } as PlantContext;
  }, [location]);

  const steps = [
    {
      order: 0,
      title: 'Details',
      getStep: (wizardController: WizardController<PlantContext>) => (
        <Details wizardController={wizardController} />
      )
    } as WizardStep<PlantContext>,
    {
      order: 1,
      title: 'Place',
      getStep: (wizardController: WizardController<PlantContext>) => (
        <Place wizardController={wizardController} />
      )
    } as WizardStep<PlantContext>,
    {
      order: 2,
      title: 'Module',
      getStep: (wizardController: WizardController<PlantContext>) => (
        <Module wizardController={wizardController} />
      )
    } as WizardStep<PlantContext>,
    {
      order: 3,
      title: 'Summary',
      getStep: (wizardController: WizardController<PlantContext>) => (
        <Summary wizardController={wizardController} />
      )
    } as WizardStep<PlantContext>
  ];

  return <Wizard<PlantContext> initialContext={initialContext} steps={steps} />;
};

export default PlantWizard;
