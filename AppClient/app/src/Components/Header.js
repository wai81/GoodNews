import React, {useEffect, useState} from "react";
import {NavLink, Link} from "react-router-dom";
import {Toolbar, Typography, Button, IconButton} from "@material-ui/core";
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
            const res = await fetch(`${API_BASE_URL}/categories`);
            res
                .json()
                .then(res => setCategories(res))
                .catch(err => setErrors(err));
        }

        fetchData();
    }, []);

    return (
        <React.Fragment>
            <Toolbar>
                <Button size="small"><NavLink to="/">Good News</NavLink></Button>
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
                <NavLink to = '/login'>
                    <Button  variant="outlined" size="small">
                        Sign up
                    </Button>
                </NavLink>
            </Toolbar>
            <Toolbar component="nav" variant="dense" className={classes.toolbarSecondary}>
                {categories.map(category => (
                    <Link
                        to = {`/news/category/${category.id}`}
                        color="inherit"
                        noWrap
                        variant="body2"
                        className={classes.toolbarLink}
                    >
                        {category.name}
                    </Link>
                ))}
            </Toolbar>
        </React.Fragment>
    );
}