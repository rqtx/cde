import React, { Component } from 'react'

import './Footer.css'

export class Footer extends Component {
  static displayName = Footer.name

  render() {
    return (
      <>
        <footer className="footer">
          <div className="shape shape-style-1 bg-gradient-default">
            <span />
            <span />
            <span />
            <span />
            <span />
            <span />
            <span />
            <span />
          </div>

          <div className="container">
            <div className="row align-items-center">
              <div className="col-md-4">
                <span className="copyright">Brasília. Todos os direitos reservados&copy;2020</span>
              </div>
              <div className="col-md-4">
                <ul className="list-inline social-buttons">
                  <a className="element-icon social-icon social-icon-facebook" href="https://facebook.com/"
                    target=" _blank">
                    <span className="screen-only">Facebook</span>
                    <i className="fab fa-facebook"></i> </a>
                  <a className="element-icon social-icon social-icon-instagram" href="https://www.instagram.com/"
                    target=" _blank">
                    <span className="screen-only">Instagram</span>
                    <i className="fab fa-instagram"></i> </a>
                  <a className="element-icon social-icon social-icon-whatsapp"
                    href="https://api.whatsapp.com/send?1=pt_BR&amp;phone=5561998072935" target=" _blank">
                    <span className="screen-only">Whatsapp</span>
                    <i className="fab fa-whatsapp"></i> </a>
                </ul>
              </div>
              <div className="col-md-4">
                <ul className="list-inline quicklinks">
                  <li className="list-inline-item">
                    <a href=" #">Politica de Privacidade</a>
                  </li>
                  <li className="list-inline-item">
                    <a href=" #">Termos de Uso</a>
                  </li>
                </ul>
              </div>
            </div>
          </div>
        </footer>
      </>
    )
  }
}