import React from 'react';
import { Route } from 'react-router';
import Layout from './Components/Layout';
import Home from './Pages/Home';
import Login from './Pages/Login';
import Logout from './Pages/Logout';
import Signup from './Pages/Signup';
import ViewAll from './Pages/ViewAll';
import { AuthContextComponent } from './AuthContext';
import PrivateRoute from './Components/PrivateRoute';


export default function App() {
    return (
        <AuthContextComponent>
            <Layout>
                <Route exact path='/' component={Home} />
                <Route exact path='/login' component={Login} />
                <Route exact path='/signup' component={Signup} />
                <Route exact path='/viewall' component={ViewAll} />
                <PrivateRoute exact path='/logout' component={Logout} />
            </Layout>
        </AuthContextComponent>
    )
}