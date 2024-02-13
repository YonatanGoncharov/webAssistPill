

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


  // Function to remove a medication
function removeMedication(medicationId) {
    const medicationElement = document.querySelector(`.medication[value="${medicationId}"]`);
    if (medicationElement) {
        medicationElement.remove();
    }
}



// Function to edit a medication
function editMedication() {
    const medicationId = document.getElementById("medication-id-edit").value;
    const medicationNameEdit = document.getElementById("medication-name-edit").value;
    const medicationHowToTakeEdit = document.getElementById("medication-how-to-take-edit").value;
    const medicationQuantityEdit = document.getElementById("medication-quantity-edit").value;

    // Find the medication in the list by its ID
    const medicationElement = document.querySelector(`.medication[value="${medicationId}"]`);

    if (medicationElement) {
        // Update the medication information
        const medicationInfo = medicationElement.querySelector(".medication-info");
        medicationInfo.innerHTML = `
            Medication Name: ${medicationNameEdit}<br>
            How to Take: ${medicationHowToTakeEdit}<br>
            Quantity: ${medicationQuantityEdit}
        `;
    }

    // Hide the edit form
    const form = document.querySelector(".edit-medication-form");
    form.style.display = "none";
}




// Function to show the add medication form
function showAddMedicationForm() {
    const form = document.querySelector(".add-medication-form");
    form.style.display = "block";
}

// Function to add a new medication
function addMedication() {
    const medicationNameAdd = document.getElementById("medication-name-add").value;
    const medicationHowToTakeAdd = document.getElementById("medication-how-to-take-add").value;
    const medicationQuantityAdd = document.getElementById("medication-quantity-add").value;

    // Create a new medication element
    const newMedicationElement = document.createElement("div");
    newMedicationElement.className = "medication";
    const medicationId = Date.now(); // Generate a unique ID for the new medication

    // Set the unique ID as the value attribute
    newMedicationElement.setAttribute("value", medicationId);

    newMedicationElement.innerHTML = `
        <div class="medication-info">
            Medication Name: ${medicationNameAdd}<br>
            How to Take: ${medicationHowToTakeAdd}<br>
            Quantity: ${medicationQuantityAdd}
        </div>
        <button class="remove-button" onclick="removeMedication(${medicationId})">Remove</button>
        <button class="form-button" onclick="showEditMedicationForm(${medicationId})">Edit</button>
    `;

    // Add the new medication element to the medication list
    const medicationList = document.querySelector(".medication-list");
    medicationList.appendChild(newMedicationElement);

    // Clear the input fields
    document.getElementById("medication-name-add").value = "";
    document.getElementById("medication-how-to-take-add").value = "";
    document.getElementById("medication-quantity-add").value = "";

    // Hide the add medication form
    const form = document.querySelector(".add-medication-form");
    form.style.display = "none";
}


// Function to add a new medication for the schedule
// Function to add a new medication for the schedule
function addMedicationSchedule() {
    
    if (isFormValid()){     
          // Get the user input values
          var selectElement = document.querySelector('#medication-dropdown');
          const medicationName = selectElement.options[selectElement.selectedIndex].innerText;
          const medicationTime = document.getElementById("medication-time").value;
        const repeatDays = getSelectedDays();

        // Determine the time of day
        let timeOfDay;
        const hour = parseInt(medicationTime.split(":")[0]);
        if (hour >= 6 && hour < 12) {
            timeOfDay = "Morning";
        } else if (hour >= 12 && hour < 17) {
            timeOfDay = "Afternoon";
        } else if (hour >= 17 && hour < 20) {
            timeOfDay = "Evening";
        } else {
            timeOfDay = "Noon";
        }

        // Create a new medication element with a unique ID
        const newMedicationElement = document.createElement("div");
        newMedicationElement.className = "medication";
        const medicationId = Date.now(); // Generate a unique ID for the new medication
        newMedicationElement.setAttribute("value", medicationId); // Assign a unique ID

        // Populate the new medication element
        newMedicationElement.innerHTML = `
            <div class="medication-info">
                Medication Name: ${medicationName} <br> Time to Take: ${medicationTime} (${timeOfDay})<br> Repeat Days: ${repeatDays}
            </div>
            <button class="remove-button" onclick="removeMedication(${medicationId})">Remove</button>
            <button class="edit-button" onclick="showEditMedicationScheduleForm(${medicationId})">Edit</button>
        `;

        // Add the new medication element to the medication list
        const medicationList = document.querySelector(".medication-list");
        medicationList.appendChild(newMedicationElement);

        // Clear the input fields
        document.getElementById("medication-name").value = "";
        document.getElementById("medication-time").value = "";
        clearSelectedDays();
    }
    else{
        showErrorPopup("Please fill in all fields.");
    }
    
}





// Function to show the edit medication form
function showEditMedicationForm(medicationId) {
    const form = document.querySelector(".edit-medication-form");
    const medicationNameEdit = document.getElementById("medication-name-edit");
    const medicationHowToTakeEdit = document.getElementById("medication-how-to-take-edit");
    const medicationQuantityEdit = document.getElementById("medication-quantity-edit");

    // Set the medication ID in the hidden input field
    document.getElementById("medication-id-edit").value = medicationId;

    // Find the medication element by ID
    const medicationElement = document.querySelector(`.medication[value="${medicationId}"]`);
    
    if (medicationElement) {
        // Extract medication details from the displayed information of the selected medication
        const medicationInfo = medicationElement.querySelector(".medication-info");
        const details = medicationInfo.innerText.split("\n");
        const medicationName = details[0].split(":")[1].trim();
        const medicationHowToTake = details[1].split(":")[1].trim();
        const medicationQuantity = details[2].split(":")[1].trim();

        // Populate the edit form with the current medication details
        medicationNameEdit.value = medicationName;
        medicationHowToTakeEdit.value = medicationHowToTake;
        medicationQuantityEdit.value = medicationQuantity;
    } else {
        // If medicationElement is null, it's a newly added medication, so clear the form fields
        medicationNameEdit.value = "";
        medicationHowToTakeEdit.value = "";
        medicationQuantityEdit.value = "";
    }

    form.style.display = "block";
}






// Function to capitalize the first letter of a string
function capitalizeFirstLetter(string) {
    return string.charAt(0).toUpperCase() + string.slice(1);
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




// Call the function to populate the dropdowns when the page loads
window.addEventListener("load", populateMedicationDropdowns);

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