// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const menuBox = document.querySelector(".menu-box");
const burger = document.querySelector(".burger");
const filterButtons = document.querySelectorAll(".filter-btn");
const filter = document.querySelector(".filter");
const filterBar = document.querySelector(".filter-bar");
const applyFilter = document.querySelector(".apply-filter");
const searchInput = document.getElementById("search");
function toggleMenu() {
    if (menuBox.classList.contains("showMenu")) {
        menuBox.classList.remove("showMenu");
    } else {
        menuBox.classList.add("showMenu");
    }
}

burger.addEventListener("click", toggleMenu);


filter.addEventListener("click", () => {
    if (filterBar.style.display === "none" || filterBar.style.display === "") {
        filterBar.style.opacity = "0";
        filterBar.style.display = "block";
        setTimeout(() => {
            filterBar.style.opacity = "1";
        }, 10);
    } else {
        filterBar.style.opacity = "0";
        setTimeout(() => {
            filterBar.style.display = "none";
        }, 500);
    }
});

function activateFilterButton(event) {
    const clickedButton = event.target;
    clickedButton.classList.toggle("filter-active");
}


filterButtons.forEach((button) => {
    button.addEventListener("click", activateFilterButton);

})

searchInput.addEventListener("keydown", function (event) {
    if (event.key === "Enter") {
        const searchTerm = searchInput.value;
        const encodedSearchTerm = encodeURIComponent(searchTerm);
        window.location.href = "/searchresult?search=" + encodedSearchTerm;
    }
});

applyFilter.addEventListener("click", function () {
    const activeButtons = document.querySelectorAll(".filter-btn.filter-active");
    const searchTerms = Array.from(activeButtons).map(button => "search=" + encodeURIComponent(button.value));
    const queryString = searchTerms.join("&");
    window.location.href = "/searchresult?" + queryString;
});

