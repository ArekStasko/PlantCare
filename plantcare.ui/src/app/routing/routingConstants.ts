interface Dictionary<T> {
  [Key: string]: T;
}

const RoutingPaths = {
  auth: '/plantcare/:id/:token',
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
