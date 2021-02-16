import * as React from 'react';
import { useEffect, useState } from 'react';
import AuthService from '../../services/AuthService';
import KratosForm from '../kratosForm/KratosForm';
import { KratosFormDetails } from '../kratosForm/KratosFormDetails';


interface IRegisterProps {
}

type RegisterState = {
  url: string;
  formDetails: KratosFormDetails;
}

const Register: React.FC<IRegisterProps> = (props) => {
  const [state, setState] = useState<RegisterState>({ url: '', formDetails: new KratosFormDetails()});
  const authService = new AuthService();

  useEffect(() => {
    const func = async() => {
      const flowId = authService.getRegistrationFlowId();
      const url = authService.getRegistrationFlowActionUrl(flowId);
      const formDetails = await authService.getRegistrationFlowDetails(flowId);
      setState({...state, url: url, formDetails: formDetails});      
    }
    func();
	}, []);

  return (
    <div>
      <h2>Register</h2>      
      <KratosForm action={state.url} submitLabel='Register' formDetails={state.formDetails} />
    </div>
  );
};

export default Register;
