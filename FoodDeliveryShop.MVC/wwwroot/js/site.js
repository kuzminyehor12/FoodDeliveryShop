// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
const categories = document.querySelectorAll('.btn');
var isClicked = false;

for (const c of categories) {
    c.addEventListener('click', function handleClick() {
        if (isClicked === false) {
            c.classList.add('btn-primary');
            isClicked = true;
        }
        else {
            c.classList.add('btn-dark');
            isClicked = false;
        }
    });
}