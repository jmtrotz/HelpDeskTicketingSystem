using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using Assignment9.DAL;
using System.Data;

/**
 * C# code for the main page of Assignment 13: The Complete PetAPuppy System
 * Class: CS 356 
 * @author Jeffrey Trotz
 * @date 12/13/2018
 */
namespace Assignment9.aspxPages
{
    public partial class Main : System.Web.UI.Page
    {
        /**
         * This function decides whether the holiday or default theme should be applied, redirects
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
          /**
           * Sets a hidden field so the JavaScript can tell if the page is
           * loaded in post back or not so the animations don't play every
           * time a buttonis clicked
           */
            if (IsPostBack)
            {
                ClientScript.RegisterHiddenField("isPostBack", "True");
            }

            // Redirect the user to the login page if their session has timed out
            if (Session["loggedIn"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            
            // Welcome the user when they login
            if ((Session["lastLoginDate"] != null) && (Session["user"] != null))
            {
                lblWelcomeMessage.Text = "Hello " + Convert.ToString(Session["user"]) + 
                    "! You last visited " + Convert.ToString(Session["lastLoginDate"]);
            }

            // Get all tickets from the database
            DataTable allTickets = this.GetAllTickets();

            // Show the admin ticket grid and hide the dumbed-down user ticket grid if the user is an admin
            if (this.IsAdmin())
            {
                gvUserTickets.Visible = false;
                gvAdminTickets.Visible = true;
                btnHigh.Visible = true;
                btnMedium.Visible = true;
                btnLow.Visible = true;
                btnUnassigned.Visible = true;

                gvAdminTickets.DataSource = allTickets;
                gvAdminTickets.DataBind();
            }
            
            // If the user is not an admin, bind tickets to the regular ticket grid
            else
            {
                gvUserTickets.DataSource = allTickets;
                gvUserTickets.DataBind();
            }
        }

        /**
         * Called when the "update ticket" button in the gridview is selected.
         * It gets the ticket number for the selected row and saves it as a
         * session variable for use by the update ticket page
         * @param sender Object that contains a reference to the control/object that raised the event
         * @param e Event data
         */
        protected void GvTickets_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            // Make sure the command name matches what we're looking for. 
            // Used to fix redirects to the update ticket page when trying to change page index
            if (e.CommandName.Equals("UpdateTicket"))
            {
                // Get ticket number and redirect to update ticket page
                Session["ticketNumber"] = Convert.ToString(e.CommandArgument);
                Response.Redirect("UpdateTicket.aspx");
            }
        }

