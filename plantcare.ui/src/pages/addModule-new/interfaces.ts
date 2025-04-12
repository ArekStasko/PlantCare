import { BLEDevice } from '../../common/models/BLEDevice';

export interface AddModuleContext {
  device?: BLEDevice;
  characteristic?: BluetoothRemoteGATTCharacteristic;
  wifiName?: string;
  wifiPassword?: string;
  moduleName?: string;
}
