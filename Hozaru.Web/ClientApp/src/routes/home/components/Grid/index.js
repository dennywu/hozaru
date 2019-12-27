import React, { Component } from 'react';
import { GridItem } from './GridItem';
import { GridEmpty } from './GridEmpty';
import { GridLoading } from './GridLoading';
import './Grid.css';
import Loading from '../../../../components/loading';
import axios from 'axios';
import debounce from "lodash.debounce";

export class Grid extends Component {
    constructor(props) {
        super(props);
        this.handleScroll = this.handleScroll.bind(this);
        this.state = {
            products: [],
            loading: false,
            hasMore: true
        };
    }

    componentDidMount() {
        this.populateProducts();
        window.addEventListener('scroll', this.handleScroll, true);
    }

    componentWillUnmount() {
        window.removeEventListener('scroll', this.handleScroll);
    }

    handleScroll() {
        if (this.state.loading || !this.state.hasMore) return;

        if (window.innerHeight + document.documentElement.scrollTop === document.documentElement.offsetHeight) {
            this.populateProducts();
        }
    }

    async populateProducts() {
        this.setState({ loading: true });
        axios.get('/api/products', {
            params: {
                status: "active",
                skipCount: this.state.products.length > 0 ? this.state.products.length : 0,
                maxResultCount: 3
            }
        }).then(res => {

            this.setState({
                products: [...this.state.products, ...res.data.items],
                loading: false
            }, () => {
                let hasMore = res.data.totalCount > this.state.products.length;
                this.setState({ hasMore: hasMore });
            });
        });
    }

    static chunkArray(myArray, chunk_size) {
        var index = 0;
        var arrayLength = myArray.length;
        var tempArray = [];

        for (index = 0; index < arrayLength; index += chunk_size) {
            var myChunk = myArray.slice(index, index + chunk_size);
            tempArray.push(myChunk);
        }

        return tempArray;
    }

    static renderGridRow(products) {
        var grids = [];
        for (var i = 0; i < 3; i++) {
            if (products[i] != null) {
                var product = products[i];
                grids.push(<GridItem key={product.id} product={product} />);
            }
            else {
                grids.push(<GridEmpty key={Math.random()} />);
            }
        }
        return grids;
    }

    static renderGridLoading() {
        var grids = [];
        for (var i = 0; i < 3; i++) {
            grids.push(<GridLoading key={Math.random()} />);
        }
        return grids;
    }

    render() {
        var productChuncked = Grid.chunkArray(this.state.products, 3);

        return (
            <div className="grid-container mb-85px">
                {productChuncked.map(chuncked =>
                    <div className="product-grid" key={Math.random()}>
                        {Grid.renderGridRow(chuncked)}
                    </div>
                )}
                {
                    this.state.loading &&
                    <div className="product-grid" key={Math.random()}>
                        {Grid.renderGridLoading()}
                    </div>
                }
            </div>
        );
    }
}