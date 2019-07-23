"use strict"

// Form Validation Begins

let nameInput = document.querySelector('#name');
let emailInput = document.querySelector('#email');
let phoneInput = document.querySelector('#phone');
let messageInput = document.querySelector('#message');
let submitButton = document.querySelector('.submit');

//Patterns :

let namePattern = /^([a-zA-Z]|\s)*$/;
let emailPattern = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
let phonePattern = /^([+])([0-9])/


function validateForm() {
    if (nameInput.value == "" || emailInput.value == "" || phoneInput.value == "" || messageInput.value == "") {
        alert("Inputs must be filled.")
        nameInput.classList.add("error");
        emailInput.classList.add("error");
        phoneInput.classList.add("error")
        return false
    }
    if (!nameInput.value.match(namePattern)) {
        nameInput.classList.add("error")
        alert("Name input contains only letters")
        return false
    }
    if (!emailInput.value.match(emailPattern)) {
        emailInput.classList.add("error")
        alert("Email input isn't filled correctly.")
        return false
    }
    if (!phoneInput.value.match(phonePattern)) {
        phoneInput.classList.add("error");
        alert("Phone input contains only numbers and '+'");
    }
    return false
}

// Form Validation Ends