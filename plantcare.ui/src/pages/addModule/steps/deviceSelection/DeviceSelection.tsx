import { WizardStepProps } from '../../../../common/wizard/interfaces';
import { AddModuleContext } from '../../interfaces';
import { WizardStep } from '../../../../common/wizard/components/wizardStep/WizardStep';
import React, { useCallback, useMemo } from 'react';
import { SelectBleDevice } from '../../../../common/components/selectBleDevice/SelectBleDevice';
import { BLEDevice } from '../../../../common/models/BLEDevice';

const DeviceSelection = ({ wizardController }: WizardStepProps<AddModuleContext>) => {
  const onSelectDevice = useCallback(
    (device?: BLEDevice, wifiDataService?: BluetoothRemoteGATTCharacteristic) => {
      wizardController.updateContext({
        ...wizardController.context,
        device: device,
        wifiDataService: wifiDataService
      });
    },
    []
  );

  const disableNextBtn = useMemo(() => {
    const savedDevice = wizardController.context.device;
    const savedWifiDataServiceCharacteristic = wizardController.context.wifiDataService;

    return !savedDevice || !savedWifiDataServiceCharacteristic;
  }, [wizardController.context]);

  return (
    <WizardStep
      nextButton={{
        onClick: () => wizardController.goToNextStep(),
        isDisabled: disableNextBtn,
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
      <SelectBleDevice
        serviceUuid="00000180-0000-1000-8000-00805f9b34fb"
        saveWifiDataServiceCharacteristicUuid="0000dead-0000-1000-8000-00805f9b34fb"
        title="Select Plantcare module from device list"
        onDeviceSelection={onSelectDevice}
      />
    </WizardStep>
  );
};

export default DeviceSelection;
