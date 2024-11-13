import WizardContext from "../../common/Layouts/Wizard/WizardContext/wizardContext";
import { useForm } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import validators from "../../common/services/Validators";
import { IWizardStep } from "../../common/Layouts/Wizard/interfaces";

export const AddModule = () => {
  const methods = useForm({
    mode: 'onChange',
    resolver: yupResolver(validators.addModuleSchema)
  });

  const onSubmit = async () => {
    console.log("Submit !")
    return {data: true}
  };

  const steps: IWizardStep[] = [];

  return <WizardContext onSubmit={onSubmit} steps={steps} methods={methods} />;
};

export default AddModule;
