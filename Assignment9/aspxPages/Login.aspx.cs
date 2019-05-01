using System;
using System.Web;
using System.Web.UI;
using System.Globalization;
using Assignment9.DAL;

/**
 * C# code for the login page of Assignment 13: The Complete PetAPuppy System
 * Class: CS 356 
 * @author Jeffrey Trotz
 * @date 12/13/2018
 */
namespace Assignment9.aspxPages
{
    public partial class Login : System.Web.UI.Page
    {
        /* Variable to store the number of times the username/password did not match.
         * If the number gets over 5 they will be locked out for 5 minutes. The goal
         * is to prevent people from trying to guess usernames/passwords. This is my
         * attempt at replicating features I've seen on other websites (usually forums
         * have something like this).
         */
        private static int invalidLogins = 0;

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
            // Reset text in case there's any "old" data hanging around
            lblWelcome.Text = "";

            // If an issue was reported, show them some useful information
            if (Session["issueReported"] != null)
            {
                divWelcomeNewUser.Visible = true;
                lblWelcome.Text = "Thank you for bringing this issue to our attention. " +
                    "Please consider creating an account below to track your issue";

                // Set value to null so we don't see it every time the page reloads
                Session["issueReported"] = null;
            }

            // Welcome the new user if they were redirected after creating an account
            if (Session["newUsername"] != null)
            {
                divWelcomeNewUser.Visible = true;
                lblWelcome.Text = "Welcome " + Session["newUsername"].ToString() + "! Please login below";
                
                // Set value to null so we don't see it every time the page reloads
                Session["newUsername"] = null;
            }

            /**
             * Sets a hidden field so the JavaScript can tell if the page is
             * loaded in post back or not so the animations don't play every
             * time a button is clicked
             */
            if (IsPostBack)
            {
                ClientScript.RegisterHiddenField("isPostBack", "True");
            }
        }

        /**
         * This functon is called when the "Log Issues Without Creating an Account" 
         * button is clicked. It redirects the user to the add ticket page
         * @param sender Object that contains a reference to the control/object that raised the event
         * @param e Event data
         */
        protected void BtnReportIssue_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddTicket.aspx");
        }

        /**
         * This functon is called when the "Login" button is clicked. It makes
         * sure the user hasn't been locked out and verifies the validity of
         * their input. If everything checks out it saves their login status in
         * a session variable and redirects them to the main page
         * @param sender Object that contains a reference to the control/object that raised the event
         * @param e Event data
         */
        protected void BtnLogin_Click(object sender, EventArgs e)
        {
            // Hide/clear error message div/label
            divLoginError.Visible = false;
            lblErrorMessage.Text = "";

            // Make sure the user hasn't been locked out and their input is valid
            if (this.CheckForLockout() && this.VerifyInput())
            {
                Response.Redirect("Main.aspx");
            }
        }

        /**
         * This function is called when the "reset" button is clicked.
         * It clears all text boxes and hide any error messages
         * @param sender Object that contains a reference to the control/object 
         * that raised the event
         * @param e Event data
         */
        protected void BtnReset_Click(object sender, EventArgs e)
        {
            // Clear text boxes
            txtUsername.Text = "";
            txtPassword.Text = "";

            // Hide/clear error message div/label
            divLoginError.Visible = false;
            lblErrorMessage.Text = "";
        }

        /**
         * This function is called when the "New User?" link button is clicked.
         * It redirects the user to the create account page
         * @param sender Object that contains a reference to the control/object 
         * that raised the event
         * @param e Event data
         */
        protected void LBtnNewUser_Click(object sender, EventArgs e)
        {
            Response.Redirect("CreateAccount.aspx");
        }

        /**
         * This function is called when the "Forgot Password?" link button is clicked.
         * It redirects the user to the create error page since I have no logic to
         * email them their password (yet)
         * @param sender Object that contains a reference to the control/object 
         * that raised the event
         * @param e Event data
         */
        protected void LBtnForgotPassword_Click(object sender, EventArgs e)
        {
            Response.Redirect("Error.aspx");
        }

        /**
         * This function checks to make sure the user isn't locked out from
         * trying to login too many times. The goal is to deter the user
         * from trying to randomly guess usernames/passwords
         * @ return Returns true if everything checks out
         */
        private bool CheckForLockout()
        {
            // Get the cookie
            HttpCookie lockoutCookie = Request.Cookies["LoginLockout"];

            // Calculate the remaining time and show an error message if the cookie exists
            if (lockoutCookie != null)
            {
                long cookieTime = Convert.ToInt64(Request.Cookies["loginLockout"].Value);
                long currentTime = Convert.ToInt64(DateTime.Now.Ticks);
                double minutes = TimeSpan.FromTicks(cookieTime - currentTime).TotalMinutes;
                double waitMinutes = Math.Round(minutes);
                double waitSeconds = Math.Round((minutes - Math.Truncate(minutes)) * 60);

                divLoginError.Visible = true;
                lblErrorMessage.Text = "You have exceeded your login attempts. Please wait " +
                    waitMinutes + " minutes and " + waitSeconds + " seconds before attempting to login again";
                return false;
            }

            // Return true if the cookie does not exist
            if (lockoutCookie == null)
            {
                return true;
            }

            // Default
            else
            {
                return false;
            }
        }

        /**
         * This function verifies the user's input to make sure it is valid
         * @ return Returns true if everything checks out
         */
        private bool VerifyInput()
        {
            // Create object to access database methods
            DataAccessLayer DBmethods = new DataAccessLayer();

            // Input to verify
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            // Show error message if the user ID field is empty
            if (username.Length == 0)
            {
                divLoginError.Visible = true;
                lblErrorMessage.Text = "Please enter your username";
                return false;
            }

            // Show error message if the password field is empty
            if (password.Length == 0)
            {
                divLoginError.Visible = true;
                lblErrorMessage.Text = "Please enter your password";
                return false;
            }

            // Checks to make sure the username/password match
            if (!DBmethods.UserExists(username, password))
            {
                // Show an error message if the username/password do not match and they have not reached the login limit yet
                if (invalidLogins < 4)
                {
                    invalidLogins += 1;
                    divLoginError.Visible = true;
                    lblErrorMessage.Text = "Invalid login. You have " + (5 - invalidLogins) + " login attempts remaining";
                    return false;
                }

                // Creates a cookie to disable further login attempts for 5 minutes and shows an error message
                else
                {
                    HttpCookie lockoutCookie = new HttpCookie("LoginLockout")
                    {
                        Value = DateTime.Now.AddMinutes(5).Ticks.ToString(),
                        Expires = DateTime.Now.AddMinutes(5)
                    };
                    
                    HttpContext.Current.Response.Cookies.Add(lockoutCookie);
                    divLoginError.Visible = true;
                    lblErrorMessage.Text = "You have exceeded your login attempts. Please try again in 5 minutes";
                    return false;
                }
            }

            // Return true if everything checks out
            else
            {
                Session["loggedIn"] = "true";
                Session["user"] = username;
                return true;
            }
        }
    }
}