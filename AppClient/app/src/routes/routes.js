import React from "react";
import { Router, Route, Link } from "react-router-dom";
import createBrowserHistory from "history/createBrowserHistory";
import PropTypes from "prop-types";
import { withStyles } from "@material-ui/core/styles";
import Drawer from "@material-ui/core/Drawer";
import { makeStyles } from '@material-ui/core/styles';
import News from "../Pages/News";
import NewsDetails from "../Pages/NewsDetails";
import NewsCategory from "../Pages/NewsCategory";
import Login from "../Pages/Login";
import Register from "../Pages/Register";
import {Container} from "@material-ui/core";


const useStyles = makeStyles(theme => ({
    root: {
        flexGrow: 1,
        zIndex: 1,
        overflow: "hidden",
        position: "relative",
        display: "flex"
    },

    content: {
        flexGrow: 1,
        backgroundColor: theme.palette.background.default,
        padding: theme.spacing.unit * 3,
        minWidth: 0 // So the Typography noWrap works
    },
    toolbar: theme.mixins.toolbar,
}));

const history = createBrowserHistory();
const Routes = props => {
    const { classes } = props;
    return(
        <div>
             <Router history={history}>
                <div className={classes.root}>
                    <Route exact path='/' component={News}/>
                    <Route exact path='/news' component={News}/>
                    <Route path='/news/:id' component={NewsDetails}/>
                    <Route path='/news/category/:id' component={NewsCategory}/>
                    <Route path='/login' component={Login}/>
                    <Route path='/register' component={Register}/>
                </div>
             </Router>
         </div>
    )
}

export default (Routes);
