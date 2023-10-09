import React, {useEffect, useState} from "react";
import {Card, Accordion} from "react-bootstrap";
import {Container} from "reactstrap";
import RegisterToEvent from "./RegisterToEvent";


const EventBody = ({id, title, userLimit, dateOfEvent, x}) => {
    const [users, setUsers] = useState([]);

    useEffect( () => {
        const getEvents = async  () => {
            const request = await fetch('https://localhost:7012/Event/'+id);
            const response = await request.json();
            setUsers(response)
        }
        getEvents();

    }, []);

    return (
        <Accordion style={{paddingBottom: '10px'}}>
            <Accordion.Item eventKey={x}>
                <Accordion.Header>{title}</Accordion.Header>
                <Accordion.Body>
                    <Container className="mb-3">
                        <Card >
                            <Card.Body>
                                <Card.Title>{title}</Card.Title>
                                <Card.Subtitle className="mb-2 text-muted">Event date: {dateOfEvent}</Card.Subtitle>
                                <Card.Text>
                                </Card.Text>

                            </Card.Body>
                        </Card>
                    </Container>
                    <Container>
                        <RegisterToEvent id={id}/>
                    </Container>
                    <Container style={{display: 'flex'}}>
                        {users.length > 0 ? users.map((user, i) => {
                            return (
                                <Card style={{ width: '13rem', marginRight: '.5rem' }}>
                                    <Card.Body>
                                        <Card.Title>User: {user.firstName} {user.lastName}</Card.Title>
                                        <Card.Subtitle className="mb-2 text-muted">IdCode: {user.identityCode}</Card.Subtitle>
                                    </Card.Body>
                                </Card>
                            )
                        }) : <h6>No users yet</h6>}
                    </Container>
                </Accordion.Body>
            </Accordion.Item>
        </Accordion>
    )
}

export default EventBody;