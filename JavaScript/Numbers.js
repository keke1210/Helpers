// Parses the string and if the string is not a valid number is returned as 0
function toNumber(strNum) {
    var numParsed = parseInt(strNum);
    return isNaN(numParsed) ? 0 : numParsed; 
}

// checks if value is a number
function isNumber(num) {
    return !isNaN(num);
}