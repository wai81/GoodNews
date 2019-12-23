import React,  {useEffect, useState}  from "react";
import { RichText } from 'prismic-reactjs';
import { Container, Typography} from "@material-ui/core";
import {matchPath} from "react-router";
import {Rating} from "@material-ui/lab";
import Moment from "react-moment";
import NotFound from "./NotFound";
import {API_BASE_URL} from "../config";
import StarBorderIcon from "@material-ui/icons/StarBorder";
import {UserProvider,useUser} from "../services/UseUser";



const NewsDetailPage = (props)=> {
    const [hasError, setErrors] = useState(false);
    const [post, setPost] = useState([]);
    const { user } = useUser();

    const match = matchPath(props.history.location.pathname, {
        path: '/newsPost/:id',
        exact: true,
        strict: false
    });

    const postId = match.params.id;

    useEffect(() => {
        async function fetchData() {
            const res = await fetch(`${API_BASE_URL}/api/News/newsPost/${postId}`);
            res
                .json()
                .then(res => setPost(res.news))
                .catch(err => setErrors(err));
        }
        fetchData();
    }, []);

    

    if (hasError!=true) {

            return (
                <UserProvider>
                {user.email ? user.email &&
                <Container align="justify">
                    <Typography variant="subtitle1" color="textSecondary">
                        <Rating name="read-only" value={post.indexPositive} precision={0.5}  emptyIcon={<StarBorderIcon fontSize="inherit" />} readOnly />
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
                 :props.history.push("/login")}
                                    
                 </UserProvider>
            );
    } else {
        return <NotFound />;
    }
}
export default  NewsDetailPage
