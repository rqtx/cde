import React, { Component } from 'react';

export class Home extends Component {
  static displayName = Home.name;

  render() {
    return (
      <>
        <main>
          <h1>Central de Erros</h1>
          <h2>Objetivo</h2>
          <p>Em projetos modernos é cada vez mais comum o uso de arquiteturas baseadas em serviços ou microsserviços. Nestes ambientes complexos, erros podem surgir em diferentes camadas da aplicação (backend, frontend, mobile, desktop) e mesmo em serviços distintos. Desta forma, é muito importante que os desenvolvedores possam centralizar todos os registros de erros em um local, de onde podem monitorar e tomar decisões mais acertadas. Neste projeto vamos implementar um sistema para centralizar registros de erros de aplicações.</p>
          <p>A arquitetura do projeto é formada por:</p>
          <h2>Backend - API</h2>

          <ul>
            <li>criar <em>endpoints</em> para serem usados pelo frontend da aplicação</li>
            <li>criar um <em>endpoint</em> que será usado para gravar os logs de erro em um banco de dados relacional</li>
            <li>a API deve ser segura, permitindo acesso apenas com um token de autenticação válido</li>
          </ul>

          <h2>Frontend</h2>

          <ul>
            <li>deve implementar as funcionalidades apresentadas nos wireframes</li>
            <li>deve ser acessada adequadamente tanto por navegadores desktop quanto mobile</li>
            <li>deve consumir a API do produto</li>
            <li>desenvolvida na forma de uma Single Page Application</li>
          </ul>

          <h2>Observações</h2>

          <ul>
            <li>Se a aceleração tiver ênfase no backend (Java, Python, C#, Go, PHP, etc) a equipe deve obrigatoriamente implementar a API. <strong>A implementação do frontend não é necessária</strong></li>
            <li>Se a aceleração tiver ênfase em frontend (React, Vue, Angular, etc) a equipe deve obrigatoriamente implementar o frontend da aplicação e o backend pode ser substituido por uma aplicação <em>mock</em>. <strong>A implementação da API não é necessária</strong>, caso o time deseje podem ser utilizados mocks.</li>
          </ul>

          <h2>Wireframes</h2>

          <p>Os wireframes a seguir servem para ilustrar as funcionalidades básicas que a aplicação deverá ter, porém o time terá total liberdade para definir os detalhes de implementação e estratégia a ser utilizada no desenvolvimento.</p>

          <p><img src="https://codenation-challenges.s3-us-west-1.amazonaws.com/central-erros/1-cadastro.png" alt="" width="100%" /></p>

          <p><img src="https://codenation-challenges.s3-us-west-1.amazonaws.com/central-erros/2-login.png" alt="" width="100%" /></p>

          <p><img src="https://codenation-challenges.s3-us-west-1.amazonaws.com/central-erros/3-dashboard.png" alt="" width="100%" /></p>

          <p><img src="https://codenation-challenges.s3-us-west-1.amazonaws.com/central-erros/4-ambientes.png" alt="" width="100%" /></p>

          <p><img src="https://codenation-challenges.s3-us-west-1.amazonaws.com/central-erros/5-order.png" alt="" width="100%" /></p>

          <p><img src="https://codenation-challenges.s3-us-west-1.amazonaws.com/central-erros/6-filtro.png" alt="" width="100%" /></p>

          <p><img src="https://codenation-challenges.s3-us-west-1.amazonaws.com/central-erros/7-detalhes.png" alt="" width="100%" /></p>
          <ul>
            <li><a href='https://get.asp.net/'>ASP.NET Core</a> and <a href='https://msdn.microsoft.com/en-us/library/67ef8sbd.aspx'>C#</a> for cross-platform server-side code</li>
            <li><a href='https://facebook.github.io/react/'>React</a> for client-side code</li>
            <li><a href='http://getbootstrap.com/'>Bootstrap</a> for layout and styling</li>
          </ul>
          <p>To help you get started, we have also set up:</p>
          <ul>
            <li><strong>Client-side navigation</strong>. For example, click <em>Counter</em> then <em>Back</em> to return here.</li>
            <li><strong>Development server integration</strong>. In development mode, the development server from <code>create-react-app</code> runs in the background automatically, so your client-side resources are dynamically built on demand and the page refreshes when you modify any file.</li>
            <li><strong>Efficient production builds</strong>. In production mode, development-time features are disabled, and your <code>dotnet publish</code> configuration produces minified, efficiently bundled JavaScript files.</li>
          </ul>
          <p>The <code>ClientApp</code> subdirectory is a standard React application based on the <code>create-react-app</code> template. If you open a command prompt in that directory, you can run <code>npm</code> commands such as <code>npm test</code> or <code>npm install</code>.</p>
        </main>
      </>
    );
  }
}
