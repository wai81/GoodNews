import React, { Component, Fragment, useEffect, useState } from "react"
import { BrowserRouter} from 'react-router-dom';
import { makeStyles } from '@material-ui/core/styles';
import {Grid, Typography,Container,CssBaseline } from '@material-ui/core';
import GitHubIcon from '@material-ui/icons/GitHub';
import FacebookIcon from '@material-ui/icons/Facebook';
import TwitterIcon from '@material-ui/icons/Twitter';
import { Header, Footer, MainFeaturedPost, FeaturedPost, Sidebar } from './Layouts';
import Exercises from './Exercises'
import Main from "./Layouts/Main";


const useStyles = makeStyles(theme => ({
    mainGrid: {
        marginTop: theme.spacing(3),
    },
}));


const sections = [
    //{ title: 'Technology', url: '#' },
    //{ title: 'Design', url: '#' },
    //{ title: 'Culture', url: '#' },
    //{ title: 'Business', url: '#' },
    //{ title: 'Politics', url: '#' },
    //{ title: 'Opinion', url: '#' },
    //{ title: 'Science', url: '#' },
    //{ title: 'Health', url: '#' },
    //{ title: 'Style', url: '#' },
    //{ title: 'Travel', url: '#' },
];

const mainFeaturedPost = {
    title: 'Title of a longer featured blog post',
    description:
        "Multiple lines of text that form the lede, informing new readers quickly and efficiently about what's most interesting in this post's contents.",
    image: 'https://source.unsplash.com/random',
    imgText: 'main image description',
    linkText: 'Continue reading…',
};

const featuredPosts = [
    {
        title: 'Featured post',
        dateCreate: 'Nov 12',
        newsDescription:
            'This is a wider card with supporting text below as a natural lead-in to additional content.',
        image: 'https://source.unsplash.com/random',
        imageText: 'Image Text',
    },
    {
        title: 'Post title',
        dateCreate: 'Nov 11',
        newsDescription:
            'This is a wider card with supporting text below as a natural lead-in to additional content.',
        image: 'https://source.unsplash.com/random',
        imageText: 'Image Text',
    },
];
//const posts = [post1, post2, post3];
const sidebar = {
    title: 'Кратко о проекте',
    description:
        'Данное решение проекта позволяет загружать новости в базу, проводить лематизацию, проводить анализ содержания текста и устанавливать индекс положительности новости.',
    archives: [
        { title: 'March 2020', url: '#' },
        { title: 'February 2020', url: '#' },
        { title: 'January 2020', url: '#' },
        { title: 'November 1999', url: '#' },
        { title: 'October 1999', url: '#' },
        { title: 'September 1999', url: '#' },
        { title: 'August 1999', url: '#' },
        { title: 'July 1999', url: '#' },
        { title: 'June 1999', url: '#' },
        { title: 'May 1999', url: '#' },
        { title: 'April 1999', url: '#' },
    ],
    social: [
        { name: 'GitHub', icon: GitHubIcon },
        { name: 'Twitter', icon: TwitterIcon },
        { name: 'Facebook', icon: FacebookIcon },
    ],
};

export default class extends Component {
    render() {
        //const classes = useStyles();
        return(
         <BrowserRouter>
            <Fragment>
             <CssBaseline/>
             <Container maxWidth="lg">
                <Header title="Good News" sections={sections}/>
                    <main>
                        <MainFeaturedPost post={mainFeaturedPost} />
                        <Grid container spacing={4}>
                            {featuredPosts.map(post => (
                                <FeaturedPost key={post.title} post={post} />
                            ))}
                        </Grid>

                        <Grid container spacing={5} >
                            <Main  title="From the firehose" posts={posts}/>
                            <Sidebar
                                title={sidebar.title}
                                description={sidebar.description}
                               />
                        </Grid>
                    </main>
              </Container>
             <Footer title="Good News" description="Мое первое решение"/>
            </Fragment>
        </BrowserRouter>
        );
    }
}