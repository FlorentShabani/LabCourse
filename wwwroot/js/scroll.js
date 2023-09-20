document.addEventListener("DOMContentLoaded", function () {
    const scrollButton1 = document.getElementById("scrollToR");
    const scrollToSection1 = document.getElementById("scrollToRev");

    const scrollButton2 = document.getElementById("scrollToD");
    const scrollToSection2 = document.getElementById("scrollToDesc");

    scrollButton1.addEventListener("click", function () {
        scrollToSection1.scrollIntoView({ behavior: "smooth" });
    });
    scrollButton2.addEventListener("click", function () {
        scrollToSection2.scrollIntoView({ behavior: "smooth" });
    });
});