import WizardContext from "../../common/Layouts/Wizard/WizardContext/wizardContext";
import { useForm } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import validators from "../../common/services/Validators";
import { IWizardStep } from "../../common/Layouts/Wizard/interfaces";
import React from "react";
import AddModuleSummary from "./steps/Summary/addModuleSummary";

export const AddModule = () => {
  const methods = useForm({
    mode: 'onChange',
    resolver: yupResolver(validators.addModuleSchema)
  });

  const onSubmit = async () => {
    console.log("Submit !")
    return {data: true}
  };

  const steps: IWizardStep[] = [
    {
      title: 'Module Summary',
      component: <AddModuleSummary />,
      validators: [],
      id: 0,
      nextStep: 1,
      isStepVisible: true,
      isFinal: false
    }
  ];

  return <WizardContext onSubmit={onSubmit} steps={steps} methods={methods} />;
};

export default AddModule;
