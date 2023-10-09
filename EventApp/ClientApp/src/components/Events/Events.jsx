import React, {useEffect, useState} from 'react';
import {Container} from "reactstrap";
import EventBody from "./EventBody";
import isLoggedIn from "../Utils";
import CreateEvent from "./CreateEvent";

const Events = () => {
    const [events, setEvents] = useState([]);

    useEffect( () => {
        const getEvents = async  () => {
            const request = await fetch('https://localhost:7012/Event');
            const response = await request.json();
            setEvents(response)
        }
        getEvents();

    }, []);

    return (
            <Container style={{backgroundColor: ""}}>
                <div>
                    {isLoggedIn() && <CreateEvent />}
                </div>
                {
                    events.length > 0 ? events.map((event, i) => {
                        return (<EventBody {...{...event, x: i}}/>)
                    }) :
                        <Container style={{textAlign: 'center'}}>
                            <h5>There are no any events yet</h5>
                        </Container>
                }
            </Container>
    );
}


export default Events;
