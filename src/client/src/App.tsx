import React from 'react';
import './App.css';
import { Route, BrowserRouter as Router, Switch, Link } from 'react-router-dom';
import Login from './components/login/Login';
import Register from './components/register/Register';
import Home from './components/home/Home';
import Account from './components/account/Account';
import Error from './components/error/Error';

function App() {
  return (    
    <Router>
      <nav>
        <ul>
          <li><Link to="/">Home</Link></li>
          <li><Link to="/login">Login</Link></li>
          <li><Link to="/register">Register</Link></li>
        </ul>
      </nav>
      <Switch>
        <Route path="/login">
          <Login />
        </Route>
        <Route path="/register">
          <Register />
        </Route>
        <Route path="/account">
          <Account />
        </Route>
        <Route path="/error">
          <Error />
        </Route>
        <Route path="/">
          <Home />
        </Route>
      </Switch>    
    </Router>
  );
}

export default App;
