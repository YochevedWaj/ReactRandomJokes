import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { useAuthContext } from '../AuthContext';
import { Link } from 'react-router-dom';
import { produce } from 'immer';


const Home = () => {
    const { user } = useAuthContext();
    const [joke, setJoke] = useState({
        id: 0,
        type: '',
        setup: '',
        punchline: '',
        userLikedJokes: []
    });
    const [counts, setCounts] = useState({ likes: 0, dislikes: 0 });
    const [disableLike, setDisableLike] = useState(false);
    const [disableDislike, setDisableDislike] = useState(false);


    useEffect(() => {
        const getJoke = async () => {
            const { data } = await axios.get('/api/jokes/getrandomjoke');
            setJoke(data);
        }
        getJoke();
        setCounts({ likes: getAmount(true), dislikes: getAmount(false) });
        getDisableStatus();
    }, []);

    const getDisableStatus = async () => {
        const { data } = await axios.get(`/api/jokes/canlike?jokeID=${id}`);
        setDisableLike(data.disableLike);
        setDisableDislike(data.disableDislike);
    }


    const getAmount = (liked) => {
        return joke.userLikedJokes.filter(ulj => ulj.liked === liked).length;
    }

    const { setup, punchline, id } = joke;
    const { likes, dislikes } = counts;

    const updateCounts = async () => {
        if(id) {
            const { data } = await axios.get(`/api/jokes/getcounts?jokeID=${id}`);
            setCounts(data);
        }
    }

    setInterval(updateCounts, 500);

    const onLikeClick = async (like) => {
        await axios.post('/api/jokes/sendfeedback', { jokeId: id, like });
        updateCounts();
        getDisableStatus();
    }


        
    return ( <div className='container'>
        {!id && <h1>Loading....</h1>}
        {id &&
            <div className='row'>
                <div className='col-md-6 offset-md-3 card card-body bg-light'>
                    <div>
                        <h4>{setup}</h4>
                        <h4>{punchline}</h4>
                    </div>
                    {!user && <div>
                        <Link to='/login'>Login to your account to like/dislike this joke</Link>
                    </div>}
                    {user && <div>
                    <button className='btn btn-primary' disabled={disableLike} onClick={() => onLikeClick(true)}>Like</button>
                    <button className='btn btn-danger' disabled={disableDislike} onClick={() => onLikeClick(false)}>Dislike</button>
                    </div>}
                    <div>
                        <h4>Likes: {likes}</h4>
                        <h4>Dislikes: {dislikes}</h4>
                    </div>
                    <button className='btn btn-link' onClick={() => window.location.reload()}>Refresh</button>
                </div>
            </div>}
        </div>)
}

export default Home;