function sign_up() {
    let pass = document.getElementById("password").value;
    let pass2 = document.getElementById("password-repeat").value;
    if (pass != pass2) {
        document.getElementById("message").innerHTML = "Password and password(repeat) fields should coincide";
    } else {
        window.location.href = "landing.html";
    }
}