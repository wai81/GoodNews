import React from 'react';
import PropTypes from 'prop-types';
import { makeStyles } from '@material-ui/core/styles';
import {Grid,Paper,Typography,Link} from '@material-ui/core';


const useStyles = makeStyles(theme => ({
    sidebarAboutBox: {
        padding: theme.spacing(2),
        backgroundColor: theme.palette.grey[200],
    },
    sidebarSection: {
        marginTop: theme.spacing(3),
    },
}));

export default function Sidebar(props) {
    const classes = useStyles();
    const { description, title } = props;

    return (
        <Grid item xs={12} md={4}>
            <Paper elevation={0} className={classes.sidebarAboutBox}>
                <Typography variant="h6" gutterBottom>
                    {title}
                </Typography>
                <Typography>{description}</Typography>
            </Paper>

        </Grid>
    );
}

Sidebar.propTypes = {
    //archives: PropTypes.array,
    description: PropTypes.string,
    //social: PropTypes.array,
    title: PropTypes.string,
};