import React, { Component } from "react"
import {Route, Switch} from "react-router-dom";
import Layout from "./hoc/Layout/Layout";
import LoginPage from "./containers/LoginPage/LoginPage";
import RegisterPage from "./containers/RegisterPage/RegisterPage";
import NewsDetailPage from "./containers/NewsDetailPage/NewsDetailPage";
import NewsListPage from "./containers/NewsListPage/NewsListPage";

class App extends  Component{
    render() {
        return(
            <Layout>
                <Switch>
                    <Route path="/login" componet = {LoginPage}/>
                    <Route path="/register" componet = {RegisterPage}/>
                    <Route path='/:id' component={NewsDetailPage} />
                    <Route path='/' component={NewsListPage} />
                </Switch>
            </Layout>
        )
    }
}
export  default  App