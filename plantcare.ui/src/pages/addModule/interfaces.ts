import { BLEDevice } from '../../common/models/BLEDevice';

export interface AddModuleContext {
  device?: BLEDevice;
  wifiDataService?: BluetoothRemoteGATTCharacteristic;
  wifiName?: string;
  wifiPassword?: string;
  address?: string;
  moduleName?: string;
}
