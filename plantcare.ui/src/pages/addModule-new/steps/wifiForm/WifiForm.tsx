import { WizardStepProps } from "../../../../common/wizard/interfaces";
import { AddModuleContext } from "../../interfaces";
import { WizardStep } from "../../../../common/wizard/components/wizardStep/WizardStep";


const WifiForm = ({wizardController}: WizardStepProps<AddModuleContext>) => {

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
    title={'Wifi Configuration'}
    >
      Wifi Form
    </WizardStep>
  )
}

export default WifiForm;