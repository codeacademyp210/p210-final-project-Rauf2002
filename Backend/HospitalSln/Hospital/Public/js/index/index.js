"use strict";


// Slider Starts
const sContainer = document.querySelector(".slider-container");
const sItems = Array.from(sContainer.children);
const prevButton = document.querySelector(".slider-button-left");
const nextButton = document.querySelector(".slider-button-right");
let itemSize = sItems[0].getBoundingClientRect();
console.log(itemSize);
const setSliderPosition = (item, index) => {
    item.style.left = index * itemSize.width + "px";
};

sItems.forEach(setSliderPosition);

// Click prev button, slider move to the left
prevButton.addEventListener("click", function (e) {
    let activeItem = sContainer.querySelector(".active-slide");
    if (activeItem.previousElementSibling) {
        let prevItem = activeItem.previousElementSibling;
        prevItem.classList.add("active-slide");
        activeItem.classList.remove("active-slide");
        sContainer.style.transform = "translateX(-" + prevItem.style.left + ")";
    }
    if (activeItem.previousElementSibling == null) {
        let prevItem = activeItem.parentNode.lastElementChild;
        prevItem.classList.add("active-slide");
        activeItem.classList.remove("active-slide");
        sContainer.style.transform = "translateX(-" + prevItem.style.left + ")";
    }
});
// Click next button, slider move to the right
nextButton.addEventListener("click", function (e) {
    let activeItem = sContainer.querySelector(".active-slide");
    console.log(activeItem);
    if (activeItem.nextElementSibling) {
        let nextItem = activeItem.nextElementSibling;
        nextItem.classList.add("active-slide");
        activeItem.classList.remove("active-slide");
        sContainer.style.transform = "translateX(-" + nextItem.style.left + ")";
    }
    if (activeItem.nextElementSibling == null) {
        let nextItem = activeItem.parentElement.firstElementChild;
        nextItem.classList.add("active-slide");
        activeItem.classList.remove("active-slide");
        sContainer.style.transform = "translateX(-" + nextItem.style.left + ")";
    }
});

// Slider Ends


//Next-Prev Starts 

let prevBtn = document.querySelector(".previous")
let nextBtn = document.querySelector(".next");
let buttonDiv = document.querySelector(".buttonDiv");
nextBtn.addEventListener("click", nextSlide);
prevBtn.addEventListener("click", prevSlide);



function nextSlide() {
    document.querySelector(".sliderItem").style.left = -1400 + "px";
    document.querySelector(".sliderItem2").style.left = 240 + "px";

    //Changing icons
    document.querySelector(".nextI").classList.remove("far");
    document.querySelector(".nextI").classList.add("fas");
    document.querySelector(".nextI").style.color = "#ebebeb";
    document.querySelector(".prevI").classList.remove("fas");
    document.querySelector(".prevI").classList.add("far");
    document.querySelector(".prevI").style.color = "#ebebeb";
}

function prevSlide() {
    document.querySelector(".sliderItem").style.left = 240 + "px";
    document.querySelector(".sliderItem2").style.left = 1400 + "px";

    //Changing icons
    document.querySelector(".nextI").classList.remove("fas");
    document.querySelector(".nextI").classList.add("far");
    document.querySelector(".nextI").style.color = "#ebebeb";
    document.querySelector(".prevI").classList.remove("far");
    document.querySelector(".prevI").style.color = "#ebebeb";
    document.querySelector(".prevI").classList.add("fas");
}

// Next-Prev Ends


// Form Validation Starts

let nameInput = document.querySelector('#name');
let phoneInput = document.querySelector('#phone');
let departmentInput = document.querySelector('#department');
let doctorInput = document.querySelector('#doctor');
let dateInput = document.querySelector('#date');
let submitButton = document.querySelector('.submit');

let namePattern = /^([a-zA-Z]|\s)*$/;
let phonePattern = /^([+])([0-9])/;

function validateForm() {
    if (nameInput.value.trim() == "" || phoneInput.value.trim() == "" || dateInput.value == "") {
        alert("Inputs must be filled.");
        return false;
    }
    if (!nameInput.value.match(namePattern)) {
        alert("Name input contains only letters");
        return false;
    }
    if (!phoneInput.value.match(phonePattern)) {
        alert("Phone input contains only numbers and '+'");
        return false;
    }
    return true
}

// Form Validation Ends