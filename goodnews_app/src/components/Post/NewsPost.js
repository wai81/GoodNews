import React from 'react';
import {NavLink} from "react-router-dom";
import PropTypes from 'prop-types';
import { makeStyles } from '@material-ui/core/styles';
import {Link,Typography, Paper ,Grid, Card,CardActionArea,CardContent,CardMedia,Hidden} from '@material-ui/core';
import {Rating} from "@material-ui/lab";
import Moment from 'react-moment';



const useStyles = makeStyles({
    card: {
        display: 'flex',
        marginBottom: 4,

    },
    cardDetails: {
        flex: 1,
    },
    cardMedia: {
        width: 160,
    },
});

const NewsPost = ({ posts, loading }) => {
    const classes = useStyles();
    if (loading) {
        return <h2>Loading...</h2>;
    }
    const rows = posts.map((post) => (
        <Grid item>
                <Card className={classes.card}>
                  <CardActionArea component={NavLink} to={`/news/${post.id}`}>
                    <div className={classes.cardDetails}>
                        <CardContent>
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
                                {post.newsContent.length > 250 ? post.newsContent.substr(0,250)+'...':post.newsContent }
                            </Typography>
                            <Link to={`/news/${post.id}`}>
                            <Typography variant="subtitle1" color="primary">
                                Continue reading...
                            </Typography>
                            </Link>
                        </CardContent>
                    </div>
                    <Hidden xsDown>
                        <CardMedia className={classes.cardMedia} image={post.image} title={post.imageTitle} />
                    </Hidden>
                  </CardActionArea>
           </Card>
        </Grid>
     ))


    return (
        <Grid>
            {rows}
        </Grid>
    );
};

export default NewsPost;

