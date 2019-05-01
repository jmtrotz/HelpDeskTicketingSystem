using System;
using System.Web.UI;
using System.Text.RegularExpressions;
using System.Globalization;
using Assignment9.DAL;

/**
 * C# code for the account details page for Assignment 13: The Complete PetAPuppy System
 * Class: CS 356 
 * @author Jeffrey Trotz
 * @date 12/13/2018
 */
namespace Assignment9.aspxPages
{
    public partial class AccountDetails : System.Web.UI.Page
    {
        /**
         * This function decides whether the holiday or default theme should be applied
         * @param sender Object that contains a reference to the control/object that raised the event
         * @param e Event data
         */
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Get the current date
            DateTime currentDate = DateTime.Today;

            // Holiday start/end dates
            DateTime holidayStartDate = DateTime.ParseExact("10/31", "MM/dd", CultureInfo.InvariantCulture);
            DateTime holidayEndDate = DateTime.ParseExact("12/30", "MM/dd", CultureInfo.InvariantCulture);

            // If the current date is greater than the start date and less than the
            // end date, then apply the holiday theme
            if ((currentDate > holidayStartDate) && (currentDate < holidayEndDate))
            {
                Page.Theme = "Holidays";
            }

            // If it doesn't meet the criteria, apply the default theme
            else
            {
                Page.Theme = "Default";
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            /**
             * Sets a hidden field so the JavaScript can tell if the page is
             * loaded in post back or not so the animations don't play every
             * time a button is clicked
             */
            if (IsPostBack)
            {
                ClientScript.RegisterHiddenField("isPostBack", "True");
            }

            if (Session["loggedIn"] == null)
            {
                Response.Redirect("Login.aspx");
            }

            // Create object to access database methods
            DataAccessLayer DBmethods = new DataAccessLayer();

            // Reset labels in case there's any "old" information hanging around
            lblUsername.Text = "";
            lblEmailAddress.Text = "";

            // Get their username
            string username = Convert.ToString(Session["user"]);

            // Set label text to display username/email
            lblUsername.Text = "Username: " + username;
            lblEmailAddress.Text = "Email address " + DBmethods.GetEmail(username);
        }

        /**
         * This functon is called when the "delete account" button is clicked. It shows the
         * confirmation dialog to make sure they want to delete their account.
         * @param sender Object that contains a reference to the control/object that raised the event
         * @param e Event data
         */
        protected void BtnDeleteAccount_Click(object sender, EventArgs e)
        {
            // Hide any other dialog boxes and show confrirmation
            divAccountInfoError.Visible = false;
            divDeleteDoubleConfirm.Visible = false;
            divFinalConfirm.Visible = false;
            divDeleteConfirm.Visible = true;
        }

        /**
         * This functon is called when the "are you sure you want to delete account" button is clicked. 
         * It shows another confirmation dialog to make sure they REALLY want to delete their account.
         * @param sender Object that contains a reference to the control/object that raised the event
         * @param e Event data
         */
        protected void BtnConfirm_Click(object sender, EventArgs e)
        {
            // Hide any other dialog boxes and show confirmation
            divAccountInfoError.Visible = false;
            divDeleteConfirm.Visible = false;
            divFinalConfirm.Visible = false;
            divDeleteDoubleConfirm.Visible = true;
        }

        /**
         * This functon is called when the "are you sure you want to delete account" button is clicked. 
         * It shows another confirmation dialog to make double sure they REALLY want to delete their account.
         * @param sender Object that contains a reference to the control/object that raised the event
         * @param e Event data
         */
        protected void BtnDoubleConfirm_Click(object sender, EventArgs e)
        {
            // Hide any other dialog boxes and show confirmation
            divAccountInfoError.Visible = false;
            divDeleteConfirm.Visible = false;
            divDeleteDoubleConfirm.Visible = false;
            divFinalConfirm.Visible = true;
        }

        /**
         * This functon is called when the "confirm delete" button is clicked. It finally goes ahead
         * and deletes the user's account
         * @param sender Object that contains a reference to the control/object that raised the event
         * @param e Event data
         */
        protected void BtnFinalConfirm_Click(object sender, EventArgs e)
        {
            // Create object to access database methods
            DataAccessLayer DBMethods = new DataAccessLayer();

            // Hide any error messages and get username/password
            lblDeleteError.Visible = false;
            string username = txtDeleteUsername.Text;
            string password = txtDeletePassword.Text;
            
            // Show error message if the username field is empty
            if (username.Length == 0)
            {
                lblDeleteError.Visible = true;
                lblDeleteError.Text = "Please enter a username";
            }

            // Show error message if the password field is empty
            if (password.Length == 0)
            {
                lblDeleteError.Visible = true;
                lblDeleteError.Text = "Please enter a password";
            }

            // Show error message if the username/password are invalid
            if (!DBMethods.UserExists(username, password))
            {
                lblDeleteError.Visible = true;
                lblDeleteError.Text = "Invalid username or password";
            }

            // If everything checks out, delete their account and redirect
            // them to the goodbye page
            else
            {
                DBMethods.DeleteAccount(username);
                Response.Redirect("Goodbye.aspx");
            }
        }

