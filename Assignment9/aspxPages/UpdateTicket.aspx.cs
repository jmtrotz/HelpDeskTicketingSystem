using System;
using System.Web.UI;
using System.Globalization;
using Assignment9.DAL;

/**
 * C# code for the UpateTicket page of Assignment 13: The Complete PetAPuppy System
 * Class: CS 356 
 * @author Jeffrey Trotz
 * @date 12/13/2018
 */
namespace Assignment9.aspxPages
{
    public partial class UpdateTicket : System.Web.UI.Page
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

            // Double check to make sure they're still logged in
            if (Session["loggedIn"] == null)
            {
                Response.Redirect("Login.aspx");
            }

            // Make sure the ticketNumber session variable isn't null
            if (Session["ticketNumber"] != null)
            {
                lblTicketNumber.Text = "Updating ticket number " + Convert.ToString(Session["ticketNumber"]);
            }
        }

        /**
         * This function is called when the "update ticket" button is clicked
         * @param sender Object that contains a reference to the control/object that raised the event
         * @param e Event data
         */
        protected void BtnUpdateTicket_Click(object sender, EventArgs e)
        {
            // Hide/clear error message div/label
            divUpdateTicketError.Visible = false;
            divReassignTicket.Visible = false;
            lblErrorMessage.Text = "";
            lblAssignError.Text = "";

            // If the user's input passes verification return them to the main page
            if (this.VerifyInput())
            {
                Response.Redirect("Main.aspx");
            }
        }

        /**
         * Called when the "Reassign ticket" button is clicked. It shows 
         * a dialog box for reassigning the ticket to another employee.
         * @param sender Object that contains a reference to the control/object that raised the event
         * @param e Event data
         */
        protected void BtnReassign_Click(object sender, EventArgs e)
        {
            // Hide error box if it's visible and show the ticket box
            divUpdateTicketError.Visible = false;
            divReassignTicket.Visible = true;
        }

        /**
         * Called when the "Confirm" button is clicked. It verifies the user's input
         * and redirects them back to the main page if all is ok
         * @param sender Object that contains a reference to the control/object that raised the event
         * @param e Event data
         */
        protected void BtnConfirm_Click(object sender, EventArgs e)
        {
            // Clear any visible error messages
            lblAssignError.Text = "";

            // Redirect to main page if everything checks out
            if (this.VerifyReassign())
            {
                Response.Redirect("Main.aspx");
            }
        }

        /**
         * This function is called when the "reset" button is clicked. It clears all of the text boxes
         * and hides any error messages
         * @param sender Object that contains a reference to the control/object that raised the event
         * @param e Event data
         */
        protected void BtnReset_Click(object sender, EventArgs e)
        {
            // Clear text box
            txtResolutionDetails.Text = "";

            // Hide/clear error message div/label
            divUpdateTicketError.Visible = false;
            divReassignTicket.Visible = false;
            lblErrorMessage.Text = "";
            lblAssignError.Text = "";
        }

        /**
         * This function verifies the user's input. If everything 
         * checks out it updates the ticket in the database
         */
        protected bool VerifyInput()
        {
            // Input to verify
            string resolutionDetails = txtResolutionDetails.Text;

            // Show error message if the resolution details field is blank
            if (resolutionDetails.Length == 0)
            {
                divUpdateTicketError.Visible = true;
                lblErrorMessage.Text = "Please provide resolution details";
                return false;
            }

            // Update ticket in the database if everything checks out
            else
            {
                DataAccessLayer users = new DataAccessLayer();

                // Make sure input for DAL method isn't null before proceeding
                if ((Session["ticketNumber"] != null) && (Session["user"] != null))
                {
                    users.UpdateTicket(Convert.ToInt32(Session["ticketNumber"]), resolutionDetails, Convert.ToString(Session["user"]));
                    return true;
                }

                // Redirect to error page if "user" session variable is null
                else
                {
                    Response.Redirect("ErrorPage.aspx");
                    return false;
                }
            }
        }

        /**
         * This function verifies the user's input. If everything 
         * checks out it updates the ticket in the database
         */
        protected bool VerifyReassign()
        {
            // Create objects and get username
            DataAccessLayer users = new DataAccessLayer();
            string username = txtUsername.Text;

            // Show error message if no username was entered
            if (username.Length == 0)
            {
                lblAssignError.Text = "Please enter a username";
                return false;
            }

            // Show error if username does not exist
            if (!users.UsernameExists(username))
            {
                lblAssignError.Text = username + " does not exist";
                return false;
            }

            // Show error message if user is not an admin (employee)
            if (!users.IsAdmin(username))
            {
                lblAssignError.Text = username + " is not an employee";
                return false;
            }

            // Update ticket assignmetn if everything checks out 
            else
            {
                users.ReassignTicket(username, Convert.ToInt32(Session["ticketNumber"]));
                return true;
            }
        }
    }
}