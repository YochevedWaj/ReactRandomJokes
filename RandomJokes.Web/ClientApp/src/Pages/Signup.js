import React, { useState } from 'react';
import { useHistory } from 'react-router-dom';
import axios from 'axios';

const Signup = () => {
    const [formData, setFormData] = useState({ firstName: '', lastName: '', email: '', password: '' });
    const history = useHistory();

    const onTextChange = (e) => {
        setFormData({ ...formData, [e.target.name]: e.target.value });
    }
const onSubmitClick = async (e) => {
        e.preventDefault();
        await axios.post('/api/account/signup', formData);
        history.push('/login');
    }
    

    const { firstName, lastName, email, password } = formData;
    return (< div className="col-md-6 offset-md-3 jumbotron" >
        <h1>Sign up!</h1>
        <form onSubmit={onSubmitClick}>
            <input type="text" name='firstName' onChange={onTextChange} value={firstName} className="form-control" name="firstName" placeholder="First Name" />
            <br />
            <input type="text" name='lastName' onChange={onTextChange} value={lastName} className="form-control" name="lastName" placeholder="Last Name" />
            <br />
            <input type="text" name='email' onChange={onTextChange} value={email} className="form-control" name="email" placeholder="Email" />
            <br />
            <input type="password" name='password' onChange={onTextChange} value={password} className="form-control" name="password" placeholder="Password" />
            <br />
            <button className="btn btn-primary btn-block btn-lg">Sign up!</button>
        </form>
    </div >
    )
}

export default Signup;