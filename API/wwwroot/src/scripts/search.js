

let allTags;
let selectedTags = [];
let cardsData = [];
let cardFavoriteButtons = [];
let detailsFavoriteButton;

allTags = ["хуй", "пизда"]
fetch("/tags")
    .then(response => response.json())
    .then(data => allTags = data);

allTags.sort((a, b) => a.localeCompare(b));
const searchInput = document.getElementById("search-input");
const autocompleteList = document.getElementById("autocomplete-list");
const selectedTagList = document.getElementById("selected-tag-list");
const searchResults = document.getElementById("search-results");
const submitButton = document.querySelector("button[type=\"submit\"]");
const detailsPanel = document.getElementById('detailsPanel');
const closeBtn = document.getElementById('closeBtn');
const mainContainer = document.querySelector('.main-container');
const inputGroup = document.querySelector(".input-group");



function addTag(tag){
    if (!selectedTags.includes(tag)){
        selectedTags.push(tag);
        createTag(tag);
    }
    // selectedTagList.innerHTML = '';
    // selectedTags.forEach(tag => {
    //     const itemElement = document.createElement('span');
    //     itemElement.classList.add('selected-tag');
    //     itemElement.textContent = tag;
    //     console.log(selectedTagList);
    //     selectedTagList.appendChild(itemElement);
    // });

}

function updateSearch(searchQuery){
    autocompleteList.innerHTML = '';

    let filteredData;
    // Фильтрация данных
    if (searchQuery.length > 0){

        filteredData = allTags.filter(item =>
            item.toLowerCase().includes(searchQuery)
        );
    }
    else{
        filteredData = allTags;
    }

    // Отображение подсказок
    filteredData.forEach(item => {
        const itemElement = document.createElement('div');
        itemElement.classList.add('autocomplete-item');
        itemElement.textContent = item;

        // Обработчик клика по подсказке
        itemElement.addEventListener('click', function() {
            // searchInput.value = item;
            addTag(item);
            // autocompleteList.innerHTML = '';
        });

        autocompleteList.appendChild(itemElement);
    });
}

function updateFavorite(barId){
    if (localStorage.getItem(`bar_favorite_${barId}`) === 'false') {
        fetch(`api/favoriteBars/${barId}`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({
                barId: barId}),
        })
            .then(response => {
                if (!response.ok) {
                    throw new Error('Favorite not updated');
                }
            });
        localStorage.setItem(`bar_favorite_${barId}`, 'true');
    }
    else if (localStorage.getItem(`bar_favorite_${barId}`) === 'true') {
        fetch(`api/favoriteBars/${barId}`, {
            method: 'DELETE',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({
                barId: barId}),
        })
            .then(response => {
                if (!response.ok) {
                    throw new Error('Favorite not updated');
                }
            });
        localStorage.setItem(`bar_favorite_${barId}`, 'false');
    }
}
function updateDetailFavoriteButton(element) {
    const barId = Number(element.getAttribute('data-id'));
    if (localStorage.getItem(`bar_favorite_${barId}`) === 'false') {
        element.classList.remove('fa-solid');
        element.classList.add('fa-regular');
    }
    else if (localStorage.getItem(`bar_favorite_${barId}`) === 'true') {
        element.classList.remove('fa-regular');
        element.classList.add('fa-solid');
    }
}

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
        // Клик по карточке - показываем детали
        console.log(e);
        detailsPanel.classList.add('active');
        // Здесь можно добавить код для заполнения detailsPanel данными карточки
    }
    // else if (!detailsPanel.contains(e.target)) {
    //     // Клик вне карточки и вне панели деталей - скрываем панель
    //     detailsPanel.classList.remove('active');
    // }
});

submitButton.addEventListener('click', function(event) {
    fetch('/search', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({
            tags: selectedTags}),
    })
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            return response.json();
        })
        .then(data => {
            // 4. Обрабатываем полученные результаты
            cardsData = data;
            displaySearchResults(data); // Ваша функция для отображения результатов
        })
        .catch(error => {
            console.error('Error:', error);
            // Можно показать сообщение об ошибке пользователю
        });

});

closeBtn.addEventListener('click', () => {
    detailsPanel.classList.remove('active');
});

