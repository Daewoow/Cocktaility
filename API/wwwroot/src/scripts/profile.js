const isMobile = /Android|webOS|iPhone|Mac|iPad|iPod|BlackBerry|IEMobile|Chrome|Opera Mini/i.test(navigator.userAgent);
alert(navigator.userAgent);

if (isMobile) {
    const trigger = document.querySelector('.profile-trigger');
    const mobileMenu = document.querySelector('.mobile-version');

    trigger.addEventListener('click', (e) => {
        e.stopPropagation();
        mobileMenu.classList.toggle('show');
        console.log(mobileMenu.classList);
        console.log(mobileMenu);
    });

    document.addEventListener('click', (e) => {
        e.stopPropagation();
        mobileMenu.classList.remove('show');
    });
}