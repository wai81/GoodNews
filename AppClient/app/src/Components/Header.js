import React, {
    Fragment,
    useEffect,
    useState} from "react";
import {Link} from "react-router-dom";

import {
    Toolbar,
    Typography,
    Button,
    Slide} from "@material-ui/core";
import AccountBoxIcon from '@material-ui/icons/AccountBox';
import { makeStyles } from '@material-ui/core/styles';
import {API_BASE_URL} from "../config";
import {useUser} from "../services/UseUser";

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
    const [hasError, setErrors] = useState(false);
    const [categories, setCategories] = useState([]);
    const { user, setAccessToken } = useUser();

    function logout() {
        setAccessToken(null);
    }

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

    return (
        <Fragment>
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
                {user.email ? user.email &&
                <Button color="inherit" onClick={logout}>
                    <AccountBoxIcon/> {user.email} Выход</Button>:
                    <Button component={Link} to="/login" color="inherit" >
                    <AccountBoxIcon/>Авторизация</Button>
                }

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
        </Fragment>
    );
}
