import React from 'react';
import PropTypes from 'prop-types';
import { makeStyles } from '@material-ui/core/styles';
import {Typography, Grid, Card,CardActionArea,CardContent,CardMedia,Hidden} from '@material-ui/core';


const useStyles = makeStyles({
    card: {
        display: 'flex',
    },
    cardDetails: {
        flex: 1,
    },
    cardMedia: {
        width: 160,
    },
});
const NewsPost = ({ posts, loading }) => {
    if (loading) {
        return <h2>Loading...</h2>;
    }

    return (
        <ul className='list-group mb-4'>
            {posts.map(post => (
                <li key={post.id} className='list-group-item'>
                    {post.title}
                </li>
            ))}
        </ul>
    );
};

export default NewsPost;

// const NewsPost = (props) => {
//     const classes = useStyles();
//     const { post } = props;
//     return (
//         <Grid item xs={12} md={6}>
//             <CardActionArea component="a" href="#">
//                 <Card className={classes.card}>
//                     <div className={classes.cardDetails}>
//                         <CardContent>
//                             <Typography component="h6" variant="h5">
//                                 {post.title}
//                             </Typography>
//                             <Typography variant="subtitle1" color="textSecondary">
//                                 {post.dateCreate}
//                             </Typography>
//                             <Typography variant="subtitle1" paragraph>
//                                 {post.newsDescription }
//                             </Typography>
//                             <Typography variant="subtitle1" color="primary">
//                                 Continue reading...
//                             </Typography>
//                         </CardContent>
//                     </div>
//                     <Hidden xsDown>
//                         <CardMedia className={classes.cardMedia} image={post.image} title={post.imageTitle} />
//                     </Hidden>
//                 </Card>
//             </CardActionArea>
//         </Grid>
//     );
// }
//
// export default  NewsPost
//
// NewsPost.propTypes = {
//     post: PropTypes.object,
// };