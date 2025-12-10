import { BLEDevice } from '../../common/models/BLEDevice';

export interface AddModuleContext {
  device?: BLEDevice;
  writeService?: BluetoothRemoteGATTCharacteristic;
  readService?: BluetoothRemoteGATTCharacteristic;
  wifiName?: string;
  wifiPassword?: string;
  moduleName?: string;
}
