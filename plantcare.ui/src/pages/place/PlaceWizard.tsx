import Wizard from '../../common/wizard/Wizard';
import { PlaceContext, PlaceFlowType } from './interfaces';
import { WizardController, WizardStep } from '../../common/wizard/interfaces';
import Details from './steps/details/Details';
import Summary from './steps/summary/Summary';
import { useMemo } from 'react';
import { useLocation } from 'react-router';

const PlaceWizard = () => {
  const location = useLocation();

  const initialContext = useMemo(() => {
    if (location.state) {
      return location.state as PlaceContext;
    }

    return {
      flowType: PlaceFlowType.CREATE
    } as PlaceContext;
  }, [location]);

  const steps = [
    {
      order: 0,
      title: 'Details',
      getStep: (wizardController: WizardController<PlaceContext>) => (
        <Details wizardController={wizardController} />
      )
    } as WizardStep<PlaceContext>,
    {
      order: 1,
      title: 'Summary',
      getStep: (wizardController: WizardController<PlaceContext>) => (
        <Summary wizardController={wizardController} />
      )
    } as WizardStep<PlaceContext>
  ];

  return <Wizard<PlaceContext> initialContext={initialContext} steps={steps} />;
};

export default PlaceWizard;
