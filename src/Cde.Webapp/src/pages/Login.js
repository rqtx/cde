import React, { Component } from 'react'

import {
  Button, Card, CardHeader, CardBody, FormGroup, Form, Input,
  InputGroupAddon, InputGroupText, InputGroup, Container, Row, Col
} from 'reactstrap'

export class Login extends Component {
  static displayName = Login.name

  constructor(props) {
    super(props);

    this.state = {
      message: undefined
    };
  }

  //   componentDidMount() {
  //     const action = this.props.action;
  //     switch (action) {
  //         case LoginActions.Login:
  //             this.login(this.getReturnUrl());
  //             break;
  //         case LoginActions.LoginCallback:
  //             this.processLoginCallback();
  //             break;
  //         case LoginActions.LoginFailed:
  //             const params = new URLSearchParams(window.location.search);
  //             const error = params.get(QueryParameterNames.Message);
  //             this.setState({ message: error });
  //             break;
  //         case LoginActions.Profile:
  //             this.redirectToProfile();
  //             break;
  //         case LoginActions.Register:
  //             this.redirectToRegister();
  //             break;
  //         default:
  //             throw new Error(`Invalid action '${action}'`);
  //     }
  // }

  render() {
    return (
      <>
        <main ref="main">
          <section className="section section-shaped section-lg">
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
            <Container className="pt-lg-7">
              <Row className="justify-content-center">
                <Col lg="5">
                  <Card className="bg-secondary shadow border-0">
                    <CardHeader className="bg-white pb-5">
                      <div className="text-muted text-center mb-3">
                        <small>Sign up with</small>
                      </div>
                      <div className="text-center">
                        <Button
                          className="btn-neutral btn-icon mr-4"
                          color="default"
                          href="#pablo"
                          onClick={e => e.preventDefault()}
                        >
                          <span className="btn-inner--icon mr-1">
                            <img
                              alt="..."
                              src={require("../assets/img/icons/github.svg")}
                            />
                          </span>
                          <span className="btn-inner--text">Github</span>
                        </Button>
                        <Button
                          className="btn-neutral btn-icon ml-1"
                          color="default"
                          href="#pablo"
                          onClick={e => e.preventDefault()}
                        >
                          <span className="btn-inner--icon mr-1">
                            <img
                              alt="..."
                              src={require("../assets/img/icons/google.svg")}
                            />
                          </span>
                          <span className="btn-inner--text">Google</span>
                        </Button>
                      </div>
                    </CardHeader>
                    <CardBody className="px-lg-5 py-lg-5">
                      <div className="text-center text-muted mb-4">
                        <small>Or sign in with credentials</small>
                      </div>
                      <Form role="form">
                        <FormGroup className="mb-3">
                          <InputGroup className="input-group-alternative">
                            <InputGroupAddon addonType="prepend">
                              <InputGroupText>
                                <i className="ni ni-email-83" />
                              </InputGroupText>
                            </InputGroupAddon>
                            <Input placeholder="Email" type="email" />
                          </InputGroup>
                        </FormGroup>
                        <FormGroup>
                          <InputGroup className="input-group-alternative">
                            <InputGroupAddon addonType="prepend">
                              <InputGroupText>
                                <i className="ni ni-lock-circle-open" />
                              </InputGroupText>
                            </InputGroupAddon>
                            <Input
                              placeholder="Password"
                              type="password"
                              autoComplete="off"
                            />
                          </InputGroup>
                        </FormGroup>
                        <div className="custom-control custom-control-alternative custom-checkbox">
                          <input
                            className="custom-control-input"
                            id=" customCheckLogin"
                            type="checkbox"
                          />
                          <label
                            className="custom-control-label"
                            htmlFor=" customCheckLogin"
                          >
                            <span>Remember me</span>
                          </label>
                        </div>
                        <div className="text-center">
                          <Button
                            className="my-4"
                            color="primary"
                            type="button"
                          >
                            Sign in
                          </Button>
                        </div>
                      </Form>
                    </CardBody>
                  </Card>
                  <Row className="mt-3">
                    <Col xs="6">
                      <a
                        className="text-light"
                        href="#pablo"
                        onClick={e => e.preventDefault()}
                      >
                        <small>Forgot password?</small>
                      </a>
                    </Col>
                    <Col className="text-right" xs="6">
                      <a
                        className="text-light"
                        href="#pablo"
                        onClick={e => e.preventDefault()}
                      >
                        <small>Create new account</small>
                      </a>
                    </Col>
                  </Row>
                </Col>
              </Row>
            </Container>
          </section>
        </main>
      </>
    )
  }
  //   getReturnUrl(state) {
  //     const params = new URLSearchParams(window.location.search);
  //     const fromQuery = params.get(QueryParameterNames.ReturnUrl);
  //     if (fromQuery && !fromQuery.startsWith(`${window.location.origin}/`)) {
  //         // This is an extra check to prevent open redirects.
  //         throw new Error("Invalid return url. The return url needs to have the same origin as the current page.")
  //     }
  //     return (state && state.returnUrl) || fromQuery || `${window.location.origin}/`;
  // }

  // redirectToRegister() {
  //     this.redirectToApiAuthorizationPath(`${ApplicationPaths.IdentityRegisterPath}?${QueryParameterNames.ReturnUrl}=${encodeURI(Register)}`);
  // }

  // redirectToProfile() {
  //     this.redirectToApiAuthorizationPath(ApplicationPaths.IdentityManagePath);
  // }

  // redirectToApiAuthorizationPath(apiAuthorizationPath) {
  //     const redirectUrl = `${window.location.origin}${apiAuthorizationPath}`;
  //     // It's important that we do a replace here so that when the user hits the back arrow on the
  //     // browser he gets sent back to where it was on the app instead of to an endpoint on this
  //     // component.
  //     window.location.replace(redirectUrl);
  // }

  // navigateToReturnUrl(returnUrl) {
  //     // It's important that we do a replace here so that we remove the callback uri with the
  //     // fragment containing the tokens from the browser history.
  //     window.location.replace(returnUrl);
  // }
}