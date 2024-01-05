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
    public partial class password_reset : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void changePasButton_Click(object sender, EventArgs e)
        {
            string newPassword = txtNewPass.Text;
            string confirmPassword = txtConPass.Text;

            if (!string.IsNullOrWhiteSpace(newPassword) && !string.IsNullOrWhiteSpace(confirmPassword))
            {
                if (Session["User"] != null)
                {
                    if (!newPassword.Equals(confirmPassword))
                    {
                        errorMessage.Text = "The password does not match with eachother";
                    }
                    else
                    {
                        if (Session["User"] is AttendantBL attendant)
                        {     
                            try
                            {
                                attendant.UpdatePassword(newPassword);
                            }
                            catch (Exception ex)
                            {
                                if (ex.Message.Equals("Same password"))
                                {
                                    errorMessage.Text = "The password is the same as the old one!";
                                }
                            }
                        }
                        else if (Session["User"] is UserBL user)
                        {
                            try
                            {
                                user.UpdatePassword(newPassword);
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