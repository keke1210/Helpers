// Function that doesn't throw error when it tries to parse JSON
function IsValidJSON (jsonString) {
    try {
        let json = JSON.parse(jsonString);
        return (typeof json === 'object');
    }
    catch (e) {
        return false;
    }
};

// Function that tries to parse JSON and then returns the object if it's parsed else returns false
function tryParseJSON (jsonString){
    try {
        var o = JSON.parse(jsonString);

        if (o && typeof o === "object") {
            return o;
        }
    }
    catch (e) { }

    return false;
}

// Regex that checks if JSON string is valid and can be parsed into JSON object
function IsValidJsonString (jsonString) {
    if (/^[\],:{}\s]*$/.test(jsonString.replace(/\\["\\\/bfnrtu]/g, '@').
        replace(/"[^"\\\n\r]*"|true|false|null|-?\d+(?:\.\d*)?(?:[eE][+\-]?\d+)?/g, ']').
        replace(/(?:^|:|,)(?:\s*\[)+/g, ''))) {
            //the json is ok
            return true;
        }

    return false;
};