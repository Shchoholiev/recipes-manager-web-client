const mockdata = [
    {
        avatar:
            "https://images.pexels.com/photos/3814446/pexels-photo-3814446.jpeg?cs=srgb&dl=pexels-miquel-ferran-gomez-figueroa-3814446.jpg&fm=jpg",
        public: [
            {
                id: 1,
                photo:
                    "https://d104wv11b7o3gc.cloudfront.net/wp-content/uploads/2019/06/keto-ribs-10.jpg",
                title: "Traditional spare ribs baked",
                by: "Chef John",
                saved: false,
                time: 20,
            },
            {
                id: 2,
                photo:
                    "https://www.cookingclassy.com/wp-content/uploads/2020/01/chicken-and-rice-15-500x500.jpg",
                title: "Spice roasted chicken with flavored rice",
                by: "Mark Kelvin",
                saved: false,
                time: 20,
            },
            {
                id: 3,
                photo:
                    "https://www.food4life.org.uk/multisite/wp-content/uploads/sites/2/2022/06/Image-11-Grilled-lamb-chops-with-mint-and-fruity-couscous.jpg",
                title: "Lamb chops with fruity couscous and mint",
                by: " Spicy Nelly",
                saved: true,
                time: 20,
            },
            {
                id: 4,
                photo:
                    "https://www.kitchensanctuary.com/wp-content/uploads/2020/07/Nasi-Goreng-square-FS-57-500x375.jpg",
                title: "Spicy fried rice mix chicken bali",
                by: "laura wilson",
                saved: false,
                time: 20,
            },
            {
                id: 5,
                photo:
                    "https://d104wv11b7o3gc.cloudfront.net/wp-content/uploads/2019/06/keto-ribs-10.jpg",
                title: "Traditional spare ribs baked",
                by: "Chef John",
                saved: false,
                time: 20,
            },
            {
                id: 6,
                photo:
                    "https://www.cookingclassy.com/wp-content/uploads/2020/01/chicken-and-rice-15-500x500.jpg",
                title: "Spice roasted chicken with flavored rice",
                by: "Mark Kelvin",
                saved: false,
                time: 20,
            },
            {
                id: 7,
                photo:
                    "https://www.food4life.org.uk/multisite/wp-content/uploads/sites/2/2022/06/Image-11-Grilled-lamb-chops-with-mint-and-fruity-couscous.jpg",
                title: "Lamb chops with fruity couscous and mint",
                by: " Spicy Nelly",
                saved: false,
                time: 20,
            },
            {
                id: 8,
                photo:
                    "https://www.kitchensanctuary.com/wp-content/uploads/2020/07/Nasi-Goreng-square-FS-57-500x375.jpg",
                title: "Spicy fried rice mix chicken bali",
                by: "laura wilson",
                saved: false,
                time: 20,
            },
            {
                id: 9,
                photo:
                    "https://d104wv11b7o3gc.cloudfront.net/wp-content/uploads/2019/06/keto-ribs-10.jpg",
                title: "Traditional spare ribs baked",
                by: "Chef John",
                saved: true,
                time: 20,
            },
            {
                id: 10,
                photo:
                    "https://www.cookingclassy.com/wp-content/uploads/2020/01/chicken-and-rice-15-500x500.jpg",
                title: "Spice roasted chicken with flavored rice",
                by: "Mark Kelvin",
                saved: false,
                time: 20,
            },
            {
                id: 11,
                photo:
                    "https://www.food4life.org.uk/multisite/wp-content/uploads/sites/2/2022/06/Image-11-Grilled-lamb-chops-with-mint-and-fruity-couscous.jpg",
                title: "Lamb chops with fruity couscous and mint",
                by: " Spicy Nelly",
                saved: false,
                time: 20,
            },
        ],
        saved: [
            {
                id: 1,
                photo: "https://media.istockphoto.com/id/638373648/photo/chinese-style-egg-fried-rice-with-sliced-pork-fillet-on.jpg?s=612x612&w=0&k=20&c=Vl8ojkzyp7hYRFtSDJ0swCmQr7CUh14sEDh6tq2ufC4=",
                title: "Chinese style Egg fried rice with sliced pork",
                by: "Chef John",
                saved: true,
                time: 30,
            },
            {
                id: 2,
                photo: "https://www.kikkoman.eu/fileadmin/_processed_/0/0/csm_WEB_Traditional_Fukuoka_Ramen_646cd39e6b.jpg",
                title: "Vegitarian ramen with spices",
                by: "Mark Kelvin",
                saved: true,
                time: 25,
            },
            {
                id: 3,
                photo:
                    "https://www.food4life.org.uk/multisite/wp-content/uploads/sites/2/2022/06/Image-11-Grilled-lamb-chops-with-mint-and-fruity-couscous.jpg",
                title: "Lamb chops with fruity couscous and mint",
                by: " Spicy Nelly",
                saved: true,
                time: 20,
            },
            {
                id: 4,
                photo:
                    "https://www.kitchensanctuary.com/wp-content/uploads/2020/07/Nasi-Goreng-square-FS-57-500x375.jpg",
                title: "Spicy fried rice mix chicken bali",
                by: "laura wilson",
                saved: true,
                time: 20,
            },
            {
                id: 5,
                photo:
                    "https://d104wv11b7o3gc.cloudfront.net/wp-content/uploads/2019/06/keto-ribs-10.jpg",
                title: "Traditional spare ribs baked",
                by: "Chef John",
                saved: true,
                time: 20,
            },
            {
                id: 6,
                photo:
                    "https://www.cookingclassy.com/wp-content/uploads/2020/01/chicken-and-rice-15-500x500.jpg",
                title: "Spice roasted chicken with flavored rice",
                by: "Mark Kelvin",
                saved: true,
                time: 20,
            },
            {
                id: 7,
                photo:
                    "https://www.food4life.org.uk/multisite/wp-content/uploads/sites/2/2022/06/Image-11-Grilled-lamb-chops-with-mint-and-fruity-couscous.jpg",
                title: "Lamb chops with fruity couscous and mint",
                by: " Spicy Nelly",
                saved: true,
                time: 20,
            },
        ],
        personal: [
            {
                id: 1,
                photo:
                    "https://d104wv11b7o3gc.cloudfront.net/wp-content/uploads/2019/06/keto-ribs-10.jpg",
                title: "Traditional spare ribs baked",
                by: "Chef John",
                saved: false,
                time: 20,
            },
            {
                id: 2,
                photo:
                    "https://www.cookingclassy.com/wp-content/uploads/2020/01/chicken-and-rice-15-500x500.jpg",
                title: "Spice roasted chicken with flavored rice",
                by: "Mark Kelvin",
                saved: false,
                time: 20,
            },
            {
                id: 3,
                photo:
                    "https://www.food4life.org.uk/multisite/wp-content/uploads/sites/2/2022/06/Image-11-Grilled-lamb-chops-with-mint-and-fruity-couscous.jpg",
                title: "Lamb chops with fruity couscous and mint",
                by: " Spicy Nelly",
                saved: false,
                time: 20,
            }
        ],
    },
];


