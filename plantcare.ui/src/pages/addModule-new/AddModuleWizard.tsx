import Wizard from '../../common/wizard/Wizard';
import { AddModuleContext } from './interfaces';
import { WizardController, WizardStep } from '../../common/wizard/interfaces';
import DeviceSelection from './steps/deviceSelection/DeviceSelection';
import WifiForm from './steps/wifiForm/WifiForm';
import Summary from './steps/summary/Summary';

const AddModuleWizard = () => {
  const initialContext = {} as AddModuleContext;

  const steps = [
    {
      order: 0,
      title: 'Device',
      getStep: (wizardController: WizardController<AddModuleContext>) => (
        <DeviceSelection wizardController={wizardController} />
      )
    } as WizardStep<AddModuleContext>,
    {
      order: 1,
      title: 'Wifi',
      getStep: (wizardController: WizardController<AddModuleContext>) => (
        <WifiForm wizardController={wizardController} />
      )
    } as WizardStep<AddModuleContext>,
    {
      order: 2,
      title: 'Summary',
      getStep: (wizardController: WizardController<AddModuleContext>) => (
        <Summary wizardController={wizardController} />
      )
    } as WizardStep<AddModuleContext>
  ];

  return <Wizard<AddModuleContext> initialContext={initialContext} steps={steps} />;
};

export default AddModuleWizard;
