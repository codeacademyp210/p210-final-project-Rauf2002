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

    var messageVal = messageInput.value.trim();

    if (nameInput.value.trim() == "" || emailInput.value.trim() == "" || phoneInput.value.trim() == "" || messageInput.value.trim() == "") {
        alert("Inputs must be filled.");
        return false;
    }
    if (!nameInput.value.match(namePattern)) {
        alert("Name input contains only letters");
        return false;
    }
    if (!emailInput.value.match(emailPattern)) {
        alert("Email input isn't filled correctly.");
        return false;
    }
    if (!phoneInput.value.match(phonePattern)) {
        alert("Phone input contains only numbers and '+'");
        return false;
    }
    return true
}

// Form Validation Ends