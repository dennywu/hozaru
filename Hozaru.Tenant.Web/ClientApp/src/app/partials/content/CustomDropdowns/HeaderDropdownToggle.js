import React from "react";

const HeaderDropdownToggle = React.forwardRef(({ children, onClick }, ref) => (
    <div
        className="kt-header__topbar-wrapper"
        ref={ref}
        onClick={e => {
            e.preventDefault();
            onClick(e);
        }}
    >
        {children}
    </div>
));

export default HeaderDropdownToggle;

//export default class HeaderDropdownToggle extends React.Component {
//    constructor(props, context) {
//        super(props, context);
//        this.handleClick = this.handleClick.bind(this);
//    }

//    handleClick(e) {
//        e.preventDefault();
//        this.props.onClick(e);
//    }

//    render() {
//        const SampleButton = React.forwardRef((props, ref) => (
//            <div ref={this.newRef}
//                className="kt-header__topbar-wrapper"
//                onClick={this.handleClick}
//            >
//                {props.children}
//            </div>
//        ));
//        return SampleButton;
//        //return (
//        //    <div ref={this.newRef}
//        //        className="kt-header__topbar-wrapper"
//        //        onClick={this.handleClick}
//        //    >
//        //        {this.props.children}
//        //    </div>
//        //);
//    }
//}
