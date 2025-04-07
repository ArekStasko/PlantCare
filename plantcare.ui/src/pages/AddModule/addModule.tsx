import WizardContext from '../../common/Layouts/Wizard/WizardContext/wizardContext';
import { useForm } from 'react-hook-form';
import { yupResolver } from '@hookform/resolvers/yup';
import validators from '../../common/services/Validators';
import { IWizardStep } from '../../common/Layouts/Wizard/interfaces';
import React, { Dispatch, SetStateAction, useEffect, useState } from 'react';
import AddModuleSummary from './steps/Summary/addModuleSummary';
import { BLEDevice } from '../../common/models/BLEDevice';
import {
  CreateModuleRequest,
  useCreateModuleMutation
} from '../../common/RTK/createModule/createModule';

export class DeviceContext {
  device?: BLEDevice;
  characteristic?: BluetoothRemoteGATTCharacteristic;
}

export interface ModuleStepProps {
  context: DeviceContext;
  updateContext: Dispatch<SetStateAction<DeviceContext>>;
}

export const AddModule = () => {
  const [createModule, { isLoading: loading }] = useCreateModuleMutation();
  const [context, setContext] = useState<DeviceContext>({});
  const methods = useForm({
    mode: 'onChange',
    resolver: yupResolver(validators.addModuleSchema)
  });

  const onSubmit = async () => {
    try {
      const result = await createModule({} as CreateModuleRequest);
      if ('data' in result) {
        const id = result.data;
        const crc = context.characteristic;
        if (crc) {
          const name = methods.getValues('wifiName');
          const psw = methods.getValues('wifiPassword');
          const encoder = new TextEncoder();
          const data = encoder.encode(`${name}|${psw}|${id}`);
          await crc.writeValue(data);
        }
        methods.setValue('connected', true);
        const device = context.device;
        device?.gatt?.disconnect();
        return { data: true };
      }
      methods.setValue('connected', false);
      return { data: false };
    } catch (error) {
      methods.setValue('connected', false);
      return { data: false };
    }
  };

  const steps: IWizardStep[] = [
    {
      title: 'Module Summary',
      component: <AddModuleSummary />,
      validators: [],
      id: 2,
      previousStep: 1,
      isStepVisible: true,
      isFinal: true
    }
  ];

  return <WizardContext onSubmit={onSubmit} steps={steps} methods={methods} isLoading={loading} />;
};

export default AddModule;
