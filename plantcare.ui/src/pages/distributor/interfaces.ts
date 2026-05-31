import { BLEDevice } from '../../common/models/BLEDevice';

export interface AddDistributorContext {
  device?: BLEDevice;
  wifiDataService?: BluetoothRemoteGATTCharacteristic;
  wifiName?: string;
  wifiPassword?: string;
  address?: string;
  distributorName?: string;
  moduleId?: string;
  plantId?: string;
}
