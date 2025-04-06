import { WizardStepProps } from '../../../../common/wizard/interfaces';
import { AddModuleContext } from '../../interfaces';
import { WizardStep } from '../../../../common/wizard/components/wizardStep/WizardStep';

const Summary = ({ wizardController }: WizardStepProps<AddModuleContext>) => {
  return (
    <WizardStep
      nextButton={{
        onClick: () => console.log('Submit'),
        isDisabled: false,
        title: 'Submit'
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
      title={'Summary'}
    >
      Summary
    </WizardStep>
  );
};

export default Summary;
