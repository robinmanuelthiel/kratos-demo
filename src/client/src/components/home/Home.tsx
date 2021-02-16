import * as React from 'react';
import ApiService from '../../services/ApiService';
import AuthService from '../../services/AuthService';

interface IHomeProps {
}

const Home: React.FC<IHomeProps> = (props) => {
  const authService = new AuthService();
  const apiService = new ApiService();

  const getSession = async () => {
    await authService.getSessionAsync();
  }
  const refreshSession = async () => {
    await authService.refreshSessionAsync();
  }
  const logout = async () => {
    await authService.logoutAsync();
  }
  const callAPi = async () => {
    const forecast = await apiService.getWeatherForecastAsync();
    console.log('forecast', forecast);
  }

  return (
    <div>
      <h2>Home</h2>
      Everyone can see this.      
      <br />      
      <button onClick={getSession}>Session</button>
      <br />      
      <button onClick={refreshSession}>Refresh</button>
      <br />      
      <button onClick={callAPi}>Call REST API</button>
      <br />      
      <button onClick={logout}>Logout</button>
    </div>
  );
};

export default Home;
