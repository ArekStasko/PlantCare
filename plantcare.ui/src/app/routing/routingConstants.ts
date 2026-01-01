interface Dictionary<T> {
  [Key: string]: T;
}

const RoutingPaths = {
  site: 'http://192.168.1.42:3001/auth',
  idp: 'http://192.168.1.42:3000/idp',
  authBasic: '/auth',
  auth: '/auth/:id?/:token?',
  root: '/dashboard',
  plant: '/create-plant',
  updatePlant: '/update-plant',
  plantStatistics: '/statistics',
  createPlace: '/create-place',
  addModule: '/add-module'
};

export const ActionsTranslation: Dictionary<string> = {
  '/dashboard': 'Dashboard',
  '/create-plant': 'Create Plant',
  '/create-place': 'Create Place',
  '/add-module': 'Add Module'
};

export const ActionsToPerform = [
  RoutingPaths.root,
  RoutingPaths.plant,
  RoutingPaths.createPlace,
  RoutingPaths.addModule
];

export default RoutingPaths;
