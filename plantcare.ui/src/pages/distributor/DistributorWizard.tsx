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

const DistributorWizard = () => {
  const initialContext = {} as AddDistributorContext;

  const steps = [
    {
      order: 0,
      title: 'Device',
      getStep: (wizardController: WizardController<AddDistributorContext>) => (
        <DeviceSelection wizardController={wizardController} />
      )
    } as WizardStep<AddDistributorContext>,
    {
      order: 1,
      title: 'Wifi',
      getStep: (wizardController: WizardController<AddDistributorContext>) => (
        <WifiForm wizardController={wizardController} />
      )
    } as WizardStep<AddDistributorContext>,
    {
      order: 2,
      title: 'Address',
      getStep: (wizardController: WizardController<AddDistributorContext>) => (
        <Address wizardController={wizardController} />
      )
    } as WizardStep<AddDistributorContext>,
    {
      order: 3,
      title: 'Name',
      getStep: (wizardController: WizardController<AddDistributorContext>) => (
        <NameForm wizardController={wizardController} />
      )
    } as WizardStep<AddDistributorContext>,
    {
      order: 4,
      title: 'Summary',
      getStep: (wizardController: WizardController<AddDistributorContext>) => (
        <Summary wizardController={wizardController} />
      )
    } as WizardStep<AddDistributorContext>
  ];

  return <Wizard<AddDistributorContext> initialContext={initialContext} steps={steps} />;
};

export default DistributorWizard;
