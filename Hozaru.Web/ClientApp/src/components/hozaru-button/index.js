import React, { Component } from 'react';
import PropTypes from 'prop-types';
import './index.css'

export const STATE = {
    LOADING: 'loading',
    DISABLED: 'disabled',
    SUCCESS: 'success',
    ERROR: 'error',
    NOTHING: ''
}

export default class HozaruButton extends Component {
    constructor(props) {
        super();
        this.state = { currentState: props.state || STATE.NOTHING };
    }

    static propTypes = {
        state: PropTypes.oneOf(Object.keys(STATE).map(k => STATE[k])),
        type: PropTypes.string,
        state: PropTypes.oneOf(Object.keys(STATE).map(k => STATE[k]))
    };

    componentWillReceiveProps(nextProps) {
        if (nextProps.state === this.props.state) { return }
        switch (nextProps.state) {
            case STATE.SUCCESS:
                this.setState({ currentState: STATE.NOTHING })
                return
            case STATE.ERROR:
                this.error()
                return
            case STATE.LOADING:
                this.setState({ currentState: STATE.LOADING })
                return
            case STATE.DISABLED:
                this.disable()
                return
            case STATE.NOTHING:
                this.setState({ currentState: STATE.NOTHING })
                return
            default:
                return
        }
    }

    render() {
        const disabled = this.state.currentState === STATE.LOADING ? "disabled" : "";
        return (
            <button type={this.props.type} className={this.props.className + ' hozaru-button'} disabled={disabled}>
                <span> {this.state.currentState !== STATE.LOADING && this.props.children}</span>

                {this.state.currentState === "loading" &&
                    <svg viewBox="0 0 100 100" xmlns="http://www.w3.org/2000/svg">
                        <circle cx="50" cy="50" r="45" />
                    </svg>
                }
            </button>
        );
    }
}