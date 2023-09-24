document.addEventListener("DOMContentLoaded", function () {
    const scrollButton1 = document.getElementById("scrollToR");
    const scrollToSection1 = document.getElementById("scrollToRev");

    const scrollButton2 = document.getElementById("scrollToD");
    const scrollToSection2 = document.getElementById("scrollToDesc");

    const navbarHeight = 200; // Adjust this value according to your actual navbar height

    scrollButton1.addEventListener("click", function () {
        const targetPosition = scrollToSection1.getBoundingClientRect().top + window.scrollY;
        window.scrollTo({ top: targetPosition - navbarHeight, behavior: "smooth" });
    });

    scrollButton2.addEventListener("click", function () {
        const targetPosition = scrollToSection2.getBoundingClientRect().top + window.scrollY;
        window.scrollTo({ top: targetPosition - navbarHeight, behavior: "smooth" });
    });
});