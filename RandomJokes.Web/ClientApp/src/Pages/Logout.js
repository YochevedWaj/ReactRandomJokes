import axios from 'axios';
import React, { useEffect } from 'react';
import { useHistory } from 'react-router-dom';
import { useAuthContext } from '../AuthContext';

const Logout = () => {
    const history = useHistory();
    const { setUser } = useAuthContext();

    useEffect(() => {
        const logout = async () => {
            await axios.post('/api/account/logout');
            setUser(null);
            history.push('/login');
        }
        logout();

    })


    return <></>
}
export default Logout;