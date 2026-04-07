import { useDispatch } from 'react-redux';
import emptyApi from '../RTK/emptyApi';

const useInvalidateCache = () => {
  const dispatch = useDispatch();

  const invalidateCache = () => {
    dispatch(emptyApi.util.resetApiState());
  };

  return {
    invalidateCache
  };
};

export default useInvalidateCache;
