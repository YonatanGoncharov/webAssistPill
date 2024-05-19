// Function to show the new medication form
function showNewMedicationForm() {
    const form = document.querySelector(".new-medication-form"); // Select the new medication form
    form.style.display = "block"; // Display the form
}

// Function to exit the user account
function exitAccount() {
    window.location.href = "main.aspx"; // Redirect to the main page
}

// Function to show the add medication form
function showAddMedicationForm() {
    const form = document.querySelector(".add-medication-form"); // Select the add medication form
    form.style.display = "block"; // Display the form
}

// Function to exit/close a form by hiding it
function exitForm(formSelector) {
    const form = document.querySelector(formSelector); // Select the form by selector
    form.style.display = "none"; // Hide the form
}

// Function to show the error popup with a specific message
function showErrorPopup(message) {
    const errorPopup = document.getElementById("error-popup"); // Get the error popup element
    const errorMessage = document.getElementById("error-message"); // Get the error message element

    errorMessage.textContent = message; // Set the error message
    errorPopup.style.display = "block"; // Display the error popup
}

// Function to close the error popup
function closeErrorPopup() {
    const errorPopup = document.getElementById("error-popup"); // Get the error popup element
    errorPopup.style.display = "none"; // Hide the error popup
}


// Function to handle form submission after validation
function addMedicationFormValidation() {
    if (validateMedicationForm()) { // If form validation passes
        addMedication(); // Call function to add medication
    }
}

// Function to confirm removal of an attendant
function confirmRemove(attendantName) {
    return confirm("Are you sure you want to remove " + attendantName + "?"); // Show confirmation dialog
}

// Function to show medication details in a modal
function showMedicationDetails(name, imgPath, instructions, description) {
    var modal = document.getElementById('medicationModal'); // Get the medication modal element
    var modalImg = document.getElementById('medicationImg'); // Get the modal image element
    var modalName = document.getElementById('medicationName'); // Get the modal name element
    var modalInstructions = document.getElementById('medicationInstructions'); // Get the modal instructions element
    var modalDescription = document.getElementById('medicationDescription'); // Get the modal description element

    // Check if the medication image exists
    var imagePath = '../images/medication_images/' + imgPath; // Construct image path
    var defaultImagePath = '../images/medication_images/default_image.png'; // Default image path

    // Set the image source and name in the modal
    modalImg.src = doesImageExist(imagePath) ? imagePath : defaultImagePath; // Set modal image source
    modalName.innerHTML = name; // Set modal name
    modalInstructions.innerHTML = 'Instructions: ' + instructions; // Set modal instructions
    modalDescription.innerHTML = 'Description: ' + description; // Set modal description

    // Show the modal
    modal.style.display = 'block'; // Display the modal
}

// Function to show an edited picture in a modal
function showEditedPicture(imgPath) {
    var modalImg = document.getElementById('imagePreviewEdit'); // Get the modal image element
    var imagePath = '../images/medication_images/' + imgPath; // Construct image path
    var defaultImagePath = '../images/medication_images/default_image.png'; // Default image path

    // Set the image source in the modal
    modalImg.src = doesImageExist(imagePath) ? imagePath : defaultImagePath; // Set modal image source
}

// Function to close the medication modal
function closeMedicationModal() {
    var modal = document.getElementById('medicationModal'); // Get the medication modal element

    // Hide the modal
    modal.style.display = 'none'; // Hide the modal
}

// Function to check if an image exists
function doesImageExist(url) {
    var http = new XMLHttpRequest(); // Create a new XMLHttpRequest
    http.open('HEAD', url, false); // Open a synchronous HEAD request to the URL
    http.send(); // Send the request
    return http.status !== 404; // Return whether the request was successful (image exists)
}

// Function to preview an image selected for upload
function previewImage(input) {
    var preview = document.getElementById('imagePreview'); // Get the image preview element
    var file = input.files[0]; // Get the selected file
    var reader = new FileReader(); // Create a new FileReader instance

    // Event listener for when file reading is complete
    reader.onloadend = function () {
        preview.src = reader.result; // Set the preview image source to the read result
    }

    if (file) { // If a file is selected
        reader.readAsDataURL(file); // Read the file as a data URL
    } else {
        preview.src = ""; // Clear the preview if no file is selected
    }
}

// Function to preview an image selected for editing
function previewImageEdit(input) {
    var preview = document.getElementById('imagePreviewEdit'); // Get the image preview element
    var file = input.files[0]; // Get the selected file
    var reader = new FileReader(); // Create a new FileReader instance

    // Event listener for when file reading is complete
    reader.onloadend = function () {
        preview.src = reader.result; // Set the preview image source to the read result
    }

    if (file) { // If a file is selected
        reader.readAsDataURL(file); // Read the file as a data URL
    } else {
        preview.src = ""; // Clear the preview if no file is selected
    }
}
