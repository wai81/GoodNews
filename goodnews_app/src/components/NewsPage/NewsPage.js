import React,{useEffect, useState} from 'react';
import PropTypes from 'prop-types';
import { Rating } from '@material-ui/lab';
import { makeStyles } from '@material-ui/core/styles';
import {Grid, Typography } from '@material-ui/core';
import Divider from '@material-ui/core/Divider';
import NewsPost from "../Post/NewsPost";
import Pagination from "../Pagination";
import axios from 'axios';
import MainFeaturedPost from "../Post/MainFeaturedPost";

const useStyles = makeStyles(theme => ({
    markdown: {
        ...theme.typography.body2,
        padding: theme.spacing(3, 0),

    },
}));
const post = [
    {
        id : 1,
        title: 'Title of a longer featured blog post',
        description:
            "Multiple lines of text that form the lede, informing new readers quickly and efficiently about what's most interesting in this post's contents.",
        image: 'https://source.unsplash.com/random',
        imgText: 'main image description',
        linkText: 'Continue reading…',
        idexPositiviti: 5,
    },
    {
        id : 2,
        title: 'Title of a longer featured blog post',
        description:
            "Multiple lines of text that form the lede, informing new readers quickly and efficiently about what's most interesting in this post's contents.",
        image: 'https://source.unsplash.com/random',
        imgText: 'main image description',
        linkText: 'Continue reading…',
        idexPositiviti: 5,
    }
];
const NewsPage = (props) =>{
    const classes = useStyles();
    const [posts, setPosts] = useState([]);
    const [loading, setLoading] = useState(false);
    const [currentPage, setCurrentPage] = useState(1);
    const [postsPerPage] = useState(4);
    const [hasError, setErrors] = useState(false);

    useEffect(() => {
        const fetchPosts = async () => {
            setLoading(true);
            //const res = await axios.get('https://jsonplaceholder.typicode.com/posts');
            const res = await axios.get('https://localhost:44300/api/News');
            setPosts(res.data);
            setLoading(false);
        };

        fetchPosts();
    }, []);

    // useEffect(() => {
    //     async function fetchData() {
    //         setLoading(true);
    //         const res = await fetch('https://localhost:44300/api/News');
    //         res
    //             .json()
    //             .then(res => setPosts(res.data))
    //             .catch(err => setErrors(err));
    //         setLoading(false)
    //     }
    //
    //     fetchData();
    // }, []);
    // Get current posts
    const indexOfLastPost = currentPage * postsPerPage;
    const indexOfFirstPost = indexOfLastPost - postsPerPage;
    const currentPosts = posts.slice(indexOfFirstPost, indexOfLastPost);

    const paginate = pageNumber => setCurrentPage(pageNumber);

    return (
        <Grid container spacing={6}>
        <MainFeaturedPost />
        <Grid item xs={12} md={10}>
            <Divider />
            {/*   {post.map(newsItem => (*/}
            {/*    <NewsPost className={classes.markdown} key={newsItem.id} post={newsItem} />*/}
            {/*))}*/}
            <NewsPost posts={currentPosts} loading={loading} />
            <Pagination
                postsPerPage={postsPerPage}
                totalPosts={posts.length}
                paginate={paginate}
            />
        </Grid>
        </Grid>



    );
}

export default  NewsPage;
//
// NewsListPost.propTypes = {
//     posts: PropTypes.array,
//     title: PropTypes.string,
// };