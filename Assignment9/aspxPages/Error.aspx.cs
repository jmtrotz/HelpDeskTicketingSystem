using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;

/**
 * C# code for the error page of Assignment 13: The Complete PetAPuppy System
 * Class: CS 356 
 * @author Jeffrey Trotz
 * @date 12/13/2018
 */
namespace Assignment9.aspxPages
{
    public partial class Error : System.Web.UI.Page
    {
        /**
         * This function decides whether the holiday or default theme should be applied
         * @param sender Object that contains a reference to the control/object 
         * that raised the event
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
         * @param sender Object that contains a reference to the control/object 
         * that raised the event
         * @param e Event data
         */
        protected void Page_Load(object sender, EventArgs e)
        {
            /**
             * Sets a hidden field so the JavaScript can tell if the page is
             * loaded in post back or not so the animations only play once
             */
            if (IsPostBack)
            {
                ClientScript.RegisterHiddenField("isPostBack", "True");
            }
        }
    }
}