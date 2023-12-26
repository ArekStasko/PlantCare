import moment from 'moment';

const getStartOfCurrentDay = (): string => {
  const startOfDay = moment().startOf('day');
  return startOfDay.format('YYYY-MM-DD HH:mm:ss');
};

const getEndOfCurrentDay = (): string => {
  const endOfDay = moment().endOf('day');
  return endOfDay.format('YYYY-MM-DD HH:mm:ss');
};

const getProperTime = (date: Date): string => {
  let minutes = date.getMinutes();
  let hours = date.getHours();
  minutes = minutes % 60;
  console.log(`TIME: ${hours}:${minutes}`);
  return `${hours}.${minutes}`;
};

const convertDatesToStrings = (date: Date): string => {
  const properDate = new Date(date);
  return getProperTime(properDate);
};

export default {
  getStartOfCurrentDay,
  getEndOfCurrentDay,
  convertDatesToStrings
};
