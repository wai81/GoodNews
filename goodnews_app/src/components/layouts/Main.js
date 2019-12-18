import React,{useEffect, useState} from 'react';
import PropTypes from 'prop-types';
import { makeStyles } from '@material-ui/core/styles';
import {Grid, Typography} from '@material-ui/core';
import Divider from '@material-ui/core/Divider';
import FeaturedPost from '../Post/FeaturedPost';

const useStyles = makeStyles(theme => ({
    markdown: {
        ...theme.typography.body2,
        padding: theme.spacing(3, 0),
    },
}));

export default function Main(props) {
    const classes = useStyles();
    const { posts, title } = props;

    const [hasError, setErrors] = useState(false);
    const [news, setNews] = useState([]);

    useEffect(() => {
        async function fetchData() {
            const res = await fetch("https://localhost:44300/api/News");
            res
                .json()
                .then(res => setNews(res))
                .catch(err => setErrors(err));
        }

        fetchData();
    }, []);
   return (
        <Grid container spacing={4}>
            {news.map(newsitem => (
                <FeaturedPost key={newsitem.id} post={newsitem} />
            ))}

        </Grid>
    );
}

Main.propTypes = {
    posts: PropTypes.array,
    title: PropTypes.string,
};