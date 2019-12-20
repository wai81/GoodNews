import React from 'react';
import {makeStyles} from "@material-ui/core";
import {Toolbar} from "@material-ui/core"

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

const Pagination = ({ postsPerPage, totalPosts, paginate }) => {
    const classes = useStyles();
    const pageNumbers = [];

    for (let i = 1; i <= Math.ceil(totalPosts / postsPerPage); i++) {
        pageNumbers.push(i);
    }

    return (

        <div className="pagination">
            <Toolbar component="nav" variant="dense" className={classes.toolbarSecondary}>
            {pageNumbers.map(number => (
                <li key={number} className='page-item'>
                    <a onClick={() => paginate(number)} href='!#' className='page-link'>
                        {number}
                    </a>
                </li>
            ))}
            </Toolbar>
        </div>




    );
};

export default Pagination;