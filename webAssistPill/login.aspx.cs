using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AssistPillBL; // Assuming this namespace contains business logic classes for user authentication

namespace webAssistPill
{
    public partial class login : System.Web.UI.Page
    {
        // This method is called when the page loads
        protected void Page_Load(object sender, EventArgs e)
        {
            // No logic currently present in Page_Load
            //UserBL user = new UserBL("yonatantheking123123@gmail.com", "yonabar123");
        }

        // Server-side validation method for the password field
        protected void reqPassword_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (txtPassword.Text.Length == 0)
            {
                cvPassword.ErrorMessage = "Please enter a password";
                args.IsValid = false;
            }
            else if (txtPassword.Text.Length < 8)
            {
                cvPassword.ErrorMessage = "Password must contain at least 8 characters!";
                args.IsValid = false;
            }
            else
            {
                // Validate if the password contains only letters and numbers
                Match m = Regex.Match(txtPassword.Text, @"^[a-zA-Z][a-zA-Z0-9]*$", RegexOptions.IgnoreCase);
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

        // Event handler for the login button click event
        protected void loginButton_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text;
            string password = txtPassword.Text;
            bool isExists = true;

            // Check if email and password fields are not empty
            if (!string.IsNullOrWhiteSpace(email) && !string.IsNullOrWhiteSpace(password))
            {
                try
                {
                    // Try to authenticate the user as a regular user
                    UserBL auser = new UserBL(email, password);
                    Session["User"] = auser;
                    Session["IsAttendant"] = false;
                    Response.Redirect("user_home.aspx");
                }
                catch (Exception ex)
                {
                    // Handle exceptions if the user doesn't exist or the password is wrong
                    if (ex.Message.Equals("The user doesn't exist"))
                    {
                        isExists = false;
                    }
                    else if (ex.Message.Equals("Wrong password"))
                    {
                        errorMessage.Text = ex.Message;
                    }
                }

                // If the user is possibly an attendant, check and redirect to attendant home page
                if (!isExists)
                {
                    try
                    {
                        AttendantBL buser = new AttendantBL(email, password);
                        Session["User"] = buser;
                        Session["IsAttendant"] = true;
                        Response.Redirect("attendant_home.aspx");
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message.Equals("The user doesn't exist"))
                        {
                            errorMessage.Text = ex.Message;
                        }
                        else if (ex.Message.Equals("Wrong password"))
                        {
                            errorMessage.Text = "The password is incorrect";
                        }
                    }
                }
            }
        }

        // Server-side validation method for the email field
        protected void cvEmail_ServerValidate(object source, ServerValidateEventArgs args)
        {
            string email = args.Value;

            if (string.IsNullOrWhiteSpace(email))
            {
                cvEmail.ErrorMessage = "Please enter an email address!";
                args.IsValid = false;
            }
            else
            {
                // Validate email format using MailAddress class
                try
                {
                    var mailAddress = new System.Net.Mail.MailAddress(email);
                    args.IsValid = true;
                }
                catch (FormatException)
                {
                    cvEmail.ErrorMessage = "Please enter a valid email address!";
                    args.IsValid = false;
                }
            }
        }
    }
}
