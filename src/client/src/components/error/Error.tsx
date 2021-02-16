import * as React from 'react';
import { useEffect, useState } from 'react';
import AuthService from '../../services/AuthService';

interface IErrorProps {
}

type ErrorState = {
  error: string;
}

const Error: React.FC<IErrorProps> = (props) => {
  const [state, setState] = useState<ErrorState>({ error: '' });
  const authService = new AuthService();

  useEffect(() => {
    const func = async() => {
      const error = await authService.getErrorAsync();
      setState({...state, error: error});      
    }
    func();
	}, []);

  return (
    <div>
      <h2>Error</h2>
      <pre>{state.error}</pre>    
    </div>
  );
};

export default Error;
