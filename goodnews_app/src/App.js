import React, { Component } from "react"
import {Route, Switch} from "react-router-dom";
import Layout from "./hoc/Layout/Layout";
import LoginPage from "./containers/LoginPage/LoginPage";
import RegisterPage from "./containers/RegisterPage/RegisterPage";
import NewsDetailPage from "./containers/NewsDetailPage/NewsDetailPage";
import NewsListPage from "./containers/NewsPage/NewsPage";
import NewsByCategoryPage from "./containers/NewsByCategoryPage/NewsByCategoryPage";

class App extends  Component{
    render() {
        return(
            <Layout>
                <Switch>
                    <Route exact path='/' component={NewsListPage}/>
                    <Route exact path='/news' component={NewsListPage}/>
                    <Route path='/news/:id' component={NewsDetailPage} />
                    <Route path='/news/category/:id' component={NewsByCategoryPage} />
                    <Route path='/login' componet = {LoginPage}/>
                    <Route path='/register' componet = {RegisterPage}/>
                  </Switch>
            </Layout>
        )
    }
}
export  default  App