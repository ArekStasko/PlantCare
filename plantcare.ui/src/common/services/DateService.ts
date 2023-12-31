import moment from 'moment';

const getStartOfGivenDay = (year: number, month: number, day: number): string => {
  const startOfDay = moment({ year, month: month - 1, day }).startOf('day');
  return startOfDay.format('YYYY-MM-DD HH:mm:ss');
};

const getEndOfGivenDay = (year: number, month: number, day: number): string => {
  const startOfDay = moment({ year, month: month - 1, day }).endOf('day');
  return startOfDay.format('YYYY-MM-DD HH:mm:ss');
};

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
  let convertedMinutes =
    (minutes % 60).toString().length == 1 ? `0${minutes % 60}` : (minutes % 60).toString();
  return `${hours}:${convertedMinutes}`;
};

const convertDatesToStrings = (date: Date): string => {
  const properDate = new Date(date);
  return getProperTime(properDate);
};

export default {
  getStartOfGivenDay,
  getEndOfGivenDay,
  getStartOfCurrentDay,
  getEndOfCurrentDay,
  convertDatesToStrings
};
