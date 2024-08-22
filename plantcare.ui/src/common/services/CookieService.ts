import Cookies from 'js-cookie';

export const GetToken = (): string | undefined => {
  const token = Cookies.get('token');
  if (!token) return undefined;
  return token;
};
