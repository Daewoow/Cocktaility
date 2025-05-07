async function checkAuthAndRedirect() {
    try {
        const response = await fetch('/api/auth/check-auth', {
            credentials: 'include'
        });
        const data = await response.json();
        if (!data.isAuthenticated) {
            window.location.href = '/search';
        }
    } catch (error) {
        window.location.href = '/search';
    }
}

checkAuthAndRedirect();

function displaySearchResults2(data){
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
        const bibika = localStorage.getItem(`bar_favorite_${bar['id']}`);
        if (bibika === null) {
            localStorage.setItem(`bar_favorite_${bar['id']}`, `false`);
        }
    }

    const cards = document.querySelectorAll('.venue-card');
    cards.forEach(card => {
        card.addEventListener('click', function(event) {
            if (event.target.closest('.favorite-button')) {
                return; // Прерываем выполнение, если клик был по кнопке
            }
            const cardId = Number(this.getAttribute('data-id'));
            detailsPanel.classList.add('active');
            const bar = favoriteBars.find(card => card['id'] === cardId);

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
                  <div class="menu">
                    <a href="${bar['menu']}" target="_blank">Меню</a>
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
const id = setInterval(() =>
{
    if (favoriteBars !== undefined) {
        displaySearchResults2(favoriteBars);
        clearInterval(id);
    }
}, 100);

