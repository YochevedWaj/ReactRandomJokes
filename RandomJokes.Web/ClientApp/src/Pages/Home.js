import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { useAuthContext } from '../AuthContext';
import { Link } from 'react-router-dom';
import { produce } from 'immer';


const Home = () => {
    const { user } = useAuthContext();
    //const [count, setCount] = useStateIfMounted(0);
    const [joke, setJoke] = useState({
        id: 0,
        type: '',
        setup: '',
        punchline: '',
        userLikedJokes: []
    });
    const [counts, setCounts] = useState({ likes: 0, dislikes: 0 });
    const [isLoading, setIsLoading] = useState(true);


    useEffect(() => {
        const getJoke = async () => {
            const { data } = await axios.get('/api/jokes/getrandomjoke');
            setJoke(data);
            setIsLoading(false);
        }
        getJoke();
        //setCounts({ likes: getAmount(true), dislikes: getAmount(false) });
        //updateCounts();
        //
    }, []);

    const { setup, punchline, userLikedJokes, id } = joke;
    const { likes, dislikes } = counts;

    const updateCounts = async () => {
        if(id) {
            const { data } = await axios.get(`/api/jokes/getcounts?jokeID=${id}`);
            setCounts(data);
        }
    }

    setInterval(updateCounts, 500);

    const getAmount = (liked) => {
        return userLikedJokes.filter(ulj => ulj.liked === liked).length;
    }

    const onLikeClick = async (like) => {
        await axios.post('/api/jokes/sendfeedback', { jokeId: id, like });
        await updateCounts();
    }

        
    return ( <div className='container'>
        {!joke && <h1>Loading....</h1>}
        {joke &&
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
                    <button className='btn btn-primary' disabled={userLikedJokes.find(u => u.userID === user.id && u.liked)} onClick={() => onLikeClick(true)}>Like</button>
                    <button className='btn btn-danger' onClick={() => onLikeClick(false)}>Dislike</button>
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