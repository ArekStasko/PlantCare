import { WizardStepProps } from "../../../../common/wizard/interfaces";
import { AddModuleContext } from "../../interfaces";
import { WizardStep } from "../../../../common/wizard/components/wizardStep/WizardStep";


const DeviceSelection = ({wizardController}: WizardStepProps<AddModuleContext>) => {

  return (
    <WizardStep
      nextButton={{
        onClick: () => wizardController.goToNextStep(),
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
      title={'Device'}
    >
      Select Device
    </WizardStep>
  )
}

export default DeviceSelection;