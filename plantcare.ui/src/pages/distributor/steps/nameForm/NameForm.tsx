import { Typography } from '@mui/material';
import { AddDistributorContext } from '../../interfaces';
import { WizardStepProps } from '../../../../common/wizard/interfaces';
import { WizardStep } from '../../../../common/wizard/components/wizardStep/WizardStep';

const NameForm = ({ wizardController }: WizardStepProps<AddDistributorContext>) => {
  return (
    <WizardStep
      nextButton={{
        onClick: async () => console.log('name form submit'),
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
      title={'NameForm'}
    >
      <Typography>Distributor name form</Typography>
    </WizardStep>
  );
};

export default NameForm;
