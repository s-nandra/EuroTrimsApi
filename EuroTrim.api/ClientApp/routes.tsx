import * as React from 'react';
import { Route } from 'react-router-dom';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { FetchProduct } from './components/FetchProducts';
import { AddProduct } from './components/AddProduct';
export const routes = <Layout>
    <Route exact path='/' component={Home} />
    <Route path='/fetchproduct' component={FetchProduct} />
    <Route path='/addproduct' component={AddProduct} />
    <Route path='/product/edit/:empid' component={AddProduct} />
</Layout>;