        /**
         * This function color codes the tickets based on their date after the data has been
         * bound to the grid. Tickets older than 7 days are red. Tickets between 3 and 7 days
         * old are yellow. Tickets between 0 and 3 days old are green
         * @param sender Object that contains a reference to the control/object that raised the event
         * @param e Event data
         */
        protected void GvTickets_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Convert grid data to DateTime format and get the current date
                DateTime ticketDate = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "TicketDate"));
                DateTime currentDate = DateTime.Today;

                // Color the ticket text red if it's older than 7 days
                if ((currentDate - ticketDate).Days > 7)
                {
                    e.Row.ForeColor = System.Drawing.Color.Red;
                }

                // Color the ticket text gold (more readable than yellow) if they're between 3 and 7 days old
                else if ((currentDate - ticketDate).Days < 7 && (currentDate - ticketDate).Days > 3)
                {
                    e.Row.ForeColor = System.Drawing.Color.Gold;
                }

                // Color the ticket text green if they're less than 3 days old
                else
                {
                    e.Row.ForeColor = System.Drawing.Color.Green;
                }
            }
        }

        /**
         * This function rebinds the data to the ticket grid when the user goes to the next page
         * @param sender Object that contains a reference to the control/object that raised the event
         * @param e Event data
         */
        protected void GvTickets_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            // Create object to access database methods and get the ticket data
            DataAccessLayer DBmethods = new DataAccessLayer();
            DataTable tickets = new DataTable();
            tickets = DBmethods.GetAllTickets();

            // If the user is an admin, perform actions on the admin ticket grid
            if (this.IsAdmin())
            {
                gvAdminTickets.PageIndex = e.NewPageIndex;               
                gvAdminTickets.DataSource = tickets;                
                gvAdminTickets.DataBind();
            }

            // If they're just a regular user, performa actions on the regular ticket grid
            else
            {
                gvUserTickets.PageIndex = e.NewPageIndex;
                gvUserTickets.DataSource = tickets;
                gvUserTickets.DataBind();
            }            
        }

        /**
         * This function is called when the "All Tickets" button is selected.
         * It shows all tickets from the database in the data grid.
         * @param sender Object that contains a reference to the control/object that raised the event
         * @param e Event data
         */
        protected void BtnAll_Click(object sender, EventArgs e)
        {
            // Get all tickets
            DataTable allTickets = this.GetAllTickets();

            // Bind data to the admin grid if the user is an admin
            if (this.IsAdmin())
            {
                gvAdminTickets.DataSource = allTickets;
                gvAdminTickets.DataBind();
            }

            // Bind data to the user grid if the user is not an admin
            else
            {
                gvUserTickets.DataSource = allTickets;
                gvUserTickets.DataBind();
            }
        }

        /**
         * This function is called when the "My Tickets" button is selected. It gets
         * tickets for a specific user. If the user is an admin, it gets all tickets 
         * assigned to them. If they're just a regular user, it gets all tickets that
         * they have submitted
         * @param sender Object that contains a reference to the control/object that raised the event
         * @param e Event data
         */
        protected void BtnMyTickets_Click(object sender, EventArgs e)
        {
            // Get username and create objects
            string username = Convert.ToString(Session["user"]);
            DataAccessLayer DBmethods = new DataAccessLayer();
            DataTable specificTickets = new DataTable();

            // Get assigned tickets if the user is an admin
            if (this.IsAdmin())
            {
                specificTickets = DBmethods.GetAdminTickets(username);
                gvAdminTickets.DataSource = specificTickets;
                gvAdminTickets.DataBind();
            }

            // Get all submitted tickets if they're just a regular user
            else
            {
                specificTickets = DBmethods.GetUserTickets(username);
                gvUserTickets.DataSource = specificTickets;
                gvUserTickets.DataBind();
            }            
        }

        /**
         * This function is called when the "My Open Tickets" button is called. If
         * the user is an admin, it gets all open tickets that have been assigned to them. 
         * If the user is not an admin, it gets all open tickets that they have submitted
         * @param sender Object that contains a reference to the control/object that raised the event
         * @param e Event data
         */
        protected void BtnMyOpen_Click(object sender, EventArgs e)
        {
            // If they're an admin, get all open tickets assigned to them
            if (this.IsAdmin())
            {
                this.GetOpenOrClosedAdminTickets("Open");
            }

            // If theyre a regular user, get all open tickets  they've submitted
            else
            {
                this.GetOpenOrClosedUserTickets("Open");
            }
        }

        /**
         * This function is called when the "My Closed Tickets" button is called. If
         * the user is an admin, it gets all closed tickets that have been assigned to them. 
         * If the user is not an admin, it gets all closed tickets that they have submitted
         * @param sender Object that contains a reference to the control/object that raised the event
         * @param e Event data
         */
        protected void BtnMyClosed_Click(object sender, EventArgs e)
        {
            // If they're an admin, get all closed tickets assigned to them
            if (this.IsAdmin())
            {
                this.GetOpenOrClosedAdminTickets("Closed");
            }

            // If they're a regular user, get all closed tickets they've submitted
            else
            {
                this.GetOpenOrClosedUserTickets("Closed");
            }
            
        }

        /**
         * This function is called when the "Unassigned Tickets" button is selected.
         * It shows all tickets from the database that are unassigned
         * @param sender Object that contains a reference to the control/object that raised the event
         * @param e Event data
         */
        protected void BtnUnassigned_Click(object sender, EventArgs e)
        {
            // Create objects
            DataAccessLayer DBmethods = new DataAccessLayer();
            DataTable unassignedTickets = DBmethods.GetUnassignedTickets();

            gvAdminTickets.DataSource = unassignedTickets;
            gvAdminTickets.DataBind();
        }

        /**
         * This function is called when the "High Priority" button is selected.
         * It shows all tickets assigned to that admin that are high priority
         * @param sender Object that contains a reference to the control/object that raised the event
         * @param e Event data
         */
        protected void BtnHigh_Click(object sender, EventArgs e)
        {
            // Create objects
            DataAccessLayer DBmethods = new DataAccessLayer();
            DataTable highPriorityTickets = DBmethods.GetSpecificPriorityTickets("High", Convert.ToString(Session["user"]));

            gvAdminTickets.DataSource = highPriorityTickets;
            gvAdminTickets.DataBind();
        }

        /**
         * This function is called when the "High Priority" button is selected.
         * It shows all tickets assigned to that admin that are high priority
         * @param sender Object that contains a reference to the control/object that raised the event
         * @param e Event data
         */
        protected void BtnMedium_Click(object sender, EventArgs e)
        {
            // Create objects
            DataAccessLayer DBmethods = new DataAccessLayer();
            DataTable mediumPriorityTickets = DBmethods.GetSpecificPriorityTickets("Medium", Convert.ToString(Session["user"]));

            gvAdminTickets.DataSource = mediumPriorityTickets;
            gvAdminTickets.DataBind();
        }

        /**
         * This function is called when the "High Priority" button is selected.
         * It shows all tickets assigned to that admin that are high priority
         * @param sender Object that contains a reference to the control/object that raised the event
         * @param e Event data
         */
        protected void BtnLow_Click(object sender, EventArgs e)
        {
            // Create objects
            DataAccessLayer DBmethods = new DataAccessLayer();
            DataTable lowPriorityTickets = DBmethods.GetSpecificPriorityTickets("Low", Convert.ToString(Session["user"]));

            gvAdminTickets.DataSource = lowPriorityTickets;
            gvAdminTickets.DataBind();
        }

        /**
         * This function checks if the user is an admin or not
         * @return Returns true if the user is an admin. Returns false if they are not
         */
        private bool IsAdmin()
        {
            // Create object for DB methods
            DataAccessLayer DBmethods = new DataAccessLayer();

            // If the user is an admin, return true
            if (DBmethods.IsAdmin(Convert.ToString(Session["user"])))
            {
                return true;
            }

            // If not, return false
            else
            {
                return false;
            }
        }

        /**
         * This function is used to get all tickets from the database
         * @return Returns a DataTable object containing all tickets
         */
        private DataTable GetAllTickets()
        {
            // Create object for DB methods and get all tickts
            DataAccessLayer DBmethods = new DataAccessLayer();
            DataTable allTickets = new DataTable();
            allTickets = DBmethods.GetAllTickets();

            // Return data set object
            return allTickets;
        }

        /**
         * This function gets all open or closed tickets from the database
         * assigned to a specific admin
         * @param ticketStatus Status of the tickets to get (open or closed)
         */
        private void GetOpenOrClosedAdminTickets(string ticketStatus)
        {
            // Create objects
            DataAccessLayer DBmethods = new DataAccessLayer();
            DataTable openOrClosedAdminTickets = new DataTable();

            // Get tickets and bind data to the grid
            openOrClosedAdminTickets = DBmethods.GetOpenOrClosedAdminTickets(ticketStatus, Convert.ToString(Session["user"]));
            gvAdminTickets.DataSource = openOrClosedAdminTickets;            
            gvAdminTickets.DataBind();

            // If ticketStatus was "closed", call function to colorize the ticket grid
            if (ticketStatus.Equals("Closed"))
            {
                this.ColorizeClosedTickets(openOrClosedAdminTickets);
            }
        }

        /**
         * This function gets all open or closed tickets from the database
         * submitted by a specific user
         * @param ticketStatus Status of the tickets to get (open or closed)
         */
        private void GetOpenOrClosedUserTickets(string ticketStatus)
        {
            // Create objects
            DataAccessLayer DBmethods = new DataAccessLayer();
            DataTable openOrClosedUserTickets = new DataTable();

            // Get tickets and bind data to the grid
            openOrClosedUserTickets = DBmethods.GetOpenOrClosedUserTickets(ticketStatus, Convert.ToString(Session["user"]));
            gvUserTickets.DataSource = openOrClosedUserTickets;
            gvUserTickets.DataBind();
        }

        /**
         * This function colorizes closed tickets based on how long they took to close.
         * Tickets that took more than 7 days to close are red. Tickets that took between
         * 3 and 7 days are yellow, and tickets that took less than 3 days are green
         * 
         * 
         * NOTE: This may not have been the best way to do it, but it is the only way that
         * I could get to work. I couldn't figure out a way using OnRowDataBound since I'm
         * already using that for something else and I didn't want to add a third ticket 
         * grid just for closed tickets, so I came up with this...
         * 
         * 
         * @param table DataTable to loop through
         */
        private void ColorizeClosedTickets(DataTable table)
        {
            // Keeps track of which row we're coloring
            int rowNumber = 0;

            // Loops through each row to check conditions
            foreach (DataRow row in table.Rows)
            {
                // Get the submission date and close date of the ticket
                DateTime ticketDate = Convert.ToDateTime(row["TicketDate"]);
                DateTime resolutionDate = Convert.ToDateTime(row["ResolutionDate"]);

                // Color the ticket text red if it took more than 7 days to close
                if ((resolutionDate - ticketDate).Days > 7)
                {
                    gvAdminTickets.Rows[rowNumber].ForeColor = System.Drawing.Color.Red;
                }

                // Color the ticket text gold (more readable than yellow) if it took between 3 and 7 days to close
                else if ((resolutionDate - ticketDate).Days < 7 && (resolutionDate - ticketDate).Days > 3)
                {
                    gvAdminTickets.Rows[rowNumber].ForeColor = System.Drawing.Color.Gold;
                }

                // Color the ticket text green if it took less than 3 days to close
                else
                {
                    gvAdminTickets.Rows[rowNumber].ForeColor = System.Drawing.Color.Green;
                }

                // Increase the counter
                rowNumber++;
            }
        }
    }
}