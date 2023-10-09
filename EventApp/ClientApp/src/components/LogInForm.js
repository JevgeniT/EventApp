import React, {useState} from 'react';
import Button from "react-bootstrap/Button";
import {Col, Form, Modal, Row} from "react-bootstrap";
import {useNavigate} from "react-router-dom";

const LogInForm = (props) => {
    const [show, setShow] = useState(false);
    const [email, setEmail] = useState('test@test.com');
    const [password, setPassword] = useState('qweqwe');
    const navigate = useNavigate();

    const handleClose = () => setShow(false);
    const handleShow = () => setShow(true);
    const logIn = async  () => {
        const request = await fetch('https://localhost:7012/Identity/login', {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify({email, password}),
        })
        const response = await request.json();
        localStorage.setItem('jwt', response.jwt);
        setTimeout(() => {
            navigate('/events');
            handleClose();
        }, 500);
    }

    return (
        <>
            <Button variant="link" onClick={handleShow}>
                Log in
            </Button>

            <Modal show={show} onHide={handleClose}>
                <Modal.Header closeButton>
                    <Modal.Title>Log in</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <Form onSubmit={logIn}>
                    <Form.Group as={Row} className="mb-3" controlId="formPlaintextEmail">
                        <Form.Label column sm="2">
                            Email
                        </Form.Label>
                        <Col sm="10">
                            <Form.Control defaultValue="test@test.com"
                                          onChange={(e)=> setEmail(e.target.value)}/>
                        </Col>
                    </Form.Group>

                    <Form.Group as={Row} className="mb-3" controlId="formPlaintextPassword">
                        <Form.Label column sm="2">
                            Password
                        </Form.Label>
                        <Col sm="10">
                            <Form.Control type="password" placeholder="qweqwe" muted
                                          onChange={(e)=> setPassword(e.target.value)}/>
                        </Col>
                    </Form.Group>
                    <Button variant="primary" onClick={logIn}>
                        Login
                    </Button>
                </Form>
                </Modal.Body>
            </Modal>
        </>
    );
}

export default LogInForm;