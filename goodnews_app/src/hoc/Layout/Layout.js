import React, {Component, Fragment} from "react";
import Header from "../../components/Header/Header";
import MainFeaturedPost from "../../components/Post/MainFeaturedPost";
import NewsPage from "../../containers/NewsPage/NewsPage";
import Footer from "../../components/Footer/Footer";
import Sidebar from "../../components/Sidebar/Sidebar";
import {Container, CssBaseline, Grid} from "@material-ui/core";
import {makeStyles} from "@material-ui/core/styles";


const useStyles = makeStyles(theme => ({
    mainGrid: {
        marginTop: theme.spacing(3),
    },
}));

class Layout  extends  Component {
    render() {
        return(
            <div>
                <CssBaseline/>
                <Container maxWidth="lg">
                <Header/>
                <main>
                    <MainFeaturedPost />
                    <Grid container spacing={6}>
                        <NewsPage/>
                        {/*<Sidebar/>*/}
                    </Grid>
                    </main>
                </Container>
                <Footer/>
            </div>
        )
    }
}
export default Layout