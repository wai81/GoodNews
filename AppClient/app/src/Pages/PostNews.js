import React from 'react';
import {Link} from "react-router-dom";
import { makeStyles } from '@material-ui/core/styles';
import {
    Typography,
    Grid,
    Card,
    CardActionArea,
    CardContent,
    CardMedia,
    Hidden,
    Zoom
} from '@material-ui/core';
import {Rating} from "@material-ui/lab";
import StarBorderIcon from '@material-ui/icons/StarBorder';
import Moment from 'react-moment';


const useStyles = makeStyles({
    card: {
        display: 'flex',
        marginBottom: 4,
    },
    cardDetails: {
        flex: 1,
        margin: 4,
    },
    cardMedia: {
        width: 160,
    },
});
const post = {
    image: 'https://source.unsplash.com/random',
    imageText: 'Image Text',
};

const PostNews = (props, loading) => {
    const classes = useStyles();
    // if (loading) {
    //      return <h2>Loading...</h2>;
    // }

    return (
        <Grid item xs={12} md={6}>
            <Zoom
                in={'true'}
                style={{ transitionDelay: 'true'  ? '500ms' : '0ms' }}
                >
                <CardActionArea  component={Link} to={`/newsPost/${props.post.id}`} key = {props.post.id}>
                    <Card className={classes.card}>
                    <div className={classes.cardDetails}>
                        <CardContent>
                            <Typography variant="subtitle1" color="textSecondary">
                                <Rating name="read-only" value={props.post.indexPositive} precision={0.5}  emptyIcon={<StarBorderIcon fontSize="inherit" />} readOnly />
                                {props.post.indexPositive}
                            </Typography>
                            <Typography component="h6" variant="h6">
                                {props.post.title}
                            </Typography>
                            <Typography variant="subtitle1" color="textSecondary">
                                <Moment format="YYYY/MM/DD">{props.post.dateCreate}</Moment>
                            </Typography>
                            <Typography variant="subtitle1" paragraph>
                                {props.post.newsContent.length > 250 ? props.post.newsContent.substr(0,250)+'...':props.post.newsContent }
                            </Typography>
                            <Link component={Link} to={`/newsPost/${props.post.id}`} key = {props.post.id}>
                                <Typography variant="subtitle1" color="primary">
                                    Читать подробнее...
                                </Typography>
                            </Link>
                        </CardContent>
                    </div>
                    <Hidden xsDown>
                        <CardMedia className={classes.cardMedia} image={post.image} title={post.imageTitle} />
                    </Hidden>
                    </Card>
                </CardActionArea>
            </Zoom>
        </Grid>


    );


};

export default PostNews;
