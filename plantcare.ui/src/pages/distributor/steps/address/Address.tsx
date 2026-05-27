import { Typography } from '@mui/material';
import { AddDistributorContext } from '../../interfaces';
import { WizardStepProps } from '../../../../common/wizard/interfaces';
import { WizardStep } from '../../../../common/wizard/components/wizardStep/WizardStep';

const Address = ({ wizardController }: WizardStepProps<AddDistributorContext>) => {
  return (
    <WizardStep
      nextButton={{
        onClick: async () => console.log('address submit'),
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
      title={'Address'}
    >
      <Typography>Distributor Address</Typography>
    </WizardStep>
  );
};

export default Address;
