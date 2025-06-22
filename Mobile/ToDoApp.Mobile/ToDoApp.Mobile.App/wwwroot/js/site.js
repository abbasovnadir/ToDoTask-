window.testFunction = () => {
    $("#myButton").text("Clicked!");
};

function togglePasswordVisibility(element, show) {
    element.type = show ? "text" : "password";
}

function logLogoutSuccess() {
    window.location.href = "/Account/Login";
}