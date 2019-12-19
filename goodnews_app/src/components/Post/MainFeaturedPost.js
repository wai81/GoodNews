import React from 'react';
import PropTypes from 'prop-types';
import { Rating } from '@material-ui/lab';
import { makeStyles } from '@material-ui/core/styles';
import {Paper,Typography,Link,Grid} from '@material-ui/core';

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
    mainIdexPositivitit: {
        position: 'relative',
        padding: theme.spacing(3),
        [theme.breakpoints.up('md')]: {
            padding: theme.spacing(0),
            paddingRight: 0,
        },
    },
}));

const MainFeaturedPost = (props) =>{
    const classes = useStyles();
    //const { post } = props;
    const post = {
        title: 'Title of a longer featured blog post',
        description:
            "Multiple lines of text that form the lede, informing new readers quickly and efficiently about what's most interesting in this post's contents.",
        image: 'https://source.unsplash.com/random',
        imgText: 'main image description',
        linkText: 'Continue reading…',
        idexPositiviti: 5,
    };
    return (
        <Paper className={classes.mainFeaturedPost} style={{ backgroundImage: `url(${post.image})` }}>
            {/* Increase the priority of the hero background image */}
            {<img style={{ display: 'none' }} src={post.image} alt={post.imageText} />}
            <div className={classes.overlay} />
            <Grid container>
               <Grid item md={6}>

                    <div className={classes.mainFeaturedPostContent}>

                        <Typography component="h1" variant="h3" color="inherit" gutterBottom>
                            {post.title}
                        </Typography>
                        <Typography variant="h5" color="inherit" paragraph>
                            {post.description}
                        </Typography>
                        <Link variant="subtitle1" href="#">
                            {post.linkText}
                        </Link>
                    </div>

                </Grid>
                <Grid item md={5}>
                <Typography className={classes.mainIdexPositivitit} variant="h6" color="inherit" gutterBottom align={"right"}>
                    <Rating name="read-only" value={post.idexPositiviti} readOnly />
                    <p>Индек позитивности: {post.idexPositiviti} </p>
                </Typography>
                </Grid>
            </Grid>
        </Paper>
    );
}
export default  MainFeaturedPost
//
// MainFeaturedPost.propTypes = {
//     post: PropTypes.object,
// };