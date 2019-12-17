import React from 'react';
import ReactDOM from 'react-dom';
import { BrowserRouter } from 'react-router-dom';

import Header from "./Header";
import Footer from "./Footer";
import MainFeaturedPost from "./MainFeaturedPost";
import FeaturedPost from "./FeaturedPost";
import Sidebar from "./Sidebar";
import Routes from './routes/routes';

export {
    Header,
    Footer,
    MainFeaturedPost,
    FeaturedPost,
    Sidebar
}
ReactDOM.render((
    <BrowserRouter>
        <Routes />
    </BrowserRouter>
), document.getElementById('root'));
