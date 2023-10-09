import React, {useState} from "react";
import {Accordion, Col, Form, Row} from "react-bootstrap";
import {Container} from "reactstrap";
import Button from "react-bootstrap/Button";

const RegisterToEvent = ({id}) => {
    const [state, setState] = useState({ firstName: '',lastName: '', identityCode:1 });

    const handleChange = e => {
        const { name, value } = e.target;
        setState(prevState => ({
            ...prevState,
            [name]: value
        }));
    };

    const registerToEvent = async  () => {
        const request = await fetch('https://localhost:7012/event/'+id, {
            method: "PUT",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(state)
        })
        setTimeout(() => {
            window.location.reload(false);
        }, 500);
    }

    return (
        <Accordion>
            <Accordion.Item eventKey="1231" style={{ marginBottom: '10px' }}>
                <Accordion.Header>Register to event</Accordion.Header>
                <Accordion.Body>
                    <Form onSubmit={registerToEvent}>
                        <Container>
                            <Row>
                                <Col>
                                    <Form.Group className="mb-3" >
                                        <Form.Label column sm="6">
                                            First name
                                        </Form.Label>
                                        <Col sm="10">
                                            <Form.Control name="firstName" value={state.firstName}
                                                          onChange={handleChange}/>
                                        </Col>
                                    </Form.Group>
                                </Col>
                                <Col>
                                    <Form.Group className="mb-3" >
                                        <Form.Label column sm="6">
                                            Last name
                                        </Form.Label>
                                        <Col sm="10">
                                            <Form.Control name="lastName" value={state.lastName}
                                                          onChange={handleChange}/>
                                        </Col>
                                    </Form.Group>
                                </Col>
                                <Col>
                                    <Form.Group className="mb-3" >
                                        <Form.Label column sm="6">
                                            Identity code
                                        </Form.Label>
                                        <Col sm="10">
                                            <Form.Control name="identityCode" value={state.identityCode} onChange={handleChange}/>
                                        </Col>
                                    </Form.Group>
                                </Col>
                                <Col >
                                    <Button variant="primary" style={{marginTop: '2rem'}} onClick={registerToEvent}>
                                        Create
                                    </Button>
                                </Col>
                            </Row>
                        </Container>
                    </Form>
                </Accordion.Body>
            </Accordion.Item>
        </Accordion>
    )
}

export default RegisterToEvent;