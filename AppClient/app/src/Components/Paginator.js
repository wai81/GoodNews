import React, {Component} from 'react';
import { MDBPagination,
    MDBPageItem,
    MDBPageNav } from "mdbreact";
import AnchorLink from 'react-anchor-link-smooth-scroll';


class Paginator extends Component{

    constructor(props) {
        super(props);
        this.state = {
            page: props.page,
            total: props.total
        };
    }

    componentDidUpdate(prevProps) {
        if(this.props !== prevProps) {
            this.setState({
                page: this.props.page,
                total: this.props.total
            })
        }
    }
    render() {
        const { page, total } = this.state;

        let RenderPages = () => {
            const pageNumbers = [];
            for (let i = (page - 2) > 0 ? page - 2 : (page - 1) > 0 ? page - 1 : page; i <= ((page + 2) < total ? page + 2 : (page + 1) < total ? page + 1 : page); i++) {
                pageNumbers.push(i);
            }

            return(
                pageNumbers.map(number => {
                    return (
                        <MDBPageItem key={number} active={page === number} >
                            <AnchorLink href='#news'>
                                <MDBPageNav onClick={() => this.props.updateData(number)}>
                                    {number}
                                </MDBPageNav>
                            </AnchorLink>
                        </MDBPageItem>
                    );
                }));
        }

        return(
            <MDBPagination className="justify-content-center" size='lg' color="teal">
                <MDBPageItem className={page > 1 ? '' : 'disabled'}>
                    <AnchorLink href='#news'>
                        <MDBPageNav onClick={() => this.props.updateData(page - 1)} aria-label="Previous">
                            <span aria-hidden="true">&laquo;</span>
                            <span className="sr-only">Previous</span>
                        </MDBPageNav>
                    </AnchorLink>
                </MDBPageItem>
                <RenderPages />
                <MDBPageItem>
                    <AnchorLink href='#news'>
                        <MDBPageNav onClick={() => this.props.updateData(page + 1)}>
                            &raquo;
                        </MDBPageNav>
                    </AnchorLink>
                </MDBPageItem>
            </MDBPagination>
        )
    }
}

export default Paginator;


