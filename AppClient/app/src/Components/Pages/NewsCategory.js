import React, {useEffect, useState} from "react";
import PostNews from "./PostNews";
import Grid from "@material-ui/core/Grid";
import {Divider } from "@material-ui/core";
import {API_BASE_URL} from "../../config";
import {matchPath} from "react-router";

const NewsCategory = (props) => {

    const [hasError, setErrors] = useState(false);
    const [news, setNews] = useState([]);
    const match = matchPath(props.history.location.pathname, {
        path: '/news/category/:id',
        exact: true,
        strict: false
    });
    let categoryId = match.params.id;
    console.log(categoryId);
    useEffect(() => {
        async function fetchData() {
            const res = await fetch(`${API_BASE_URL}/news/category/${categoryId}`);
            res
                .json()
                .then(res => setNews(res))
                .catch(err => setErrors(err));
        }
    fetchData();
    }, []);

    return (

        <Grid container spacing={6} >

            <Grid item xs={12} md={10}>
                <Divider />

                {news.map(post =>
                    <PostNews key={post.id} postNews={post}/>
                )}

            </Grid>

        </Grid>

    );
};
export default NewsCategory;