import React, {useEffect, useState} from "react";
import PostNews from "./PostNews";
import Pagination from "../Pagination";
import  mainFeaturedPost from "./mainFeaturedPost";
import axios from 'axios';
import Grid from "@material-ui/core/Grid";
import {Divider } from "@material-ui/core";
import {API_BASE_URL} from "../../config";


const News = () => {

    const [hasError, setErrors] = useState(false);
    const [news, setNews] = useState([]);
    const [loading, setLoading] = useState(false);
    const [countPage, setCountPage] = useState(null);
    const [currentPage, setCurrentPage] = useState(1);
    const [postsPerPage] = useState(0);

    useEffect(() => {
        const fetchPosts = async () => {
            //setLoading(true);
            //const res = await axios.get(`${API_BASE_URL}/news/`);
            const res = await fetch(`${API_BASE_URL}/News`);
            res
                .json()
                .then(res => setNews(res.news))
                .catch(err => setErrors(err));

            //setLoading(false);
        };

        fetchPosts();
    }, []);
    // Get current posts

    const indexOfLastPost = currentPage * postsPerPage;
    const indexOfFirstPost = indexOfLastPost - postsPerPage;
    const currentPosts = news.slice(indexOfFirstPost, indexOfLastPost);
    // Change page
    const paginate = pageNumber => setCurrentPage(pageNumber);

    return (

        <Grid container justify="center" spacing={10} >
            {/*<mainFeaturedPost/>*/}
            <Grid item xs={12} md={12}>
                <Divider />
                {/*<Posts news={currentPosts} loading={loading}/>*/}
                {currentPosts.map(post =>
                    <PostNews postNews={post} loading={loading}/>
                )}
                {/*<PostNews news={currentPosts} loading={loading}/>*/}
                <Pagination
                    postsPerPage={postsPerPage}
                    totalPosts={news.length}
                    paginate={paginate}
                />
            </Grid>

        </Grid>

    );
};
export default News;