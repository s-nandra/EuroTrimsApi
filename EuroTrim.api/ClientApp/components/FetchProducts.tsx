import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import { Link, NavLink } from 'react-router-dom';
interface FetchProductDataState {
    empList: ProductData[];
    loading: boolean;
}
export class FetchProduct extends React.Component<RouteComponentProps<{}>, FetchProductDataState> {
    constructor() {
        super();
        this.state = { empList: [], loading: true };
        fetch('api/Products/')
            .then(response => response.json() as Promise<ProductData[]>)
            .then(data => {
                this.setState({ empList: data, loading: false });
            });
        // This binding is necessary to make "this" work in the callback  
        this.handleDelete = this.handleDelete.bind(this);
        this.handleEdit = this.handleEdit.bind(this);
    }
    public render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : this.renderProductTable(this.state.empList);
        return <div>
            <h1>Product Data</h1>
            <p>This component demonstrates fetching Product data from the server.</p>
            <p>
                <Link to="/addProduct">Create New</Link>
            </p>
            {contents}
        </div>;
    }
    // Handle Delete request for an Product  
    private handleDelete(id: number) {
        if (!confirm("Do you want to delete Product with Id: " + id))
            return;
        else {
            fetch('api/Products/' + id, {
                method: 'delete'
            }).then(data => {
                this.setState(
                    {
                        empList: this.state.empList.filter((rec) => {
                            return (rec.id != id);
                        })
                    });
            });
        }
    }
    private handleEdit(id: number) {
        this.props.history.push("/Product/edit/" + id);
    }
    // Returns the HTML table to the render() method.  
    private renderProductTable(empList: ProductData[]) {
        return <table className='table'>
            <thead>
                <tr>
                    <th></th>
                    <th>ProductId</th>
                    <th>Name</th>
           
                </tr>
            </thead>
            <tbody>
                {empList.map(emp =>
                    <tr key={emp.id}>
                        <td></td>
                        <td>{emp.id}</td>
                        <td>{emp.prodName}</td>
                        <td>{emp.buyPrice}</td>
                        <td>{emp.colour}</td>
                        <td>{emp.dutyPrice}</td>
                        <td>{emp.listPrice}</td>
                        <td>{emp.per}</td>
                        <td>{emp.quantity}</td>
                   
                        <td>
                            <a className="action" onClick={(id) => this.handleEdit(emp.id)}>Edit</a>  |
                            <a className="action" onClick={(id) => this.handleDelete(emp.id)}>Delete</a>
                        </td>
                    </tr>
                )}
            </tbody>
        </table>;
    }
}
export class ProductData {
    id: number = 0;
    prodName: string = "";
    buyPrice: number = 0;
    colour: string = "";
    dutyPrice: number = 0;
    listPrice: number = 0;
    partNo: number = 0;
    per: number = 0;
    quantity: number = 0;

}