function displaySearchResults(data){
    console.log(data);
    searchResults.innerHTML = '';
    for (const bar of data){
        let tagsElement = "";
        for (let tag of bar['tags']){
            tagsElement += `<span class="tag">${tag['name']}</span>`;
        }
        let favoriteTag = localStorage.getItem(`bar_favorite_${bar['id']}`) === "true" ? "fa-solid" : "fa-regular";
        const card = `
            <div class="venue-card" style="background-color: ${getPastelColor()}" data-id="${bar['id']}">
              <div class="venue-image">
                <img src="${bar['photo']}" alt="Фото заведения">
              </div>
              <div class="venue-info">
                <div class="venue-tags"> ${tagsElement} </div>
                <div class="venue-bottom-info">
                  <div class="name-address">
                    <h4 class="venue-name">${bar['name']}</h4>
                    <div class="venue-address">
                      <svg class="icon" width="16" height="16" viewBox="0 0 24 24" fill="none" stroke="currentColor">
                        <path d="M21 10c0 7-9 13-9 13s-9-6-9-13a9 9 0 0 1 18 0z"></path>
                        <circle cx="12" cy="10" r="3"></circle>
                      </svg>
                      <span>${bar['address']}</span>
                    </div>
                  </div>
                  <i class="${favoriteTag} fa-bookmark favorite-button" data-id="${bar['id']}"></i>
                </div>
              </div>
            </div>`;
        searchResults.insertAdjacentHTML('beforeend', card);
    }
    
    const cards = document.querySelectorAll('.venue-card');
    cards.forEach(card => {
        card.addEventListener('click', function(event) {
            if (event.target.closest('.favorite-button')) {
                return; // Прерываем выполнение, если клик был по кнопке
            }
            const cardId = Number(this.getAttribute('data-id'));
            detailsPanel.classList.add('active');
            const bar = cardsData.find(card => card['id'] === cardId);
            
            if (bar){
                let tagsElement = "";
                for (let tag of bar['tags']){
                    tagsElement += `<span class="tag">${tag['name']}</span>`;
                }
                let favoriteTag = localStorage.getItem(`bar_favorite_${bar['id']}`) === "true" ? "fa-solid" : "fa-regular";
                const info = `
                <img src="${bar['photo']}" alt="Фото заведения" class="description-image">
                <div class="tags-favorite">
                  <div class="details-tags"> ${tagsElement} </div>
                  <i class="${favoriteTag} fa-bookmark favorite-button" data-id="${bar['id']}"></i>
                </div>
                <div class="venue-info">
                  <div class="name-address">
                    <h2 class=" venue-name details-name">${bar['name']}</h2>
                    <div class="venue-address">
                      <svg class="icon" width="16" height="16" viewBox="0 0 24 24" fill="none" stroke="currentColor">
                        <path d="M21 10c0 7-9 13-9 13s-9-6-9-13a9 9 0 0 1 18 0z"></path>
                        <circle cx="12" cy="10" r="3"></circle>
                      </svg>
                      <span>${bar['address']}</span>
                    </div>
                  </div>
                  <div class="work-time">
                    <h4>Время работы</h4>
                    <div><span>${bar['timeOfWork']}</span></div>
                  </div>
                </div>
                `;
                let detailsContent = detailsPanel.querySelector('#detailsContent');
                detailsContent.innerHTML = info;
                detailsPanel.style.backgroundColor = this.style.backgroundColor
                detailsPanel.querySelector('.favorite-button').addEventListener('click', function(event){
                    updateFavorite(Number(this.getAttribute('data-id')));
                    document
                    .querySelectorAll('.favorite-button')
                    .forEach(item => updateDetailFavoriteButton(item));
                    }
                );
            }
        });
    });

    document.querySelectorAll('.favorite-button').forEach(item => {
        item.addEventListener('click', function(event){
            updateFavorite(Number(this.getAttribute('data-id')));
            document
            .querySelectorAll('.favorite-button')
            .forEach(item => updateDetailFavoriteButton(item));
        });
    });
}

function getPastelColor() {
    const hue = Math.floor(Math.random() * 360);
    return `hsl(${hue}, 70%, 85%)`;
}


function createTag(tagText) {
    const tag = document.createElement('div');
    tag.className = 'search-tag';
    tag.innerText = tagText;

    const removeBtn = document.createElement('span');
    removeBtn.className = 'remove-tag';
    removeBtn.innerText = '×';
    removeBtn.onclick = () => {
        tag.remove();
        let index = selectedTags.indexOf(tagText);
        if (index !== -1) {
            selectedTags.splice(index, 1);
        }
    };

    tag.appendChild(removeBtn);
    inputGroup.insertBefore(tag, searchInput);
}




