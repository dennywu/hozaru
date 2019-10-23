import React, { Component } from 'react';
import PropTypes from 'prop-types';
import { connect } from 'react-redux';
import { changeNote } from '../../../../services/shopping-cart/actions';

class Note extends Component {
    constructor() {
        super();
        this.handleChangeNote = this.handleChangeNote.bind(this);
    }

    static propTypes = {
        note: PropTypes.string
    };

    handleChangeNote(event) {
        var note = event.target.value;
        this.props.changeNote(note);
    }

    render() {
        return (
            <div className="container mt-2 mb-2 ">
                <div className="row">
                    <div className="col-12 pt-2 pb-2">
                        <div className="input-group">
                            <div className="input-group-prepend">
                                <span className="input-group-text">Pesan</span>
                            </div>
                            <textarea
                                className="form-control text-right font-12px"
                                rows="1"
                                placeholder="Silakan tinggalkan pesan (jika ada)"
                                defaultValue={this.props.note}
                                onBlur={this.handleChangeNote}
                            >
                            </textarea>
                        </div>
                    </div>
                </div>
            </div>
        );
    }
}

const mapStateToProps = state => ({
    note: state.shoppingCart.note
});

export default connect(mapStateToProps, { changeNote })(Note);