import { Typography } from '@mui/material';
import { AddDistributorContext } from '../../interfaces';
import { WizardStepProps } from '../../../../common/wizard/interfaces';
import { WizardStep } from '../../../../common/wizard/components/wizardStep/WizardStep';

const DeviceSelection = ({ wizardController }: WizardStepProps<AddDistributorContext>) => {
  return (
    <WizardStep
      nextButton={{
        onClick: async () => console.log('device selection submit'),
        isDisabled: false,
        title: 'Next'
      }}
      cancelButton={{
        onClick: () => wizardController.onCancel(),
        isDisabled: false,
        title: 'Cancel'
      }}
      backButton={{
        onClick: () => wizardController.goToPreviousStep(),
        isDisabled: false,
        title: 'Back'
      }}
      title={'DeviceSelection'}
    >
      <Typography>Distributor selection</Typography>
    </WizardStep>
  );
};

export default DeviceSelection;
