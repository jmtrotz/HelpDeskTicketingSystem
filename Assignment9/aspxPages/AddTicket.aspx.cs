using System;
using System.Web.UI;
using System.Globalization;
using System.Text.RegularExpressions;
using Assignment9.DAL;

/**
 * C# code for the AddTicket page of Assignment 13: The Complete PetAPuppy System
 * Class: CS 356 
 * @author Jeffrey Trotz
 * @date 12/13/2018
 */
namespace Assignment9.aspxPages
{
    public partial class AddTicket : System.Web.UI.Page
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
         * This function is called when the "report issue" button is clicked. It verifies the user's
         * input, and if everything checks out, it redirects them back to the login page
         * @param sender Object that contains a reference to the control/object that raised the event
         * @param e Event data
         */
        protected void BtnReportIssue_Click(object sender, EventArgs e)
        {
            // Hide error messages
            divAddTicketError.Visible = false;
            lblErrorMessage.Text = "";

            // If the user's input passes verification and is logged in, redirect them to
            // the main page. If they aren't logged in, save a session variable and 
            // redirect to the login page
            if (this.VerifyInput())
            {
                if (Session["loggedIn"] != null)
                {
                    Response.Redirect("Main.aspx");
                }

                // 
                else
                {
                    Session["issueReported"] = "true";
                    Response.Redirect("Login.aspx");
                }
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
            // Clear text boxes
            txtEmailAddress.Text = "";
            txtDescription.Text = "";
            txtStepsToReproduce.Text = "";

            // Hide/clear error message div/label
            divAddTicketError.Visible = false;
            lblErrorMessage.Text = "";
        }

        /**
         * This function verifies the user's input. If everything checks out it adds
         * the new ticket to the database
         */
        protected bool VerifyInput()
        {
            // Input to verify
            string emailAddress = txtEmailAddress.Text;
            string description = txtDescription.Text;
            string stepsToReproduce = txtStepsToReproduce.Text;
            string priorityLevel = ddlPriorityLevel.SelectedValue;

            // Show error message if the email field is empty
            if (emailAddress.Length == 0)
            {
                divAddTicketError.Visible = true;
                lblErrorMessage.Text = "Please enter your email address";
                return false;
            }

            // Show error message if the email address doesn't contain '@'
            if (!Regex.IsMatch(emailAddress, "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*"))
            {
                divAddTicketError.Visible = true;
                lblErrorMessage.Text = "Invalid email address";
                return false;
            }

            // Show error message if the description field is empty
            if (description.Length == 0)
            {
                divAddTicketError.Visible = true;
                lblErrorMessage.Text = "Please enter a description";
                return false;
            }

            // Show error message if the steps to reproduce field is empty
            if (stepsToReproduce.Length == 0)
            {
                divAddTicketError.Visible = true;
                lblErrorMessage.Text = "Please provide steps to reproduce this issue";
                return false;
            }

            // Show error messave if a priority level wasn't chosen
            if (priorityLevel.Length == 0)
            {
                divAddTicketError.Visible = true;
                lblErrorMessage.Text = "Please select a priority level";
                return false;
            }

            // Insert ticket into the database if everything checks out
            else
            {
                DataAccessLayer DBmethods = new DataAccessLayer();
                DBmethods.InsertTicket(emailAddress, description, stepsToReproduce, priorityLevel);
                return true;
            }
        }
    }
}