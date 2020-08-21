import React, {Component} from 'react';
import logo from './logo.svg';
import './App.css';
import axios from 'axios';

class App extends Component {
  // provides data for the view when we show it in the browser
  state = {
    values: []
  }

  // trigger a re render of our components
  // axios get returns an AxiosResponse
  componentDidMount() {
    axios.get('http://localhost:5000/api/values')
      .then((response) =>
      {
        console.log(response);
        this.setState({
          values: response.data
        })
      });
  }

  render() {
    return (
      <div className="App">
        <header className="App-header">
          <img src={logo} className="App-logo" alt="logo" />
          <ul>
            {this.state.values.map((value: any) => (
              <li>{value.name}</li>
            ))}
          </ul>
        </header>
      </div>
    );
  }
  
}

export default App;
