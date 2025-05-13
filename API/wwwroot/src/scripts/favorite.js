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

const id = setInterval(() =>
{
    if (favoriteBars !== undefined) {
        cardsData = favoriteBars
        displaySearchResults(favoriteBars);
        clearInterval(id);
    }
}, 100);

