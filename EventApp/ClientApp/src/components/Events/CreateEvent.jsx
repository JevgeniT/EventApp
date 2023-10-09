import React from 'react';
import * as formik from 'formik';
import * as yup from 'yup';
import Button from 'react-bootstrap/Button';
import {Accordion, Col, Form, Row} from 'react-bootstrap';

const EventForm = () => {
    const { Formik } = formik;

    const schema = yup.object().shape({
        title:yup.string().matches(/.{4,}/, {
                excludeEmptyString: true,
                message: 'must be 4 characters',
            }),
        usersLimit: yup.number().required().min(1, "must be greater than 1"),
    });

    const createEvent = async  (event) => {
        const request = await fetch('https://localhost:7012/event', {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                "Authorization" : `Bearer ${localStorage.getItem("jwt")}`
            },
            body: JSON.stringify(event)
        })
        setTimeout(() => {
            window.location.reload(false);
        }, 500);
    }

    return (
        <Accordion>
            <Accordion.Item eventKey="1231" style={{ marginBottom: '10px', backgroundColor: '#f6f6f6' }}>
                <Accordion.Header>Create event</Accordion.Header>
                <Accordion.Body style={{ marginBottom: '10px' }}>
                    <Formik
                        validationSchema={schema}
                        onSubmit={createEvent}
                        initialValues={{
                            title: '',
                            usersLimit: 1,
                            dateOfEvent: Date.now()
                        }}
                    >
                        {({ handleSubmit, handleChange, values, touched, errors }) => (
                            <Form noValidate onSubmit={handleSubmit}>
                                <Row className="mb-3">
                                    <Form.Group as={Col} md="3" controlId="validationFormik01">
                                        <Form.Label>Title</Form.Label>
                                        <Form.Control
                                            type="text"
                                            name="title"
                                            value={values.title}
                                            onChange={handleChange}
                                            isInvalid={errors.title}
                                        />
                                        <Form.Control.Feedback type="invalid">
                                            {errors.title}
                                        </Form.Control.Feedback>
                                    </Form.Group>

                                    <Form.Group as={Col} md="3" controlId="validationFormik05">
                                        <Form.Label>Users limit</Form.Label>
                                        <Form.Control
                                            type="number"
                                            placeholder="1"
                                            name="usersLimit"
                                            value={values.usersLimit}
                                            onChange={handleChange}
                                            isInvalid={!!errors.usersLimit}
                                        />
                                        <Form.Control.Feedback type="invalid">
                                            {errors.usersLimit}
                                        </Form.Control.Feedback>
                                    </Form.Group>

                                    <Form.Group as={Col} md="3" controlId="validationFormik05">
                                        <Form.Label>Date of the event</Form.Label>
                                        <Form.Control
                                            type="date"
                                            placeholder="1"
                                            name="dateOfEvent"
                                            value={values.dateOfEvent}
                                            onChange={handleChange}
                                            isInvalid={!!errors.dateOfEvent}
                                        />
                                        <Form.Control.Feedback type="invalid">
                                            {errors.usersLimit}
                                        </Form.Control.Feedback>
                                    </Form.Group>
                                    <Col style={{marginTop: '2rem'}}>
                                        <Button type="submit">Create</Button>
                                    </Col>
                                </Row>

                            </Form>
                        )}
                    </Formik>
                </Accordion.Body>
            </Accordion.Item>
        </Accordion>
    )
}

export default EventForm;