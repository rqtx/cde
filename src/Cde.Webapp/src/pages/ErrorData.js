import React, { Component } from 'react'
import classnames from 'classnames'

import {
  FormGroup,
  InputGroup,
  InputGroupAddon,
  InputGroupText,
  Input,
  Container,
  Row,
  Card,
  Col
} from 'reactstrap'

export class ErrorData extends React.Component {
  static displayName = ErrorData.name;

  constructor(props) {
    super(props);
    this.state = { log: [], loading: true };
  }

  componentDidMount() {
    this.populateErrorData();
  }

  static renderLogsTable(logs) {
    return (
      <Container className="">
        <Row className="">

          <Card className="bg-secondary shadow border-0"></Card>
          <table className='table table-striped' aria-labelledby="tabelLabel">

            <thead>
              <tr>
                <th className="custom-control-alternative custom-checkbox">
                  <input type="checkbox" class="form-check-input" id="exampleCheck1" />
                </th>
                <th>Level</th>
                <th>Log</th>
                <th>Envet</th>
                <th>Date</th>
              </tr>
            </thead>
            <tbody>
              {logs.map(log =>
                <tr key={log.date}>
                  <td className="custom-control custom-control-alternative custom-checkbox">
                    <input type="checkbox" class="form-check-input" id="exampleCheck1" />
                  </td>
                  <td>{log.level}</td>
                  <td>{log.title}</td>
                  <td>{log.details}</td>
                  <td>{log.createdAt}</td>
                </tr>
              )}
            </tbody>
          </table>

        </Row>
      </Container>
    );
  }

  render() {
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
      : ErrorData.renderLogsTable(this.state.logs);

    return (
      <div>
        <h1 id="tabelLabel" >Wellcome User. Token: </h1>
        <p>This component demonstrates fetching data from the server.</p>
        <div className="searchForm">
          <FormGroup
            className={classnames({
              focused: this.state.searchFocused
            })}
          >
            <InputGroup className="mb-4">
              <div className="dropdown">
                <select className="btn btn-secondary dropdown-toggle" type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                  <option className="dropdown-item" selected value="production">Production</option>
                  <option className="dropdown-item" value="homologation">Homologation</option>
                  <option className="dropdown-item" value="development">Development</option>
                </select>
                <select className="btn btn-secondary dropdown-toggle" type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                  <option selected value="orderby">Order by</option>
                  <option value="level">Level</option>
                  <option value="frequency">Frequeny</option>
                </select>
                <select className="btn btn-secondary dropdown-toggle" type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                  <option selected value="searchby">Search by</option>
                  <option value="homologation">Level</option>
                  <option value="descripition">Descripition</option>
                  <option value="origin">Origin</option>
                </select>
              </div>
              {/* <input
              placeholder="Search for..."
              value={this.state.query}
              onChange={this.handleInputChange}
            /> */}
              <Input
                placeholder="Search"
                type="text"
                onFocus={e => this.setState({ searchFocused: true })}
                onBlur={e => this.setState({ searchFocused: false })}
              />
              <InputGroupAddon addonType="append">
                <InputGroupText>
                  <i className="ni ni-zoom-split-in" />
                </InputGroupText>
              </InputGroupAddon>
            </InputGroup>
          </FormGroup>
        </div>
        {contents}
      </div>
    );
  }

  async populateErrorData() {
    //const token = await authService.getAccessToken();
    const response = await fetch('log', {
      //headers: !token ? {} : { 'Authorization': `Bearer ${token}` }
    });
    const data = await response.json();
    this.setState({ logs: data, loading: false });
  }
}
