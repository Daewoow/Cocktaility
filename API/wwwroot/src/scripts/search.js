let allTags;
let favoriteBars;
let selectedTags = [];
let cardsData = [];

allTags = ["tag_example"];

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
    .then(data => favoriteBars = data)
    .catch(error => error.message);

const searchInput = document.getElementById("search-input");
const autocompleteList = document.getElementById("autocomplete-list");
const selectedTagList = document.getElementById("selected-tag-list");
const searchResults = document.getElementById("search-results");
const submitButton = document.querySelector("button[type=\"submit\"]");
const detailsPanel = document.getElementById('detailsPanel');
const closeBtn = document.getElementById('closeBtn');
const mainContainer = document.querySelector('.main-container');
const inputGroup = document.querySelector(".input-group");


closeBtn.addEventListener('click', () => {
    detailsPanel.classList.remove('active');
});

document.addEventListener('keydown', (event) => {
    if (event.key === 'Escape') {
        detailsPanel.classList.remove('active');
    }
});

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
});
