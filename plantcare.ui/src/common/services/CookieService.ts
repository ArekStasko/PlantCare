import Cookies from 'js-cookie';

export type UserData = {
  id: string;
  token: string;
};

export const SaveUserData = (userData: UserData) => {
  Cookies.set('id', userData.id);
  Cookies.set('token', userData.token);
};

export const GetUserData = (): UserData | undefined => {
  const id = Cookies.get('id');
  const token = Cookies.get('token');
  if (!id || !token) return undefined;
  return {
    id,
    token
  } as UserData;
};
