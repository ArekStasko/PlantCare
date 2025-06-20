import { useDispatch } from 'react-redux';
import plantcareApi from '../../app/api/plantcareApi';

const useInvalidateCache = () => {
  const dispatch = useDispatch();

  const invalidateCache = () => {
    dispatch(plantcareApi.util.resetApiState());
  };

  return {
    invalidateCache
  };
};

export default useInvalidateCache;
