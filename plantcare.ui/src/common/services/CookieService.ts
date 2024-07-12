import Cookies from 'js-cookie';

export const SaveToken = (token: string) => {
  Cookies.set('token', token);
};

export const DeleteToken = () => {
  Cookies.remove('token');
};

export const GetToken = (): string | undefined => {
  const token = Cookies.get('token');
  if (!token) return undefined;
  return token;
};
