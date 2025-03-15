const wizardProgress = {
  display: 'flex',
  justifyContent: 'center',
  alignItems: 'center',
  height: '10%'
};

const progress = {
  height: '100%',
  width: '100%',
  display: 'flex',
  alignItems: 'center'
};

const progressTile = {
  clipPath: 'polygon(5% 0, 100% 0,95% 100%, 0 100%)',
  height: '100%',
  width: '100%',
  display: 'flex',
  justifyContent: 'space-evenly',
  alignItems: 'center'
};

export default {
  wizardProgress,
  progress,
  progressTile
};
