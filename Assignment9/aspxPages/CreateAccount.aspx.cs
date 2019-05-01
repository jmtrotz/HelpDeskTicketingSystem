using System;
using System.Web.UI;
using System.Globalization;
using System.Text.RegularExpressions;
using Assignment9.DAL;

/**
 * C# code for the create account page of Assignment 13: The Complete PetAPuppy System
 * Class: CS 356 
 * @author Jeffrey Trotz
 * @date 12/13/2018
 */
namespace Assignment9.aspxPages
{
    public partial class CreateAccount : System.Web.UI.Page
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

        /**
         * Executed when the page is loaded
         * @param sender Object that contains a reference to the control/object that raised the event
         * @param e Event data
         */
        protected void Page_Load(object sender, EventArgs e)
        {
            /*
             * Sets a hidden field so the JavaScript can tell if the page is
             * loaded in post back or not so the animations don't play every
             * time the create account or reset buttons are clicked
             */
            if (IsPostBack)
            {
                ClientScript.RegisterHiddenField("isPostBack", "True");
            }
        }

        /**
         * This functon is called when the "create account" button is clicked.
         * It verifies the user's input and redirects them back to the login
         * page if everything checks out
         * @param sender Object that contains a reference to the control/object that raised the event
         * @param e Event data
         */
        protected void BtnCreateAccount_Click(object sender, EventArgs e)
        {
            // Hide/clear error message div/label
            divCreateAccountError.Visible = false;
            lblErrorMessage.Text = "";

            // Redirects user to the login page if their input passes verification
            if (this.VerifyInput())
            {
                Response.Redirect("Login.aspx");
            }
        }

        /**
         * This function is called when the "reset" button is clicked.
         * It clears all text boxes and hide any error messages
         * @param sender Object that contains a reference to the control/object that raised the event
         * @param e Event data
         */
        protected void BtnReset_Click(object sender, EventArgs e)
        {
            // Clear text boxes
            txtUsername.Text = "";
            txtPassword.Text = "";
            txtEmailAddress.Text = "";


            // Hide/clear error message div/label
            divCreateAccountError.Visible = false;
            lblErrorMessage.Text = "";
        }

        /**
         * This function checks each field in the form to
         * make sure the user's input is valid
         * @ return Returns true if everything checks out
         */
        private bool VerifyInput()
        {
            // Create object to access database methods
            DataAccessLayer DBmethods = new DataAccessLayer();

            // Input to verify
            string emailAddress = txtEmailAddress.Text;
            string username = txtUsername.Text;
            string password = txtPassword.Text;
            string confirmPassword = txtConfirmPassword.Text;

            // Show error message if the username field is empty
            if (emailAddress.Length == 0)
            {
                divCreateAccountError.Visible = true;
                lblErrorMessage.Text = "Please enter your email address";
                return false;
            }

            // Show error message if the email address doesn't contain '@'
            if (!Regex.IsMatch(emailAddress, "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*"))
            {
                divCreateAccountError.Visible = true;
                lblErrorMessage.Text = "Invalid email address";
                return false;
            }

            // Show error message if the username field is empty
            if (username.Length == 0)
            {
                divCreateAccountError.Visible = true;
                lblErrorMessage.Text = "Please enter a username";
                return false;
            }

            // Show error message if the username is too short
            if (username.Length < 5)
            {
                divCreateAccountError.Visible = true;
                lblErrorMessage.Text = "Username must be at least 5 characters";
                return false;
            }

            // Show error message if the username is too long
            if (username.Length > 10)
            {
                divCreateAccountError.Visible = true;
                lblErrorMessage.Text = "Username must be less than 10 characters";
                return false;
            }

            // Show error message if the username exists in the database
            if (DBmethods.UsernameExists(username))
            {
                divCreateAccountError.Visible = true;
                lblErrorMessage.Text = "Username already in use";
                return false;
            }

            // Show error message if the password field is empty
            if (password.Length == 0)
            {
                divCreateAccountError.Visible = true;
                lblErrorMessage.Text = "Please enter a password";
                return false;
            }

            // Show error message if the password is too short
            if (password.Length < 8)
            {
                divCreateAccountError.Visible = true;
                lblErrorMessage.Text = "Password must be at least eight characters";
                return false;
            }

            // Show error message if the user does not verify their password
            if (confirmPassword.Length == 0)
            {
                divCreateAccountError.Visible = true;
                lblErrorMessage.Text = "Please confirm your password";
                return false;
            }

            // Show error message if the password does not match the verification
            if (!password.Equals(confirmPassword))
            {
                divCreateAccountError.Visible = true;
                lblErrorMessage.Text = "Passwords do not match";
                return false;
            }

            /*
             * Show error message if the password isn't complex enough. This regex checks for matches 
             * that contain at least one or more lowercase letter, one or more uppercase letter, zero 
             * or more numbers (optional), one or more special character, and is at least 8 characters 
             * long (kind of redundant since I have a check for that above this)
             */
            if (!Regex.IsMatch(password, "^(?=.+[a-z])(?=.+[A-Z])(?=.*\\d?)(?=.+[!@#$%^&*()]).{8,}$"))
            {
                divCreateAccountError.Visible = true;
                lblErrorMessage.Text = "Password must contain one uppercase character and one special character";
                return false;
            }

            // Insert the new user into the database and return true if everything checks out
            else
            {
                DBmethods.InsertUser(emailAddress, username, password);
                Session["newUsername"] = username;
                return true;
            }
        }
    }
}