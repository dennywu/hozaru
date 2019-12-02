import React, { Component } from "react";
import PropTypes from "prop-types";
import Button from '@material-ui/core/Button';
import { Row, Col } from "react-bootstrap";

class Toolbar extends Component {
    static propTypes = {
        order: PropTypes.object.isRequired
    };

    render() {
        return (
            <Row>
                <Col xs={12}>
                    <Button variant="contained" color="primary">Approve</Button>
                </Col>
                <Col xs={12} className="mt-10px">
                    <Button variant="contained" color="warning">Reject</Button>
                </Col>
            </Row>
        );
    }
}

export default Toolbar;