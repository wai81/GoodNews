import React, {Fragment,useEffect, useState} from "react";
import {Link, Router} from "react-router-dom";
import createBrowserHistory from "history/createBrowserHistory";

import {Toolbar, Typography, Button, IconButton, Grow, Slide} from "@material-ui/core";
import SearchIcon from '@material-ui/icons/Search';
import { makeStyles } from '@material-ui/core/styles';
import {API_BASE_URL} from "../config";

const useStyles = makeStyles(theme => ({
    toolbar: {
        borderBottom: `1px solid ${theme.palette.divider}`,
    },
    toolbarTitle: {
        flex: 1,
    },
    toolbarSecondary: {
        justifyContent: 'space-between',
        overflowX: 'auto',
    },
    toolbarLink: {
        padding: theme.spacing(1),
        flexShrink: 0,
    },
}));

export default function Header(props) {
    const classes = useStyles();
    const { sections, title } = props;

    const [hasError, setErrors] = useState(false);
    const [categories, setCategories] = useState([]);

    useEffect(() => {
        async function fetchData() {
            const res = await fetch(`${API_BASE_URL}/api/categories`);
            res
                .json()
                .then(res => setCategories(res))
                .catch(err => setErrors(err));
        }

        fetchData();
    }, []);
    const history = createBrowserHistory();

    return (
        <Fragment>
            {/*<Router history={history}>*/}
            <Toolbar>
                <Button size="small" component={Link} to="/">Good News</Button>
                <Typography
                    component="h2"
                    variant="h5"
                    color="inherit"
                    align="center"
                    noWrap
                    className={classes.toolbarTitle}
                >
                    Good News
                </Typography>
                <IconButton>
                    <SearchIcon />
                </IconButton>
                <Button component={Link} to="/login"
                        variant="outlined"
                        size="small" >
                        Sign up
                </Button>
            </Toolbar>

            <Toolbar component="nav" variant="dense" className={classes.toolbarSecondary}>
                {categories.map(category => (
                    <Slide
                        direction="up"
                        in={'true'}
                        style={{ transformOrigin: '0 0 0' }}
                        {...('true' ? { timeout: 1000 } : {})}
                    >
                    <Button
                        component={Link} to={`/newsCategory/${category.id}`}
                        color="inherit"
                        noWrap
                        key={category.id}
                        variant="body2"
                        className={classes.toolbarLink}
                    >
                        {category.name}
                    </Button>
                    </Slide>
                ))}
            </Toolbar>

            {/*</Router>*/}
        </Fragment>
    );
}