        /**
         * This functon is called when the "submit" button is clicked.
         * It calls the DecideWhatToVerify() function
         * @param sender Object that contains a reference to the control/object that raised the event
         * @param e Event data
         */
        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            // Hide/clear error message div/label
            divAccountInfoError.Visible = false;
            divDeleteConfirm.Visible = false;
            divDeleteDoubleConfirm.Visible = false;
            divFinalConfirm.Visible = false;
            lblErrorMessage.Text = "";
            lblDeleteError.Text = "";

            // Call function to decide what text boxes to verify
            this.DecideWhatToVerify();
        }
        /**
         * This functon is called when the "reset" button is clicked. It hides any visible dialog boxes.
         * @param sender Object that contains a reference to the control/object that raised the event
         * @param e Event data
         */
        protected void BtnReset_Click(object sender, EventArgs e)
        {
            // Hide error message divs
            divAccountInfoError.Visible = false;
            divDeleteConfirm.Visible = false;
            divDeleteDoubleConfirm.Visible = false;
            divFinalConfirm.Visible = false;

            // Reset labels/text boxes
            lblErrorMessage.Text = "";
            lblDeleteError.Text = "";
            txtEmailAddress.Text = "";
            txtOldPassword.Text = "";
            txtPassword.Text = "";
            txtConfirmPassword.Text = "";
        }

        /**
         * Decides what to verify depending on what text boxes have text in them
         */
        private void DecideWhatToVerify()
        {
            // Input used to decide what to verify
            string emailAddress = txtEmailAddress.Text;
            string oldPassword = txtOldPassword.Text;
            string password = txtPassword.Text;
            string confirmPassword = txtConfirmPassword.Text;

            // If the email text box has text in it, call function to verify email input
            if (emailAddress.Length != 0)
            {
                this.VerifyEmailInput(emailAddress);
            }

            // If any of the password text boxes have text in them, call function to verify password input
            if ((oldPassword.Length != 0) || (password.Length != 0) || (confirmPassword.Length != 0))
            {
                this.VerifyPasswordInput(oldPassword, password, confirmPassword);
            }

            // Show error message of all are empty
            else
            {
                divAccountInfoError.Visible = true;
                lblErrorMessage.Text = "Please enter information into the desired text box to update your account";
            }
        }


        /**
         * This function verifies the user's input if the email text box has text in it.
         * @param emailAddress Email address entered by the user
         */
        private void VerifyEmailInput(string emailAddress)
        {
            // Create object to access database methods
            DataAccessLayer DBMethods = new DataAccessLayer();

            // Show error message if the email address doesn't contain '@'
            if (!Regex.IsMatch(emailAddress, "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*"))
            {
                divAccountInfoError.Visible = true;
                lblErrorMessage.Text = "Invalid email address";
            }

            // Update email address if everything checks out
            else
            {
                DBMethods.UpdateEmail(emailAddress, Convert.ToString(Session["user"]));
                Response.Redirect("AccountDetails.aspx");
            }
        }

        /**
         * This function verifies the user's password input if any of the password
         * text boxes had text in them
         * @param oldPassword Origional password entered by the user
         * @param password New password ented by the user
         * @param confirmPassword Confirmation of the new password entered by the user
         */
        private void VerifyPasswordInput(string oldPassword, string password, string confirmPassword)
        {
            // Create object to access database methods
            DataAccessLayer DBMethods = new DataAccessLayer();

            // Show error message if the password field is empty
            if (password.Length == 0)
            {
                divAccountInfoError.Visible = true;
                lblErrorMessage.Text = "Please enter a password";
            }

            // Show error message if the password is too short
            if (password.Length < 8)
            {
                divAccountInfoError.Visible = true;
                lblErrorMessage.Text = "Password must be at least eight characters";
            }

            // Show error message if the user does not verify their password
            if (confirmPassword.Length == 0)
            {
                divAccountInfoError.Visible = true;
                lblErrorMessage.Text = "Please confirm your password";
            }

            // Show error message if the password does not match the verification
            if (!password.Equals(confirmPassword))
            {
                divAccountInfoError.Visible = true;
                lblErrorMessage.Text = "Passwords do not match";
            }
            
            // Show error message if the old password does not match what's in the database
            if (!DBMethods.UserExists(Session["user"].ToString(), oldPassword))
            {
                divAccountInfoError.Visible = true;
                lblErrorMessage.Text = "Old password does not match our reccords";
            }

            /*
             * Show error message if the password isn't complex enough. This regex checks for matches 
             * that contain at least one or more lowercase letter, one or more uppercase letter, zero 
             * or more numbers (optional), one or more special character, and is at least 8 characters 
             * long (kind of redundant since I have a check for that above this)
             */
            if (!Regex.IsMatch(password, "^(?=.+[a-z])(?=.+[A-Z])(?=.*\\d?)(?=.+[!@#$%^&*()]).{8,}$"))
            {
                divAccountInfoError.Visible = true;
                lblErrorMessage.Text = "Password must contain one uppercase character and one special character";
            }
            
            // Update password if everything checks out
            else
            {
                DBMethods.UpdatePassword(password, Convert.ToString(Session["user"]));
                Response.Redirect("AccountDetails.aspx");
            }
        }
    }
}