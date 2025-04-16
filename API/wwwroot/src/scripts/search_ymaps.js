ymaps.ready(init);

function init(){
    const myMap = new ymaps.Map("map", {
        center: [55.76, 37.64], // Координаты центра карты (Москва)
        zoom: 10
    });

    const myPlacemark = new ymaps.Placemark([55.76, 37.64], {
        balloonContent: 'Это Москва!'
    });

    myMap.geoObjects.add(myPlacemark);
}
