import { BLEDevice } from '../../common/models/BLEDevice';

export interface AddModuleContext {
  device?: BLEDevice;
  moduleIdService?: BluetoothRemoteGATTCharacteristic;
  wifiDataService?: BluetoothRemoteGATTCharacteristic;
  moduleAddressService?: BluetoothRemoteGATTCharacteristic;
  wifiName?: string;
  wifiPassword?: string;
  address?: string;
  moduleName?: string;
}
