import React,{Fragment} from 'react';
import {Router,
    BrowserRouter,
    Route,
    Switch,
    Redirect
} from 'react-router-dom';
import {Container, CssBaseline} from "@material-ui/core";
import Header from "./Components/Header";
import Footer from "./Components/Footer";
import News from "./Pages/News";
import NewsDetails from "./Pages/NewsDetails";
import NewsCategory from "./Pages/NewsCategory";
import Login from "./Pages/Login";
import Register from "./Pages/Register";
import NotFound from "./Pages/NotFound";
import MainFeaturedPost from "./Pages/MainFeaturedPost"
import createBrowserHistory from "history/createBrowserHistory";



const history = createBrowserHistory();
const App = () => {
        return (

            <Fragment>
            <CssBaseline/>
                <Container maxWidth="lg">
                    <BrowserRouter>
                        <Header />
                        <main>


                        <Switch>

                            <Route exact path='/' component={News}/>
                            <Route exact path='/news' component={News}/>
                            <Route path='/news/:id' component={NewsDetails}/>
                            <Route path='/newsCategory/:id' component={NewsCategory}/>
                            <Route path='/login' component={Login}/>
                            <Route path='/register' component={Register}/>
                            <Route path='/register' component={Register}/>

                        </Switch>
                        </main>
                    </BrowserRouter>
                </Container>
                <Footer/>
            </Fragment>

        );
    }

export default App;
