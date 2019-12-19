import React, { Fragment } from "react"
import {BrowserRouter, Route, Switch} from "react-router-dom";
import LoginPage from "./components/LoginPage/LoginPage";
import RegisterPage from "./components/RegisterPage/RegisterPage";
import NewsDetailPage from "./components/NewsDetailPage/NewsDetailPage";
import NewsPage from "./components/NewsPage/NewsPage";
import NewsByCategoryPage from "./components/NewsByCategoryPage/NewsByCategoryPage";
import {Container, CssBaseline, Grid} from "@material-ui/core";
import Header from "./components/Header/Header";
import Footer from "./components/Footer/Footer";
import MainFeaturedPost from "./components/Post/MainFeaturedPost";

const App = () =>  {
        return(
            <BrowserRouter>
               <CssBaseline/>
                <Container maxWidth="lg">
                    <Header/>
                            <Switch>
                                <Route exact path='/' component={NewsPage}/>
                                <Route exact path='/news' component={NewsPage}/>
                                <Route path='/news/:id' component={NewsDetailPage} />
                                <Route path='/news/category/:id' component={NewsByCategoryPage} />
                                <Route path='/login' componet = {LoginPage}/>
                                <Route path='/register' componet = {RegisterPage}/>
                            </Switch>

                  </Container>
                <Footer/>
            </BrowserRouter>
        );
}
export  default  App