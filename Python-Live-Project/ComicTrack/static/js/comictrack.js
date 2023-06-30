//Displays the first image in the slideshow on page load
var slideIndex = 1;
showSlides(slideIndex);

//this function changes the slide whe the left of right arrows are clicked
function plusSlides(n) {
    showSlides(slideIndex += n);
}

//This function changes the slide when the dots are clicked
function currentSlide(n) {
    showSlides(slideIndex = n)
}

//the function to show a slide
function showSlides(n) {
    var slides = document.getElementsByClassName("mySlides"); //Get all the mySlides elements in a nice array
    var dots = document.getElementsByClassName("dot"); // Get all the dot elements
    if (n > slides.length) { slideIndex = 1 }; //if n greater than length of slides, slideIndex becomes 1
    if (n < 1) { slideIndex = slides.length }; //if slideIndex goes under 1 set it to slides length
    for (let i = 0; i < slides.length; i++) {
        slides[i].style.display = "none"; //hide each of the slides
    }
    for (i = 0; i < dots.length; i++) {
        dots[i].className = dots[i].className.replace(" active", ""); //mark the active dot inactive
    }
    slides[slideIndex - 1].style.display = "block"; //make the slide appear
    dots[slideIndex - 1].className += " active"; //mark the dot as active
}
