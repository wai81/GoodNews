import React, {useEffect, useState} from "react";
import PostNews from "./PostNews";
import Grid from "@material-ui/core/Grid";
import {Divider, Typography} from "@material-ui/core";
import {API_BASE_URL} from "../config";
import {matchPath} from "react-router";
import MainFeaturedPost from "./MainFeaturedPost";

const NewsCategory = (props) => {

    const [hasError, setErrors] = useState(false);
    const [news, setNews] = useState([]);

    const match = matchPath(props.history.location.pathname, {
        path: '/newsCategory/:id',
        exact: true,
        strict: false
    });

    const categoryId = match.params.id;

    useEffect(() => {
        async function fetchData() {
            const res = await fetch(`${API_BASE_URL}/api/News/category/${categoryId}`);
            res
                .json()
                .then(res => setNews(res))
                .catch(err => setErrors(err));
        }
    fetchData();
    }, [categoryId]);

    return (

        <main>
            <MainFeaturedPost firstPos={news['0']}/>
            <Grid container spacing={4}>
                {news.map(post => (
                    <PostNews post={post} />
                ))}
            </Grid>
        </main>

        // <Grid item xs={12} md={8}>
        //     <Typography variant="h6" gutterBottom>
        //         Только хорошие новости
        //     </Typography>
        //     <Divider />
        //     {news.map(post => (
        //         <PostNews  post ={post}/>
        //     ))}
        // </Grid>

    );
};
export default NewsCategory;
