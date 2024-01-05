using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AssistPillBL;

namespace webAssistPill
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

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
        protected void loginButton_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text;
            string password = txtPassword.Text;
            bool isExists = true;
            //trying to check if the user is not an attendant and connect him\
            if (!string.IsNullOrWhiteSpace(email) && !string.IsNullOrWhiteSpace(password))
            {
                try
                {
                    UserBL auser = new UserBL(email, password);
                    Session["User"] = auser;
                    Session["IsAttendant"] = false;
                    Response.Redirect("user_home.aspx");
                }
                catch (Exception ex)
                {
                    if (ex.Message.Equals("The user doesn't exist"))
                    {
                        isExists = false;
                    }
                    else if (ex.Message.Equals("Wrong password"))
                    {
                        errorMessage.Text = ex.Message;
                    }
                }
                // if the user is possibly an attendant its checking it and connecting him to his home page
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
    }
}