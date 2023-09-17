function createSlider(containerId, sliderId) {
    var container = document.getElementById(containerId);
    var slider = document.getElementById(sliderId);
    var slides = container.getElementsByClassName('slide').length;
    var buttons = container.getElementsByClassName('btn');

    var currentPosition = 0;
    var currentMargin = 0;
    var slidesPerPage = 0;
    var slidesCount = slides - slidesPerPage;
    var containerWidth = container.offsetWidth;
    var prevKeyActive = false;
    var nextKeyActive = true;

    window.addEventListener("resize", checkWidth);

    function checkWidth() {
        containerWidth = container.offsetWidth;
        setParams(containerWidth);
    }

    function setParams(w) {
        if (w < 551) {
            slidesPerPage = 1;
        } else {
            if (w < 901) {
                slidesPerPage = 2;
            } else {
                if (w < 1101) {
                    slidesPerPage = 3;
                } else {
                    slidesPerPage = 4;
                }
            }
        }
        slidesCount = slides - slidesPerPage;
        if (currentPosition > slidesCount) {
            currentPosition -= slidesPerPage;
        }
        currentMargin = -currentPosition * (100 / slidesPerPage);
        slider.style.marginLeft = currentMargin + '%';
        if (currentPosition > 0) {
            buttons[0].classList.remove('inactive');
        }
        if (currentPosition < slidesCount) {
            buttons[1].classList.remove('inactive');
        }
        if (currentPosition >= slidesCount) {
            buttons[1].classList.add('inactive');
        }
    }

    setParams();

    function slideRight() {
        if (currentPosition != 0) {
            slider.style.marginLeft = currentMargin + (100 / slidesPerPage) + '%';
            currentMargin += (100 / slidesPerPage);
            currentPosition--;
        }
        if (currentPosition === 0) {
            buttons[0].classList.add('inactive');
        }
        if (currentPosition < slidesCount) {
            buttons[1].classList.remove('inactive');
        }
    }

    function slideLeft() {
        if (currentPosition != slidesCount) {
            slider.style.marginLeft = currentMargin - (100 / slidesPerPage) + '%';
            currentMargin -= (100 / slidesPerPage);
            currentPosition++;
        }
        if (currentPosition == slidesCount) {
            buttons[1].classList.add('inactive');
        }
        if (currentPosition > 0) {
            buttons[0].classList.remove('inactive');
        }
    }

    buttons[0].onclick = slideRight;
    buttons[1].onclick = slideLeft;

    return {
        slideRight: slideRight,
        slideLeft: slideLeft
    };
}

var slider1 = createSlider('container1', 'slider1');
var slider2 = createSlider('container2', 'slider2');
var slider3 = createSlider('container3', 'slider3');