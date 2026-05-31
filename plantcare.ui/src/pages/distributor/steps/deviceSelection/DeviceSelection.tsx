import { AddDistributorContext } from '../../interfaces';
import { WizardStepProps } from '../../../../common/wizard/interfaces';
import { WizardStep } from '../../../../common/wizard/components/wizardStep/WizardStep';
import React, { useCallback, useMemo } from 'react';
import { SelectBleDevice } from '../../../../common/components/selectBleDevice/SelectBleDevice';
import { BLEDevice } from '../../../../common/models/BLEDevice';

const DeviceSelection = ({ wizardController }: WizardStepProps<AddDistributorContext>) => {
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
      title={'DeviceSelection'}
    >
      <SelectBleDevice
        serviceUuid=""
        saveWifiDataServiceCharacteristicUuid=""
        title="Select Plantcare Distributor device from list"
        onDeviceSelection={onSelectDevice}
      />
    </WizardStep>
  );
};

export default DeviceSelection;
