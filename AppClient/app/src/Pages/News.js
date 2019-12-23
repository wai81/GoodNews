import React, {Fragment, useEffect, useState} from "react";
import { makeStyles } from '@material-ui/core/styles';
import PostNews from "./PostNews";
import {Grid,Fade} from "@material-ui/core";
import {API_BASE_URL} from "../config";
import NotFound from "./NotFound";
import MainFeaturedPost from "./MainFeaturedPost";
import {UserProvider} from "../services/UseUser";
import InfiniteScroll from "react-infinite-scroll-component";
import axios from "axios";

const useStyles = makeStyles(theme => ({
    markdown: {
        ...theme.typography.body2,
        padding: theme.spacing(3, 0),
    },
}));

const News = (props) => {
    const classes = useStyles();
    const [hasError, setErrors] = useState(false);
    const [news, setNews] = useState([]);
    const [loading, setLoading] =useState(false)
   const [page, setPage] = useState(1);


    useEffect(() => {
        axios.get(`${API_BASE_URL}/api/News/?curentNumPage=${page}`)

        .then(res => {setNews(res.data)});
        }, []);
    const fetchData = async () => {
        setPage(page + 1);
        await axios
            .get(`${API_BASE_URL}/api/News/?curentNumPage=${page + 1}`)
            .then(res => setNews(news.concat(res.data)));
    };

        // const fetchPosts = async () => {
        //     setLoading(true);
        //     const res = await fetch(`${API_BASE_URL}/api/News`);
        //     res
        //         .json()
        //         .then(res => setNews(res))
        //         .catch(err => setErrors(true));
        //
        //     setLoading(false);
        //
        // }
        //     fetchPosts();
        // }, []);

    if (news.length!==null) {

            return (
                <main>
                    <InfiniteScroll dataLength={news.length} next={fetchData} hasMore={true}>
                    <MainFeaturedPost firstPos={news['0']}/>

                    <Grid container spacing={4}>

                           {news.map(post => (
                               <UserProvider>
                                    <PostNews post={post} />
                               </UserProvider>
                            ))}

                    </Grid>
                    </InfiniteScroll>
                </main>


     );
    } else if (hasError){
        return <NotFound />;
    }
};
export default News;
