import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Login } from './pages/Login';
import { Register } from './pages/Register';
import { Home } from './pages/Home';
import { ErrorData } from './pages/ErrorData';

export default class App extends Component {
  static displayName = App.name;

  render() {
    return (
      <Layout>
        <Route exact path='/' component={Login} />
        <Route exact path='/error-data' component={ErrorData} />
        <Route exact path='/register' component={Register} />
        <Route exact path='/home' component={Home} />
      </Layout>
    );
  }
}
