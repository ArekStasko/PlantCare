interface Dictionary<T> {
  [Key: string]: T;
}

const RoutingPaths = {
  site: 'http://localhost:3001/auth',
  idp: 'http://localhost:3000/idp',
  authBasic: '/auth',
  auth: '/auth/:id?/:token?',
  root: '/dashboard',
  createPlant: '/create-plant',
  updatePlant: '/update-plant',
  plantStatistics: '/statistics',
  createPlace: '/create-place',
  updatePlace: '/update-place'
};

export const ActionsTranslation: Dictionary<string> = {
  '/dashboard': 'Dashboard',
  '/create-plant': 'Create Plant',
  '/create-place': 'Create Place'
};

export const ActionsToPerform = [
  RoutingPaths.root,
  RoutingPaths.createPlant,
  RoutingPaths.createPlace
];

export default RoutingPaths;
