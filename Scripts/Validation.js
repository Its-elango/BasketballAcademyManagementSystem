// Function to validate an address input field
function validateAddress() {
    var address = document.getElementById("address").value;
    var error = document.getElementById("addressError");

    if (address == "") {
        error.textContent = "Address can't be empty";
        return false;
    }
    else {
        error.textContent = " ";
    }
    return true;
}

// Function to validate a name input field
function validatename() {
    var name = document.getElementById("name").value;
    var error = document.getElementById("nameError");
    var word = (/^[A-Za-z]+$/);

    if (name == "") {
        error.textContent = "Name can't be empty";
        return false;
    }
    else if (!word.test(name)) {
        error.textContent = "Name only contains alphabets";
        return false;
    }
    else {
        error.textContent = " ";
    }
    return true;
}

// Function to validate another name input field (pgname)
function validatepgname() {
    var name = document.getElementById("pgname").value;
    var error = document.getElementById("pgnameError");
    var word = (/^[A-Za-z]+$/);

    if (name == "") {
        error.textContent = "Name can't be empty";
        return false;
    }
    else if (!word.test(name)) {
        error.textContent = "Name only contains alphabets";
        return false;
    }
    else {
        error.textContent = " ";
    }
    return true;
}

// Function to validate a phone number input field
function validatePhone() {
    var phone = document.getElementById("phone").value;
    var error = document.getElementById("phoneError");
    var number = /^\d+$/;

    if (phone == "") {
        error.textContent = "Phone number can't be empty";
        return false;
    }
    else if (!number.test(phone)) {
        error.textContent = "Phone number only contains numbers";
        return false;
    }
    else {
        error.textContent = " ";
    }
    return true;
}

// Function to validate another phone number input field (pgphone)
function validatepgPhone() {
    var phone = document.getElementById("pgphone").value;
    var error = document.getElementById("pgphoneError");
    var number = /^\d+$/;

    if (phone == "") {
        error.textContent = "Phone number can't be empty";
        return false;
    }
    else if (!number.test(phone)) {
        error.textContent = "Phone number only contains numbers";
        return false;
    }
    else {
        error.textContent = " ";
    }
    return true;
}

// Function to validate a password input field
function validatePassword() {
    var pass = document.getElementById("password").value;
    var error = document.getElementById("passwordError");

    // Check for empty input
    if (pass == null) {
        error.textContent = "Password can't be empty";
        return false;
    }
    // Check for the password length
    if (pass.length < 8) {
        error.textContent = "Password must be at least 8 characters.";
        return false;
    }

    // Check for at least one uppercase letter
    if (!/[A-Z]/.test(pass)) {
        error.textContent = "Password must contain an uppercase letter.";
        return false;
    }

    // Check for at least one lowercase letter
    if (!/[a-z]/.test(pass)) {
        error.textContent = "Password must contain a lowercase letter.";
        return false;
    }

    // Check for at least one digit
    if (!/\d/.test(pass)) {
        error.textContent = "Password must contain a digit.";
        return false;
    }
    else {
        error.textContent = " ";
    }
    return true;
}

// Function to validate the confirmation of a password
function validateConfirmpassword() {
    var pass = document.getElementById("password").value;
    var confirm_pass = document.getElementById("confirmPassword").value;
    var error = document.getElementById("confirmPasswordError");

    // Check for empty input
    if (pass == null) {
        error.textContent = "Confirm password can't be empty";
        return false;
    }
    if (pass != confirm_pass) {
        error.textContent = "Passwords must be the same";
        return false;
    }
    else {
        error.textContent = " ";
    }
}

// Function to validate an email input field
function validateEmail() {
    var email = document.getElementById("email").value;
    var emailPattern = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/;
    var error = document.getElementById("emailError");

    if (email == "") {
        error.textContent = "Email can't be empty";
        return false;
    }
    else if (!(emailPattern.test(email))) {
        error.textContent = "Enter a valid email";
        return false;
    }
    else {
        error.textContent = " ";
    }
    return true;
}

// Function to validate a username input field
function validateusername() {
    var uname = document.getElementById("username").value;
    var unamePattern = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/;
    var error = document.getElementById("usernameError");

    if (uname == null) {
        error.textContent = "Username can't be empty";
        return false;
    }
    else if (!(unamePattern.test(uname))) {
        error.textContent = "Enter a valid username";
        return false;
    }
    else {
        error.textContent = " ";
    }
    return true;
}

// Function to validate a date input field
function validatedate() {
    var date = document.getElementById("date").value;
    var error = document.getElementById("DateError");

    if (date == "") {
        error.textContent = "Date can't be empty";
        return false;
    }
    return true;
}

// Function to validate a time input field
function validatetime() {
    var date = document.getElementById("time").value;
    var error = document.getElementById("TimeError");

    if (date == "") {
        error.textContent = "Time can't be empty";
        return false;
    }
    return true;
}

// Function to validate a details input field
function validatedetails() {
    var address = document.getElementById("details").value;
    var error = document.getElementById("detailsError");

    if (address == "") {
        error.textContent = "This field can't be empty";
        return false;
    }
    else {
        error.textContent = " ";
    }
    return true;
}

