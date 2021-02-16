export default class ApiService {
  private apiPublicUrl = 'http://localhost:5000';

  async getWeatherForecastAsync(): Promise<string> {
    const res = await fetch(this.apiPublicUrl + '/weatherforecast', {
      credentials: 'include'
    });
    return res.json();    
  }

  async getMeAsync(): Promise<string> {
    const res = await fetch(this.apiPublicUrl + '/weatherforecast/me', {
      credentials: 'include'
    });
    return res.text(); 
  }
}
