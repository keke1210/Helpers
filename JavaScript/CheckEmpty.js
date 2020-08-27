/*
Checks if parameter is undefined, null, empty-string, or whitespaces
Very useful when retrieving data from UI
*/
function isEmptyNullOrSpaces(str) {
    return typeof str === 'undefined' || str === null || str.match(/^ *$/) !== null;
}