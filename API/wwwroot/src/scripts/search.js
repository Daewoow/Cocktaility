

let allTags;
let selectedTags = [];
let cardsData = [];

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
searchInput.addEventListener("input", function(event) {
    const inputText = this.value.toLowerCase();
    autocompleteList.innerHTML = '';

    if (!inputText) {
        return;
    }

    // Фильтрация данных
    const filteredData = allTags.filter(item =>
        item.toLowerCase().includes(inputText)
    );

    // Отображение подсказок
    filteredData.forEach(item => {
        const itemElement = document.createElement('div');
        itemElement.classList.add('autocomplete-item');
        itemElement.textContent = item;

        // Обработчик клика по подсказке
        itemElement.addEventListener('click', function() {
            // searchInput.value = item;
            addTag(item);
            autocompleteList.innerHTML = '';
        });

        autocompleteList.appendChild(itemElement);
    });
});

document.addEventListener('click', function(e) {
    if (e.target.id !== 'search-input') {
        autocompleteList.innerHTML = '';
    }
    const cardElement = e.target.closest('.venue-card');

    if (cardElement) {
        // Клик по карточке - показываем детали
        detailsPanel.classList.add('active');
        // Здесь можно добавить код для заполнения detailsPanel данными карточки
    } else if (!detailsPanel.contains(e.target)) {
        // Клик вне карточки и вне панели деталей - скрываем панель
        detailsPanel.classList.remove('active');
    }
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
            tagsElement += `<span class="tag">#${tag['name']}</span>`;
        }
        let card = `
            <div class="venue-card" style="background-color: ${getPastelColor()}" data-id="${bar['id']}">
              <div class="venue-image">
                <img src="${bar['photo']}" alt="Фото заведения">
              </div>

              <div class="venue-info">
                <div class="venue-tags">
                  ${tagsElement}
                </div>
                <h4 class="venue-name">${bar['name']}</h4>

                <div class="venue-address">
                  <svg class="icon" width="16" height="16" viewBox="0 0 24 24" fill="none" stroke="currentColor">
                    <path d="M21 10c0 7-9 13-9 13s-9-6-9-13a9 9 0 0 1 18 0z"></path>
                    <circle cx="12" cy="10" r="3"></circle>
                  </svg>
                  <span>${bar['address']}</span>
                </div>
              </div>
            </div>`;
        searchResults.insertAdjacentHTML('beforeend', card);
    }

    const cards = document.querySelectorAll('.venue-card');
    cards.forEach(card => {
        card.addEventListener('click', function() {
            const cardId = Number(this.getAttribute('data-id'));
            detailsPanel.classList.add('active');
            const bar = cardsData.find(card => card['id'] === cardId);
            if (bar){
                detailsPanel.querySelector('#detailsContent').innerHTML = `
                <h2>${bar['name']}</h2>`;
            }
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




