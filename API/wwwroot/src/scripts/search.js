let allTags;
let favoriteBars;
let selectedTags = [];
let cardsData = [];

allTags = ["tag_example"];



// checkAuth().then(isAuth => {
//     if (!isAuth) {
//         const btn = document.querySelector('.btn');
//         if (btn) btn.remove();
//     }
// });

fetch("/tags")
    .then(response => response.json())
    .then(data => allTags = data)
    .then(data => {
        allTags.sort((a, b) => a.localeCompare(b));
    });

fetch(`/api/favoriteBars/getFavoriteBars`)
    .then(response => {
        if (!response.ok) {
            let favoriteBarsIdsLocal = [];
            for (let i = 0; i < localStorage.length; i++) {
                const key = localStorage.key(i);
                const isFav = localStorage.getItem(key);
                if (isFav === 'true'){
                    let lalala = key.split('_');
                    favoriteBarsIdsLocal.push(Number(lalala[lalala.length - 1]));
                }
            }
            return fetch(`/getBarsByIds`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(favoriteBarsIdsLocal
                ),
            })
                .then(response => response.json());
        }
        return response.json();
    })
    .then(data => favoriteBars = data);

const searchInput = document.getElementById("search-input");
const autocompleteList = document.getElementById("autocomplete-list");
const selectedTagList = document.getElementById("selected-tag-list");
const searchResults = document.getElementById("search-results");
const submitButton = document.querySelector("button[type=\"submit\"]");
const detailsPanel = document.getElementById('detailsPanel');
const closeBtn = document.getElementById('closeBtn');
const mainContainer = document.querySelector('.main-container');
const inputGroup = document.querySelector(".input-group");
// const favoriteFilterButton = document.getElementById("favorite-filter-button");
// favoriteFilterButton.active = false;



// function toggleFavoriteFilter() {
//     favoriteFilterButton.active = !favoriteFilterButton.active;
//     if (favoriteFilterButton.active) {
//         favoriteFilterButton.classList.remove("fa-regular");
//         favoriteFilterButton.classList.add("fa-solid");
//         document.querySelectorAll('.venue-card').forEach((element) => {
//             const barId = Number(element.getAttribute('data-id'));
//             element.hidden = favoriteBars.includes(barId);
//         });
//     }
//     else{
//         favoriteFilterButton.classList.remove("fa-solid");
//         favoriteFilterButton.classList.add("fa-regular");
//         document.querySelectorAll('.venue-card').forEach((element) => {
//             element.hidden = false;
//         });
//     }
//     document.querySelectorAll('.venue-card').forEach((element) => {
//         const barId = Number(element.getAttribute('data-id'));
//         if (!favoriteFilterButton.active){
//             element.hidden = false;
//         }
//         else if (favoriteFilterButton.active) {
//             element.hidden = favoriteBars.find(x => x.id === barId) === undefined;
//         }
//     });
// }
//    Закомментил, причина в search.html


// favoriteFilterButton.addEventListener("click", toggleFavoriteFilter);
searchInput.addEventListener("input", function(event) {
    const inputText = this.value.toLowerCase();
    updateSearch(inputText);

});

searchInput.addEventListener("focus", function(event) {
    const inputText = this.value.toLowerCase();
    updateSearch(inputText);
});

document.addEventListener('click', function(e) {
    if (e.target.id !== 'search-input' && !e.target.classList.contains('autocomplete-item')) {
        autocompleteList.innerHTML = '';
    }
    const cardElement = e.target.closest('.venue-card');

    if (cardElement && !e.target.classList.contains('favorite-button')) {
        console.log(e);
        detailsPanel.classList.add('active');
    }
    // else if (!detailsPanel.contains(e.target)) {
    //     // Клик вне карточки и вне панели деталей - скрываем панель
    //     detailsPanel.classList.remove('active');
    // }
});

submitButton.addEventListener('click', function(event) {
    // let barsByTag = [];
    // let barsByFavorite = [];
    if (selectedTags.length > 0) {
        fetch('/search', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({
                tags: selectedTags
            }),
        })
            .then(response => {
                if (!response.ok) {
                    throw new Error('Network response was not ok');
                }
                return response.json();
            })
            .then(data => {
                cardsData = data;
                displaySearchResults(data);
            })
            .catch(error => {
                console.log('Error:', error);
            });
    }
    // else if (favoriteBars !== null && favoriteFilterButton.active) {
    //     displaySearchResults(favoriteBars);
    // }                                                               Закомментил, причина в search.html    
});

closeBtn.addEventListener('click', () => {
    detailsPanel.classList.remove('active');
});
