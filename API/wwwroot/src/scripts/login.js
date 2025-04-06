const loginUrl = "login";
console.log("Check");

document.getElementById('loginForm').addEventListener('submit', function(event) {
    event.preventDefault();

    const email = document.getElementById('username').value;
    const password = document.getElementById('password').value;
    const errorMessage = document.getElementById('error-message');

    if (!username || !password) {
        errorMessage.textContent = 'Пожалуйста, заполните все поля.';
        errorMessage.style.display = 'block';
        return;
    }

    fetch(loginUrl, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({ email: email, password: password }),
    })
        .then(response => response.json())
        .then(data => {
            if (data.success) {
                console.log('succsess: ', data.success);
                window.location.href = "/search";
            } else {
                errorMessage.textContent = data.message || 'Ошибка авторизации';
                errorMessage.style.display = 'block';
            }
        })
        .catch(error => {
            console.error('Ошибка:', error);
            errorMessage.textContent = 'Произошла ошибка при отправке данных.';
            errorMessage.style.display = 'block';
        });
});