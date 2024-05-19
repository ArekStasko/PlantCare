import Cookies from 'js-cookie';

export type UserData = {
  id: string;
  token: string;
};

export const SaveUserData = (userData: UserData) => {
  console.log(userData);
  console.log('SAVING DATA');
  Cookies.set('id', userData.id);
  Cookies.set('token', userData.token);
};

export const DeleteUserData = () => {
  Cookies.remove('id');
  Cookies.remove('token');
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
