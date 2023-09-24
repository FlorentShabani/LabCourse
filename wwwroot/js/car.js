let currentActive = 1;

const incrementActive = () => {
    if (currentActive < 4) {
        currentActive++;
    } else {
        currentActive = 1;
    }
    showActiveSlide();
}

const showActiveSlide = () => {
    const slides = document.querySelectorAll('.slide');

    slides.forEach((slide, idx) => {
        if (idx + 1 === currentActive) {
            slide.classList.add('active');
        } else {
            slide.classList.remove('active');
        }
    });
}

const setAutoSlide = () => {
    return setInterval(() => {
        incrementActive();
        showActiveSlide();
    }, 5000)
}

let interval = setAutoSlide();
const buttons = document.querySelectorAll('.button');

buttons.forEach((button, idx) => {
    button.addEventListener('click', () => {
        clearInterval(interval);
        // clearInterval

        currentActive = idx + 1;
        showActiveSlide();

        // startInterval
        interval = setAutoSlide()
    });
});

        // const button1 = document.getElementById('button-1');

        // button1.addEventListener('click', () => {
        //     currentActive = 1;
        //     showActiveSlide();
        // });