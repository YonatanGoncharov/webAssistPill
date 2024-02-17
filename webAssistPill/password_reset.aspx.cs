using AssistPillBL;
using System;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;

namespace webAssistPill
{
    public partial class password_reset : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void changePasButton_Click(object sender, EventArgs e)
        {
            string newPassword = txtNewPass.Text;
            string confirmPassword = txtConPass.Text;
            string thisUrl = Request.Url.AbsoluteUri;
            System.Collections.Specialized.NameValueCollection queryString = System.Web.HttpUtility.ParseQueryString(new Uri(thisUrl).Query);
            string userEmail = queryString["user"];


            if (!string.IsNullOrWhiteSpace(newPassword) && !string.IsNullOrWhiteSpace(confirmPassword))
            {
                if (!newPassword.Equals(confirmPassword))
                {
                    errorMessage.Text = "The password does not match with eachother";
                }
                else
                {
                    if (AttendantBL.IsAttendant(userEmail))
                    {
                        try
                        {

                            AttendantBL.UpdateForNewPassword(userEmail, newPassword);
                        }
                        catch (Exception ex)
                        {
                            if (ex.Message.Equals("Same password"))
                            {
                                errorMessage.Text = "The password is the same as the old one!";
                            }
                        }
                    }
                    else if (UserBL.IsUser(userEmail))
                    {
                        try
                        {
                            UserBL.UpdateForNewPassword(userEmail, newPassword);
                        }
                        catch (Exception ex)
                        {
                            if (ex.Message.Equals("Same password"))
                            {
                                errorMessage.Text = "The password is the same as the old one!";
                            }
                        }
                    }
                    Response.Redirect("main.aspx");
                }
            }
        }

        protected void cvPassword_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (txtNewPass.Text.Length == 0)
            {
                cvPassword.ErrorMessage = "Please enter a password";
                args.IsValid = false;
            }
            else if (txtNewPass.Text.Length < 8)
            {
                cvPassword.ErrorMessage = "Password must contain atleast 8 characters!";
                args.IsValid = false;
            }
            else
            {
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

        protected void cvConfirmPassword_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (txtConPass.Text.Length == 0)
            {
                cvConfirmPassword.ErrorMessage = "Please enter a password";
                args.IsValid = false;
            }
            else if (txtConPass.Text.Length < 8)
            {
                cvConfirmPassword.ErrorMessage = "Password must contain atleast 8 characters!";
                args.IsValid = false;
            }
            else
            {
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