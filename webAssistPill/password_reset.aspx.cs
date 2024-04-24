using AssistPillBL; // Importing the necessary namespace for business logic operations
using System;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;

namespace webAssistPill
{
    public partial class password_reset : System.Web.UI.Page
    {
        // This method is called when the page loads
        protected void Page_Load(object sender, EventArgs e)
        {
            // No logic currently present in Page_Load
        }

        // Event handler for the change password button click event
        protected void changePasButton_Click(object sender, EventArgs e)
        {
            // Retrieve values from the password and confirm password fields
            string newPassword = txtNewPass.Text;
            string confirmPassword = txtConPass.Text;

            // Retrieve user email from the query string in the URL
            string thisUrl = Request.Url.AbsoluteUri;
            System.Collections.Specialized.NameValueCollection queryString = System.Web.HttpUtility.ParseQueryString(new Uri(thisUrl).Query);
            string userEmail = queryString["user"];

            // Check if both password fields are not empty
            if (!string.IsNullOrWhiteSpace(newPassword) && !string.IsNullOrWhiteSpace(confirmPassword))
            {
                // Check if the passwords match
                if (!newPassword.Equals(confirmPassword))
                {
                    errorMessage.Text = "The password does not match with each other";
                }
                else
                {
                    // Check if the user is an attendant and update password accordingly
                    if (AttendantBL.IsAttendant(userEmail))
                    {
                        try
                        {
                            AttendantBL.UpdateForNewPassword(userEmail, newPassword);
                        }
                        catch (Exception ex)
                        {
                            // Handle exception if the new password is the same as the old one
                            if (ex.Message.Equals("Same password"))
                            {
                                errorMessage.Text = "The password is the same as the old one!";
                            }
                        }
                    }
                    // Check if the user is a regular user and update password accordingly
                    else if (UserBL.IsUser(userEmail))
                    {
                        try
                        {
                            UserBL.UpdateForNewPassword(userEmail, newPassword);
                        }
                        catch (Exception ex)
                        {
                            // Handle exception if the new password is the same as the old one
                            if (ex.Message.Equals("Same password"))
                            {
                                errorMessage.Text = "The password is the same as the old one!";
                            }
                        }
                    }
                    // Redirect the user to the main page after successful password change
                    Response.Redirect("main.aspx");
                }
            }
        }

        // Server-side validation method for the new password field
        protected void cvPassword_ServerValidate(object source, ServerValidateEventArgs args)
        {
            // Validate password length and character composition
            if (txtNewPass.Text.Length == 0)
            {
                cvPassword.ErrorMessage = "Please enter a password";
                args.IsValid = false;
            }
            else if (txtNewPass.Text.Length < 8)
            {
                cvPassword.ErrorMessage = "Password must contain at least 8 characters!";
                args.IsValid = false;
            }
            else
            {
                // Define a regular expression pattern to match the format of the password
                // ^ asserts the start of the string
                // [a-zA-Z] matches any letter (lowercase or uppercase) at the beginning of the string
                // [a-zA-Z0-9]* matches zero or more occurrences of letters (lowercase or uppercase) or digits (0-9)
                // $ asserts the end of the string
                // RegexOptions.IgnoreCase is used to perform case-insensitive matching

                Match m = Regex.Match(txtNewPass.Text, @"^[a-zA-Z][a-zA-Z0-9]*$", RegexOptions.IgnoreCase);
                if (!m.Success)
                {
                    cvPassword.ErrorMessage = "Password can only contain letters and numbers";
                    args.IsValid = false;
                }
                else
                {
                    args.IsValid = true;
                }
            }
        }

        // Server-side validation method for the confirm password field
        protected void cvConfirmPassword_ServerValidate(object source, ServerValidateEventArgs args)
        {
            // Validate password length and character composition
            if (txtConPass.Text.Length == 0)
            {
                cvConfirmPassword.ErrorMessage = "Please enter a password";
                args.IsValid = false;
            }
            else if (txtConPass.Text.Length < 8)
            {
                cvConfirmPassword.ErrorMessage = "Password must contain at least 8 characters!";
                args.IsValid = false;
            }
            else
            {
                // Define a regular expression pattern to match the format of the password
                // ^ asserts the start of the string
                // [a-zA-Z] matches any letter (lowercase or uppercase) at the beginning of the string
                // [a-zA-Z0-9]* matches zero or more occurrences of letters (lowercase or uppercase) or digits (0-9)
                // $ asserts the end of the string
                // RegexOptions.IgnoreCase is used to perform case-insensitive matching

                Match m = Regex.Match(txtConPass.Text, @"^[a-zA-Z][a-zA-Z0-9]*$", RegexOptions.IgnoreCase);
                if (!m.Success)
                {
                    cvConfirmPassword.ErrorMessage = "Password can only contain letters and numbers";
                    args.IsValid = false;
                }
                else
                {
                    args.IsValid = true;
                }
            }
        }
    }
}
