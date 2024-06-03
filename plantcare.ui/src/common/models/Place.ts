import { Plant } from './Plant';

export class Place {
  id!: number;
  userId!: number;
  name!: string;
  plants?: Plant[] | undefined;
}
