import React from 'react';
import {BrowserRouter, Route} from "react-router-dom";
import {Container, CssBaseline, Grid} from "@material-ui/core";

import Header from "./Components/Header";
import Footer from "./Components/Footer";
import Login from "./Components/Pages/Login";
import News from "./Components/Pages/News";
import Register from "./Components/Pages/Register";
import NewsDetails from "./Components/Pages/NewsDetails";
import NewsCategory from "./Components/Pages/NewsCategory";
import mainFeaturedPost from "./Components/Pages/mainFeaturedPost";

const App = () => {
  return (
    <BrowserRouter>
        {/*<div className="App">*/}
        <CssBaseline/>
        <Container maxWidth="lg">
            <Header/>

          <div className='app-content-wrapper'>
            <Route exact path='/' component={News}/>
            <Route exact path='/news' component={News}/>
            <Route path='/news/:id' component={NewsDetails}/>
            <Route path='/news/category/:id' component={NewsCategory}/>
            <Route path='/login' component={Login}/>
            <Route path='/register' component={Register}/>
          </div>
        {/*</div>*/}
        </Container>
      <Footer/>
     </BrowserRouter>
  );
}
export default App;