const avatarElement = document.querySelector(".avatar");
const sidebarButtons = document.querySelectorAll(".sidebar-btn");
const dishes = document.querySelector(".dishes");
const saved = document.querySelectorAll(".dish-saved");

function activateSidebarButton(event) {
    const clickedButton = event.target;
    const sidebarButtons = document.querySelectorAll(".sidebar-btn");



    if (clickedButton.innerHTML === "Saved") {
        getDishes("saved")
    }
    else if (clickedButton.innerHTML === "Public") {
        getDishes("public")
    }
    else {
        getDishes("personal")
    }

    sidebarButtons.forEach((button) => {
        button.classList.remove("sidebar-active");
    });

    clickedButton.classList.add("sidebar-active");
}

sidebarButtons.forEach((button) => {
    button.addEventListener("click", activateSidebarButton);
});


const avatarImageUrl = mockdata[0].avatar;
avatarElement.style.backgroundImage = `url(${avatarImageUrl})`;

function toggleSaved(id) {
    const item = mockdata[0].public.find((item) => item.id === id);
    item.saved = !item.saved;
    const img = document.querySelector(`[data-id="${id}"]`);
    img.src = item.saved ? "assets/saved.svg" : "assets/unsaved.svg";
}

function getDishes(type) {
    dishes.innerHTML = '';
    for (let i = 0; i < mockdata[0][type].length; i++) {
        let dishItem = document.createElement("div");
        dishItem.classList.add("dish-item");
        dishItem.style.background = `linear-gradient(180deg, rgba(0, 0, 0, 0) 0%, #000000 100%), url(${mockdata[0][type][i].photo})`;
        dishItem.style.backgroundSize = `cover`;
        dishItem.style.backgroundPosition = `center`;
        let title = document.createElement("div");
        title.classList.add("dish-title");
        title.innerHTML = ``;

        dishes.append(dishItem);

        dishItem.innerHTML = `
    <div class="dish-title">${mockdata[0][type][i].title}</div>
    <div class="dish-author">By ${mockdata[0][type][i].by}</div>
    <div class="timer"><img src ="assets/timer.svg"></div>
    <div class="dish-time">${mockdata[0][type][i].time} min</div>
    <div class="dish-saved"><img src = ${mockdata[0][type][i].saved === true
                ? "assets/saved.svg"
                : "assets/unsaved.svg"
            } onclick="toggleSaved(${mockdata[0][type][i].id})" data-id="${mockdata[0][type][i].id
            }"></div>
    `;
    }
}




