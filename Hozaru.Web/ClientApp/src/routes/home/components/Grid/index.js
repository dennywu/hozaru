import React, { Component } from 'react';
import { GridItem } from './GridItem';
import { GridEmpty } from './GridEmpty';
import './Grid.css';
import Loading from '../../../../components/loading';
import axios from 'axios';

export class Grid extends Component {
    constructor(props) {
        super(props);
        this.state = { products: [], loading: true };
    }

    componentDidMount() {
        this.populateProducts();
    }

    async populateProducts() {
        axios.get('/api/products')
            .then(res => {
                this.setState({ products: res.data, loading: false });
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

    static renderGridProduct(products) {
        var productChuncked = Grid.chunkArray(products, 3);

        return (
            <div className="grid-container">
                {productChuncked.map(chuncked =>
                    <div className="product-grid" key={Math.random()}>
                        {Grid.renderGridRow(chuncked)}
                    </div>
                )}
            </div>
        );
    }

    render() {
        let contents = this.state.loading
            ? <Loading />
            : Grid.renderGridProduct(this.state.products);

        return (
            <div>
                {contents}
            </div>
        );
    }
}