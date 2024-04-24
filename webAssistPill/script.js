

// Function to show the new medication form
function showNewMedicationForm() {
    const form = document.querySelector(".new-medication-form");
    form.style.display = "block";
}



// Function to exit the user account
function exitAccount() {
    window.location.href = "main.aspx";
}


function scrollToTop() {
    window.scrollTo({
        top: 0,
        behavior: 'smooth'
    });
}

// Function to show the add medication form
function showAddMedicationForm() {
    const form = document.querySelector(".add-medication-form");
    form.style.display = "block";
}

// Function to exit/close a form by hiding it
function exitForm(formSelector) {
    const form = document.querySelector(formSelector);
    form.style.display = "none";
}
// Function to show the error popup with a specific message
function showErrorPopup(message) {
    const errorPopup = document.getElementById("error-popup");
    const errorMessage = document.getElementById("error-message");

    errorMessage.textContent = message;
    errorPopup.style.display = "block";
}

// Function to close the error popup
function closeErrorPopup() {
    const errorPopup = document.getElementById("error-popup");
    errorPopup.style.display = "none";
}


function validateMedicationForm() {
    const medicationName = document.getElementById("medication-name-add").value;
    const howToTake = document.getElementById("medication-how-to-take-add").value;
    const quantity = parseInt(document.getElementById("medication-quantity-add").value);

    if (medicationName.length < 4) {
        showError("Medication name must be at least 4 characters.");
        return false;
    }

    if (howToTake.length < 1) {
        showError("How to take field cannot be empty.");
        return false;
    }

    if (quantity <= 0 || isNaN(quantity)) {
        showError("Quantity must be a positive number.");
        return false;
    }

    return true;
}

function showError(errorMessage) {
    const errorPopup = document.getElementById("error-popup");
    const errorMessageElement = document.getElementById("error-message");
    errorMessageElement.textContent = errorMessage;
    errorPopup.style.display = "block";
}

function addMedicationFormValidation() {
    if (validateMedicationForm()) {
        addMedication();
    }
}
function confirmRemove(attendantName) {
    return confirm("Are you sure you want to remove " + attendantName + "?");
}

function showMedicationDetails(name, imgPath, instructions, description) {
    var modal = document.getElementById('medicationModal');
    var modalImg = document.getElementById('medicationImg');
    var modalName = document.getElementById('medicationName');
    var modalInstructions = document.getElementById('medicationInstructions');
    var modalDescription = document.getElementById('medicationDescription');

    // Check if the medication image exists
    var imagePath = '../images/medication_images/' + imgPath;
    var defaultImagePath = '../images/medication_images/default_image.png';

    // Set the image source and name in the modal
    modalImg.src = doesImageExist(imagePath) ? imagePath : defaultImagePath;
    modalName.innerHTML = name;
    modalInstructions.innerHTML = 'Instructions: ' + instructions;
    modalDescription.innerHTML = 'Description: ' + description;

    // Show the modal
    modal.style.display = 'block';
}

function showEditedPicture(imgPath) {
    var modalImg = document.getElementById('imagePreviewEdit');
    var imagePath = '../images/medication_images/' + imgPath;
    var defaultImagePath = '../images/medication_images/default_image.png';

    // Set the image source and name in the modal
    modalImg.src = doesImageExist(imagePath) ? imagePath : defaultImagePath;
}

function closeMedicationModal() {
    var modal = document.getElementById('medicationModal');

    // Hide the modal
    modal.style.display = 'none';
}


// Function to check if an image exists
function doesImageExist(url) {
    var http = new XMLHttpRequest();
    http.open('HEAD', url, false);
    http.send();
    return http.status !== 404;
}

function previewImage(input) {
    var preview = document.getElementById('imagePreview');
    var file = input.files[0];
    var reader = new FileReader();

    reader.onloadend = function () {
        preview.src = reader.result;
    }

    if (file) {
        reader.readAsDataURL(file);
    } else {
        preview.src = "";
    }
}
function previewImageEdit(input) {
    var preview = document.getElementById('imagePreviewEdit');
    var file = input.files[0];
    var reader = new FileReader();

    reader.onloadend = function () {
        preview.src = reader.result;
    }

    if (file) {
        reader.readAsDataURL(file);
    } else {
        preview.src = "";
    }
}