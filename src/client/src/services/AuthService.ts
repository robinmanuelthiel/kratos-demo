import { KratosFormDetails } from "../components/kratosForm/KratosFormDetails";

export default class LoginService {
  // private kratosPublicUrl = 'http://127.0.0.1:4433';  
  private kratosPublicUrl = 'http://localhost:4433';  
  
  async CheckHealth(): Promise<void> {
    const res = await fetch(this.kratosPublicUrl + '/health/alive', {
      method: 'GET', 
      headers: { Accept: 'application/json' }
    });
    console.log(res);
  }

  async Login(): Promise<void> {
    return;
  }

  getRegistrationFlowId(): string {
    const flowId = new URLSearchParams(window.location.search).get('flow');
    if (!flowId) {
      window.location.href = this.kratosPublicUrl + '/self-service/registration/browser';
      return '';
    } else {            
      return flowId;
    }
  } 

  getLoginFlowId(): string {
    const flowId = new URLSearchParams(window.location.search).get('flow');
    if (!flowId) {
      window.location.href = this.kratosPublicUrl + '/self-service/login/browser';
      return '';
    } else {            
      return flowId;
    }
  } 

  getRegistrationFlowActionUrl(flowId: string): string {
    return this.kratosPublicUrl + '/self-service/registration/methods/password?flow=' + flowId;
  }

  getLoginFlowActionUrl(flowId: string): string {
    return this.kratosPublicUrl + '/self-service/login/methods/password?flow=' + flowId;
  }

  async getRegistrationFlowDetails(flowId: string): Promise<KratosFormDetails> {
    const res = await fetch(this.kratosPublicUrl + '/self-service/registration/flows?id=' + flowId, {
      method: 'GET', 
      headers: { Accept: 'application/json' }
    });
    const json = await res.json();
    console.log('Form Details', json);

    const details: KratosFormDetails = json.methods.password.config;
    console.log('details', details);
    return details;
  }

  async getLoginFlowDetails(flowId: string): Promise<KratosFormDetails> {
    const res = await fetch(this.kratosPublicUrl + '/self-service/login/flows?id=' + flowId, {
      method: 'GET', 
      headers: { Accept: 'application/json' }
    });
    const json = await res.json();
    console.log('Form Details', json);

    const details: KratosFormDetails = json.methods.password.config;
    console.log('details', details);
    return details;  
  }

  async getErrorAsync(): Promise<string> {
    const errorId = new URLSearchParams(window.location.search).get('error');
    const res = await fetch(this.kratosPublicUrl + '/self-service/errors?error=' + errorId, {
      method: 'GET', 
      headers: { Accept: 'application/json' }
    });  
    return res.text();    
  }

  async getSessionAsync(): Promise<string> {    
    const res = await fetch(this.kratosPublicUrl + '/sessions/whoami', {
      method: 'GET', 
      headers: { Accept: 'application/json' },
      credentials: 'include' // Make sure to include sesion cookie here
    });
    const json = await res.json();
    console.log(json);
    return '';    
  }

  async refreshSessionAsync() {    
    window.location.href = this.kratosPublicUrl + '/self-service/login/browser?refresh=true';      
  }

  async logoutAsync() {    
    window.location.href = this.kratosPublicUrl + '/self-service/browser/flows/logout';      
  }
}
