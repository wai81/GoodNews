import React, {Fragment, useEffect, useState} from "react";
import { makeStyles } from '@material-ui/core/styles';
import PostNews from "./PostNews";
import {Grid,Fade} from "@material-ui/core";
import {API_BASE_URL} from "../config";
import NotFound from "./NotFound";
import MainFeaturedPost from "./MainFeaturedPost";


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
    const [checked, setChecked] = useState(false);


    useEffect(() => {

        const fetchPosts = async () => {
            setLoading(true);
            const res = await fetch(`${API_BASE_URL}/api/News`);
            res
                .json()
                .then(res => setNews(res))
                .catch(err => setErrors(true));

            setLoading(false);

        }

        fetchPosts();
    }, []);
    if (news.length!==null) {

            return (
                <main>

                    <MainFeaturedPost firstPos={news['0']}/>
                         <Grid container spacing={4}>

                           {news.map(post => (
                                <PostNews post={post} />
                            ))}

                        </Grid>
                </main>


     );
    } else if (hasError){
        return <NotFound />;
    }
};
export default News;
