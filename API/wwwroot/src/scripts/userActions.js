const actionContainer = document.querySelector('.actions');

const addTagButton = document.querySelector('#addTagButton');
const addBarButton = document.querySelector('#addBarButton');
const sendTagButton = document.querySelector('#sendTagButton');
const sendBarButton = document.querySelector('#sendBarButton');

const actionAddTagInputGroup = document.querySelector('#actionAddTagInputGroup');
const actionAddBarInputGroup = document.querySelector('#actionAddBarInputGroup');

const tagInput = document.querySelector('#tagInput');
const barInput = document.querySelector('#barInput');


addTagButton.addEventListener('click', (e) => {
    e.stopPropagation();
    actionAddTagInputGroup.style.display = 'flex';
    sendTagButton.style.display = 'flex';
    addTagButton.style.display = 'none';
});

addBarButton.addEventListener('click', (e) => {
    e.stopPropagation();
    
    actionAddBarInputGroup.style.display = 'flex';
    sendBarButton.style.display = 'flex';
    addBarButton.style.display = 'none';
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
    
});

// tag
document.addEventListener('click', (e) => {
    if (!actionContainer.contains(e.target) && actionAddTagInputGroup.style.display !== 'none') {
        actionAddTagInputGroup.style.display = 'none';
        actionAddTagInputGroup.style.display = 'none';
        sendTagButton.style.display = 'none';
        addTagButton.style.display = 'flex';
    }
   
});

//bar
document.addEventListener('click', (e) => {
    if (!actionAddBarInputGroup.contains(e.target) && actionAddBarInputGroup.style.display !== 'none') {
        actionAddBarInputGroup.style.display = 'none';
        actionAddBarInputGroup.style.display = 'none';
        sendBarButton.style.display = 'none';
        addBarButton.style.display = 'flex';
    }
});