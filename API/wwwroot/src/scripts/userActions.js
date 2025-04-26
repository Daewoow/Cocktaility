const actionContainer = document.querySelector('.actions');

const addTagButton = document.querySelector('#addTagButton');
const addBarButton = document.querySelector('#addBarButton');
const sendTagButton = document.querySelector('#sendTagButton');
const sendBarButton = document.querySelector('#sendBarButton');

const actionAddTagInputGroup = document.querySelector('#actionAddTagInputGroup');
const actionAddBarInputGroup = document.querySelector('#actionAddBarInputGroup');

const tagInput = document.querySelector('#tagInput');
const barInput = document.querySelector('#barInput');

function toggleBarFrom(flag){
    if (flag){
        actionAddBarInputGroup.style.display = 'flex';
        sendBarButton.style.display = 'flex';
        addBarButton.style.display = 'none';
    }
    else{
        actionAddBarInputGroup.style.display = 'none';
        sendBarButton.style.display = 'none';
        addBarButton.style.display = 'flex';
    }
}

function toggleTagFrom(flag){
    if (flag) {
        actionAddTagInputGroup.style.display = 'flex';
        sendTagButton.style.display = 'flex';
        addTagButton.style.display = 'none';
    }
    else{
        actionAddTagInputGroup.style.display = 'none';
        sendTagButton.style.display = 'none';
        addTagButton.style.display = 'flex';
    }
}
addTagButton.addEventListener('click', (e) => {
    e.stopPropagation();
    toggleTagFrom(true);
    toggleBarFrom(false);
});

addBarButton.addEventListener('click', (e) => {
    e.stopPropagation();

    toggleBarFrom(true);
    toggleTagFrom(false);
    
});

sendTagButton.addEventListener('click', (e) => {
    const tag = tagInput.value;
    fetch(`tags/newTag`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(tag),
    })
    .then(response => {
        if (!response.ok) {
            throw new Error('Tag not added');
        }
    });
});

sendBarButton.addEventListener('click', (e) => {
    const barData = {
        name: document.getElementById('barName').value,
        address: document.getElementById('barAddress').value,
        photo: document.getElementById('barPhoto').value,
        menu: document.getElementById('barMenu').value,
        site: document.getElementById('barSite').value,
        rating: parseInt(document.getElementById('barRating').value),
        timeOfWork: document.getElementById('barTimeOfWork').value,
        tags: document.getElementById('barTags').value.split(',').map(tag => tag.trim())
    };

    fetch(`bars/newBar`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(barData    ),
    })
        .then(response => {
            if (!response.ok) {
                throw new Error('Bar not added');
            }
            // Очистка формы после успешной отправки
            document.getElementById('actionAddBarInputGroup').style.display = 'none';
            sendBarButton.style.display = 'none';
            addBarButton.style.display = 'flex';
            // Можно добавить уведомление об успешном добавлении
        })
        .catch(error => {
            console.error('Error:', error);
            // Можно добавить уведомление об ошибке
        });
});

// tag
document.addEventListener('click', (e) => {
    if (!actionAddTagInputGroup.contains(e.target) && actionAddTagInputGroup.style.display !== 'none') {
        toggleTagFrom(false);
    }
   
});

//bar
document.addEventListener('click', (e) => {
    if (!actionAddBarInputGroup.contains(e.target) && actionAddBarInputGroup.style.display !== 'none') {
        toggleBarFrom(false);
    }
});