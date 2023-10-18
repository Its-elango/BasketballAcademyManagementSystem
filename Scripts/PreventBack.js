// Prevent the user from navigating back in the browser's history.
window.history.forward();

// Define a function that continuously prevents navigation to the previous page.
function noBack() {
    window.history.forward();
}
