/*
Ex. when you need to compare a string date with today,
what you do is parse string date to Date object and remove hours
so that you can compare only by day, month, year
*/
function compareDateWithToday(dateStr){
    // Input type dateStr = "10/12/2020";
    
    // Format string from "dd/MM/yyyy" to "yyyy-MM-dd"    
    let formattedDateStr = dateStr.split("/").reverse().join("-");
    let date = new Date(formattedDateStr);
    let today = new Date();
    
    date.setHours(0, 0, 0, 0);
    today.setHours(0, 0, 0, 0);
    
    console.log(date > today);
    console.log(date < today);
    // ...
}

// Format Date from "dd/MM/yyyy" string to a new Date object without time
function formatDate(date) {
    if (!isEmptyNullOrSpaces(date)) {
        // Format string date from "dd/MM/yyyy" to "yyyy-MM-dd"
        let formattedDateStr = date.split("/").reverse().join("-");
        let myDate = new Date(formattedDateStr);
        // Remove time from dates so they compare only by day/month/year not time
        myDate.setHours(0, 0, 0, 0);
        return myDate;
    }

    return new Date();
}

