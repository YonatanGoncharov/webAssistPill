using AssistPillBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace webAssistPill
{
    public partial class register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        //register button
        protected void registerButton_Click(object sender, EventArgs e)
        {
            string email = txtEmailReg.Text;
            string password = txtPassword.Text;
            string firstN = txtfnameReg.Text;
            string lastN = txtlNameReg.Text;
            bool isAttendant = isAttendantCb.Checked;
            if (!string.IsNullOrWhiteSpace(email) || !string.IsNullOrWhiteSpace(password) || !string.IsNullOrWhiteSpace(firstN) || !string.IsNullOrWhiteSpace(lastN))
            {
                if (!email.Contains("gmail") && !email.Contains("yahoo") && !email.Contains("office365"))
                {
                    errorMessage.Text = "This site doesn't support the following email";
                    return;
                }
                //if the user checked that he is an attendant
                if (isAttendant)
                {
                    try
                    {
                        AttendantBL user = new AttendantBL(firstN, lastN, email, password);
                        Session["User"] = user;
                        Session["IsAttendant"] = true;
                        Response.Redirect("attendant_home.aspx");
                        
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message.Equals("Used Email"))
                        {
                            errorMessage.Text = ex.Message;
                        }
                    }
                }
                //if he is a patient
                else
                {
                    try
                    {
                        UserBL user = new UserBL(firstN, lastN, email, password);
                        Session["User"] = user;
                        Session["IsAttendant"] = false;
                        Response.Redirect("user_home.aspx");
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message.Equals("Used Email"))
                        {
                            errorMessage.Text = ex.Message;
                        }
                    }
                }
            }

          
            
        }
        //first name validate
        protected void cvFname_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (txtfnameReg.Text.Length == 0)
            {
                cvFname.ErrorMessage = "Please enter a first name";
                args.IsValid = false;
            }
            else if (txtfnameReg.Text.Length < 2)
            {
                cvFname.ErrorMessage = "First name must contain atleast 2 characters!";
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
                Match m = Regex.Match(txtfnameReg.Text, @"^[a-zA-Z]*$", RegexOptions.IgnoreCase);
                if (!m.Success)
                {
                    cvFname.ErrorMessage = "First name can only contain letters!";
                    args.IsValid = false;
                }
                else
                {
                    args.IsValid = true;
                }
            }
        }
        //last name validate
        protected void cvLname_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (txtlNameReg.Text.Length == 0)
            {
                cvLname.ErrorMessage = "Please enter a last name";
                args.IsValid = false;
            }
            else if (txtlNameReg.Text.Length < 2)
            {
                cvLname.ErrorMessage = "Last name must contain atleast 2 characters!";
                args.IsValid = false;
            }
            else
            {
                // Define a regular expression pattern to match the format of the password
                // ^ asserts the start of the string
                // [a-zA-Z] matches any letter (lowercase or uppercase) at the beginning of the string
                // $ asserts the end of the string
                // RegexOptions.IgnoreCase is used to perform case-insensitive matching

                Match m = Regex.Match(txtlNameReg.Text, @"^[a-zA-Z]*$", RegexOptions.IgnoreCase);
                if (!m.Success)
                {
                    cvLname.ErrorMessage = "Last name can only contain letters!";
                    args.IsValid = false;
                }
                else
                {
                    args.IsValid = true;
                }
            }
        }
        //email validate
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
                // Add your custom email validation logic here
                // For example, you can use .NET's built-in MailAddress class
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
        //password validate
        protected void reqPassword_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (txtPassword.Text.Length == 0)
            {
                cvPassword.ErrorMessage = "Please enter a password";
                args.IsValid = false;
            }
            else if (txtPassword.Text.Length < 8)
            {
                cvPassword.ErrorMessage = "Password must contain atleast 8 characters!";
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
    }
}