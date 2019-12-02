export const pad = function (num, size) {
    var result = num + "";
    while (result.length < size)
        result = "0" + result;
    return result;
}