import axios from 'axios';
import React, { useState } from 'react';
import { Link, useHistory } from 'react-router-dom';
import { useAuthContext } from '../AuthContext';

const Login = () => {
    const [formData, setFormData] = useState({ email: '', password: '' });
    const history = useHistory();
    const { setUser } = useAuthContext();

    const onTextChange = (e) => {
        setFormData({ ...formData, [e.target.name]: e.target.value });
    }

    const onSubmitClick = async (e) => {
        e.preventDefault();
        const { data } = await axios.post('/api/account/login', formData);
        setUser(data);
        history.push('/');
    }

    const { email, password } = formData;
    return (  <div className="col-md-6 offset-md-3 jumbotron" >
            <h1>Login!</h1>
        <form onSubmit={onSubmitClick}>
            <input type="text" name='email' onChange={onTextChange} value={email} className="form-control" name="email" placeholder="Email" />
        <br />
            <input type="password" name='password' onChange={onTextChange} value={password} className="form-control" name="password" placeholder="Password" />
        <br />
        <button className="btn btn-primary btn-block btn-lg">Login</button>
        <Link to='/signup'>Don't have an account? Sign up here!</Link>
    </form>
</div>
        )
}
export default Login;