let myMap = null;
let myPlacemark = null;

document.querySelector('.main-container').addEventListener('click', function(event) {
    const card = event.target.closest('.venue-card');

    if (card) {
        const venueId = card.getAttribute('data-id');
        loadBarAndShowOnMap(venueId);
    }
});

function showAddressOnMap(address) {
    ymaps.geocode(address, { results: 1 }).then(function (res) {
        const firstGeoObject = res.geoObjects.get(0);
        const coordinates = firstGeoObject.geometry.getCoordinates();

        if (!myMap) {
            myMap = new ymaps.Map("map", {
                center: coordinates,
                zoom: 15
            });
        }
        if (myPlacemark) {
            myMap.geoObjects.remove(myPlacemark);
        }
        
        myPlacemark = new ymaps.Placemark(coordinates, {
            balloonContent: firstGeoObject.getAddressLine()
        }, {
            preset: 'islands#redDotIcon'
        });

        myMap.geoObjects.add(myPlacemark);
        myMap.setCenter(coordinates);

    }).catch(function (error) {
        console.error('Ошибка:', error);
        // alert('Адрес не найден');
    });
}

async function loadBarAndShowOnMap(barId) {
    try {
        const response = await fetch(`/bars/${barId}`);
        const bar = await response.json();
        showAddressOnMap(bar.address + ', Екатеринбург');
    } catch (error) {
        console.error('Ошибка загрузки данных:', error);
        // alert('Не удалось загрузить данные о баре');
    }
}