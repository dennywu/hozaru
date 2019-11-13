import { pad } from './NumberUtil';

var formatDate = function (date) {
    var monthNames = [
        "Jan", "Feb", "Mar",
        "Apr", "Mei", "Jun", "Jul",
        "Agust", "Sept", "Oct",
        "Nov", "Des"
    ];

    var day = pad(date.getDate(), 2);
    var monthIndex = date.getMonth();
    var year = date.getFullYear();

    return day + ' ' + monthNames[monthIndex] + ' ' + year;
}

var formatDateTime = function (date) {
    var dateString = formatDate(date);
    var hour = pad(date.getHours(), 2);
    var minute = pad(date.getMinutes(), 2);
    //var second = pad(date.getSeconds(), 2);
    return dateString + ' ' + hour + ':' + minute;
}

export const dateFormat = function (date) {
    var newDate = new Date(date);
    return formatDate(newDate);
};

export const dateTimeFormat = function (date) {
    var newDate = new Date(date);
    return formatDateTime(newDate);
}
