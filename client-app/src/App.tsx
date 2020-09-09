import React, {Component} from 'react';
import './App.css';
import axios from 'axios';
import { Header, Icon } from 'semantic-ui-react';

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
      <div>
        <Header as='h2'>
          <Icon name='users'/>
          <Header.Content>Reactivities</Header.Content>
        </Header>
          <ul>
            {this.state.values.map((value: any) => (
              <li key={value.id}>{value.name}</li>
            ))}
          </ul>
      </div>
    );
  }
  
}

export default App;
