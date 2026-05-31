import { Box } from '@mui/material';
import { AddModuleContext } from '../addModule/interfaces';
import { WizardController, WizardStep } from '../../common/wizard/interfaces';
import DeviceSelection from './steps/deviceSelection/DeviceSelection';
import WifiForm from './steps/wifiForm/WifiForm';
import Address from './steps/address/Address';
import NameForm from './steps/nameForm/NameForm';
import Summary from './steps/summary/Summary';
import { AddDistributorContext } from './interfaces';
import Wizard from '../../common/wizard/Wizard';
import ExistingDistributors from "./steps/existingDistributors/ExistingDistributors";
import { useParams } from "react-router";

const DistributorWizard = () => {
  let { moduleId, plantId } = useParams();

  const initialContext = {moduleId, plantId} as AddDistributorContext;

  const steps = [
    {
      order: 0,
      title: 'Distributors',
      getStep: (wizardController: WizardController<AddDistributorContext>) => (
        <ExistingDistributors wizardController={wizardController} />
      )
    } as WizardStep<AddDistributorContext>,
    {
      order: 1,
      title: 'Device',
      getStep: (wizardController: WizardController<AddDistributorContext>) => (
        <DeviceSelection wizardController={wizardController} />
      )
    } as WizardStep<AddDistributorContext>,
    {
      order: 2,
      title: 'Wifi',
      getStep: (wizardController: WizardController<AddDistributorContext>) => (
        <WifiForm wizardController={wizardController} />
      )
    } as WizardStep<AddDistributorContext>,
    {
      order: 3,
      title: 'Address',
      getStep: (wizardController: WizardController<AddDistributorContext>) => (
        <Address wizardController={wizardController} />
      )
    } as WizardStep<AddDistributorContext>,
    {
      order: 4,
      title: 'Name',
      getStep: (wizardController: WizardController<AddDistributorContext>) => (
        <NameForm wizardController={wizardController} />
      )
    } as WizardStep<AddDistributorContext>,
    {
      order: 5,
      title: 'Summary',
      getStep: (wizardController: WizardController<AddDistributorContext>) => (
        <Summary wizardController={wizardController} />
      )
    } as WizardStep<AddDistributorContext>
  ];

  return <Wizard<AddDistributorContext> initialContext={initialContext} steps={steps} />;
};

export default DistributorWizard;
