import { Typography } from '@mui/material';
import { AddDistributorContext } from '../../interfaces';
import { WizardStepProps } from '../../../../common/wizard/interfaces';
import { WizardStep } from '../../../../common/wizard/components/wizardStep/WizardStep';

const WifiForm = ({ wizardController }: WizardStepProps<AddDistributorContext>) => {
  return (
    <WizardStep
      nextButton={{
        onClick: async () => console.log('wifi form submit'),
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
      title={'Wifi'}
    >
      <Typography>Distributor wifi</Typography>
    </WizardStep>
  );
};

export default WifiForm;
