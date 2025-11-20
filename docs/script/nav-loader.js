document.addEventListener("DOMContentLoaded", () => {
fetch("partials/nav.html")
.then(res => res.text())
.then(html => {
document.getElementById("nav-placeholder").innerHTML = html;
});
});