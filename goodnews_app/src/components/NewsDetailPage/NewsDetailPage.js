import React,  {useEffect, useState}  from "react";
import {CardContent, Container, Typography} from "@material-ui/core";
import {matchPath} from "react-router";
import {Rating} from "@material-ui/lab";
import Moment from "react-moment";

const NewsDetailPage = (props)=> {
    const [hasError, setErrors] = useState(false);
    const [post, setPost] = useState([]);
    const match = matchPath(props.history.location.pathname, {
        path: '/news/:id',
        exact: true,
        strict: false
    });
    let postId = match.params.id;
    console.log(postId)
    useEffect(() => {
        async function fetchData() {
            const res = await fetch(`https://localhost:44300/api/News/${postId}`);
            res
                .json()
                .then(res => setPost(res))
                .catch(err => setErrors(err));
        }
        fetchData();
    }, []);

    return (
        <Container align="justify">
            <Typography variant="subtitle1" color="textSecondary">
                <Rating name="read-only" value={post.indexPositive} readOnly />
                {post.indexPositive}
            </Typography>
            <Typography component="h6" variant="h5">
                {post.title}
            </Typography>
            <Typography variant="subtitle1" color="textSecondary">
                <Moment format="YYYY/MM/DD">{post.dateCreate}</Moment>
            </Typography>

            <Typography variant="subtitle1" paragraph>
                {post.newsDescription}
            </Typography>
        </Container>

    )

}
export default  NewsDetailPage