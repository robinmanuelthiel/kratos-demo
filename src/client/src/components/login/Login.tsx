import * as React from 'react';
import { useEffect, useState } from 'react';
import AuthService from '../../services/AuthService';
import KratosForm from '../kratosForm/KratosForm';
import { KratosFormDetails } from '../kratosForm/KratosFormDetails';

type LoginState = {
  url: string;
  formDetails: KratosFormDetails;
}

const Login: React.FC = () => {
  const [state, setState] = useState<LoginState>({ url: '', formDetails: new KratosFormDetails()});
  const authService = new AuthService();

  useEffect(() => {
    const func = async() => {
      const flowId = authService.getLoginFlowId();
      const url = authService.getLoginFlowActionUrl(flowId);
      const formDetails = await authService.getLoginFlowDetails(flowId);
      setState({...state, url: url, formDetails: formDetails});      
    }
    func();
	}, []);

  return (
    <div>
      <h2>Login</h2>
      <KratosForm action={state.url} submitLabel='Login' formDetails={state.formDetails} />
    </div>
  );
};

export default Login;
