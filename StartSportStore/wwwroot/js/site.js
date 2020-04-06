// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
let counter = 0;
function count() {
    counter++;
    document.querySelector("#counter").innerHTML = counter;
}
document.addEventListener("DOMContentLoaded", () => {
    setInterval(count, 1000);
});
if (!localStorage.getItem("counter")) {
    localStorage.setItem("counter", 0);
}
function count() {
    let counter = localStorage.getItem("counter");
    document.querySelector("#counter").innerHTML = counter;
    counter++;
}
document.addEventListener("DOMContentLoaded", () => {
    document.addEventListener('button').onclick = () => {
        let counter1 = localStorage.getItem("counter");
        document.querySelector('#counter').innerHTML = counter1;
        counter1++;
        localStorage.setItem('counter', counter1);
    };
    setInterval(count, 1000);
});