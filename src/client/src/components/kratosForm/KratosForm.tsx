import * as React from 'react';
import { KratosFormDetails } from './KratosFormDetails';

interface IKratorFormProps {
  action: string;
  submitLabel: string;
  formDetails: KratosFormDetails;
}

const KratosForm: React.FC<IKratorFormProps> = (props) => {
  return  (
    <form action={props.action} method="POST">      
      { 
        props.formDetails.messages?.map((message) => 
          <span key={message.id}>{message.text}</span>
        ) 
      }
      { 
        props.formDetails.fields?.map((field) => 
          <div key={field.name} >
            <input 
              key={field.name} 
              name={field.name}
              placeholder={field.name}
              defaultValue={field.value}
              type={field.type}
              required={field.required}
              />            
              {
                field.messages?.map((message) => 
                  <span key={message.id}>{message.text}</span>
                )
              }
            <br />
          </div>
        ) 
      }
      <button type="submit">{props.submitLabel}</button>
    </form>
  );
};

export default KratosForm;
