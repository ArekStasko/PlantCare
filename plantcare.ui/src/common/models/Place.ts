import { Plant } from './Plant';

export class Place {
  id!: number;
  name!: string;
  plants?: Plant[] | undefined;
}
