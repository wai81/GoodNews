import React from 'react';
import {Link} from "react-router-dom";
import { Rating } from '@material-ui/lab';
import {Paper, Typography, Grid, Button,Zoom} from '@material-ui/core';
import {makeStyles} from "@material-ui/core";
import { RichText } from 'prismic-reactjs';

const useStyles = makeStyles(theme => ({
    mainFeaturedPost: {
        position: 'relative',
        backgroundColor: theme.palette.grey[800],
        color: theme.palette.common.white,
        marginBottom: theme.spacing(4),
        backgroundImage: 'url(https://source.unsplash.com/random)',
        backgroundSize: 'cover',
        backgroundRepeat: 'no-repeat',
        backgroundPosition: 'center',
    },
    overlay: {
        position: 'absolute',
        top: 0,
        bottom: 0,
        right: 0,
        left: 0,
        backgroundColor: 'rgba(0,0,0,.3)',
    },
    mainFeaturedPostContent: {
        position: 'relative',
        padding: theme.spacing(3),
        [theme.breakpoints.up('md')]: {
            padding: theme.spacing(6),
            paddingRight: 0,
        },
    },
}));

const post = {
    title: 'Title of a longer featured blog post',
    description:
        "Multiple lines of text that form the lede, informing new readers quickly and efficiently about what's most interesting in this post's contents.",
    image: 'https://source.unsplash.com/random',
    imgText: 'main image description',
    linkText: 'Continue reading…',
    indexPositive: 5,
};

export default function  MainFeaturedPost(props) {
//const bestPost = (props) =>{
    const classes = useStyles();
    // alert( props.firstPos.title);

    return (
        <Zoom
            in={'true'}
            style={{ transitionDelay: 'true'  ? '500ms' : '0ms' }}
        >
        <Paper className={classes.mainFeaturedPost} style={{ backgroundImage: `url(${post.image})`}}>
            {/* Increase the priority of the hero background image */}
            {<img style={{ display: 'none' }} src={post.image} alt={post.imageText} />}
            <div className={classes.overlay} />
            <Grid container>
                <Grid item md={10}>

                    <div className={classes.mainFeaturedPostContent}>
                        <Typography variant="h6" color="inherit" gutterBottom align={"left"}>
                            <Rating name="read-only" value={props.firstPos?.indexPositive} readOnly />
                            {props.firstPos?.indexPositive}
                        </Typography>
                        <Typography component="h2" variant="h4" color="inherit" gutterBottom>
                            {props.firstPos?.title}
                        </Typography>
                        <Typography variant="h6" color="inherit" paragraph>
                            {props.firstPos?.newsContent}
                        </Typography>
                        <Button component={Link} to={`/news/${props.firstPos?.id}`} key = {props.firstPos?.id} variant="contained">
                            <Typography variant="subtitle1" color="inherit">
                                Читать подробнее...
                            </Typography>
                        </Button>
                    </div>
                </Grid>

            </Grid>
        </Paper>
        </Zoom>
    );

}