// Function to validate a skill input field
function validateSkill() {
    var address = document.getElementById("skill").value;
    var error = document.getElementById("skillError");

    if (address == "") {
        error.textContent = "Skill can't be empty";
        return false;
    }
    else {
        error.textContent = " ";
    }
    return true;
}

// Function to validate an experience input field
function validateexperience() {
    var phone = document.getElementById("experience").value;
    var error = document.getElementById("experienceError");
    var number = /^\d+$/;

    if (phone == "") {
        error.textContent = "Experience can't be empty";
        return false;
    }
    else if (!number.test(phone)) {
        error.textContent = "Experience only contains numbers";
        return false;
    }
    else {
        error.textContent = " ";
    }
    return true;
}

// Function to validate a gender input (radio buttons)
function validategender() {
    var genderMale = document.getElementById("genderMale");
    var genderFemale = document.getElementById("genderFemale");
    var genderOther = document.getElementById("genderOther");
    var error = document.getElementById("genderError");

    if (!genderMale.checked && !genderFemale.checked && !genderOther.checked) {
        error.textContent = "Choose a gender!";
        return false;
    } else {
        error.textContent = "";
        return true;
    }
}

// Function to validate a payment method (checkboxes)
function validatepayment() {
    var paymentcard = document.getElementById("paymentcard");
    var paymentupi = document.getElementById("paymentupi");
    var paymentpaypal = document.getElementById("paymentpaypal");
    var error = document.getElementById("paymentError");

    if (!paymentcard.checked && !paymentupi.checked && !paymentpaypal.checked) {
        error.textContent = "Choose a payment method!";
        return false;
    } else {
        error.textContent = "";
        return true;
    }
}

// Function to validate a prize input field
function validateprize() {
    var address = document.getElementById("prize").value;
    var error = document.getElementById("prizeError");

    if (address == "") {
        error.textContent = "Prize can't be empty";
        return false;
    }
    else {
        error.textContent = " ";
    }
    return true;
}

// Function to validate a venue input field
function validateVenue() {
    var address = document.getElementById("venue").value;
    var error = document.getElementById("VenueError");

    if (address == "") {
        error.textContent = "Venue can't be empty";
        return false;
    }
    else {
        error.textContent = " ";
    }
    return true;
}

// Function to validate a select input (dropdown)
function validateselect() {
    var month = document.getElementById("month").value;
    var error = document.getElementById("monthError");

    if (month == "") {
        error.textContent = "Select a month!";
        return false;
    }
    else {
        error.textContent = " ";
    }
    return true;
}

// Function to validate a sign-in form
function validateSignin() {
    var isValid = true;
    isValid = validateusername() && isValid;
    isValid = validatePassword() && isValid;
    return isValid;
}

// Function to validate a sign-up form
function validateSignup() {
    var isValid = true;
    isValid = validatename() && isValid;
    isValid = validateusername() && isValid;
    isValid = validatePassword() && isValid;
    isValid = validateEmail() && isValid;
    return isValid;
}

// Function to validate an update coach form
function validateUpdateCoach() {
    var isValid = true;
    isValid = validatename() && isValid;
    isValid = validateEmail() && isValid;
    isValid = validatedate() && isValid;
    isValid = validateAddress() && isValid;
    isValid = validatePhone() && isValid;
    isValid = validateexperience() && isValid;
    isValid = validateSkill() && isValid;
    isValid = validatedate() && isValid;
    isValid = validategender() && isValid;
    return isValid;
}

// Function to validate an enrollment form
function validateEnroll() {
    var isValid = true;
    isValid = validatename() && isValid;
    isValid = validateEmail() && isValid;
    isValid = validatedate() && isValid;
    isValid = validatePhone() && isValid;
    isValid = validateselect() && isValid;
    isValid = validatepgname() && isValid;
    isValid = validatepgPhone() && isValid;
    isValid = validategender() && isValid;
    isValid = validatepayment() && isValid;
    return isValid;
}

// Function to validate adding an admin
function validateAddAdmin() {
    var isValid = true;
    isValid = validatename() && isValid;
    isValid = validateusername() && isValid;
    isValid = validatePassword() && isValid;
    isValid = validateEmail() && isValid;
    return isValid;
}

// Function to validate adding a coach
function validateAddCoach() {
    var isValid = true;
    isValid = validatename() && isValid;
    isValid = validateusername() && isValid;
    isValid = validatePassword() && isValid;
    isValid = validateEmail() && isValid;
    return isValid;
}

// Function to validate a forgot password form
function validateForgotPassword() {
    var isValid = true;
    isValid = validateConfirmpassword() && isValid;
    isValid = validateusername() && isValid;
    isValid = validatePassword() && isValid;
    isValid = validateEmail() && isValid;
    return isValid;
}

// Function to validate a contact form
function validateContact() {
    var isValid = true;
    isValid = validatename() && isValid;
    isValid = validatePhone() && isValid;
    isValid = validatedetails() && isValid;
    isValid = validateEmail() && isValid;
    return isValid;
}
