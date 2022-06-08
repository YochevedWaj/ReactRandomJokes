import axios from 'axios';
import React, { useEffect, useState } from 'react';

const ViewAll = () => {
    const [jokes, setJokes] = useState([]);

    useEffect(() => {
        const getJokes = async () => {
            const { data } = await axios.get('/api/jokes/getjokes');
            setJokes(data);
        }
        getJokes();
    }, []);

    const getAmount = (liked, joke) => {
        return joke.userLikedJokes.filter(ulj => ulj.liked === liked).length;
    }


    return (
        <div className='row'>
            <div className='col-md-6 offset-md-3'>
                {jokes.map((j) => <div className='card card-body bg-light mb-3'>
                    <h5>{j.setup}</h5>
                    <h5>{j.punchline}</h5>
                    <span>Likes: {getAmount(true, j)}</span>
                    <br />
                    <span>Dislikes: {getAmount(false, j)}</span>
                </div>)}
            </div>
        </div>
    )
}

export default ViewAll;