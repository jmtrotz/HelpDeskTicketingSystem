/*
 * JavaScript for Assignment 13: The Complete PetAPuppy System
 * Author: Jeffrey Trotz
 * Date: 12/13/2018
 * Class: CS 356
 */
jQuery(document).ready(function ()
{
    /*
     * Variable to save if it is the first visit of the page
     * or not so the animations only play once and not every
     * time the login/reset buttons are clicked
     */
    var isPostBack = document.getElementById('isPostBack');

    /*
     * Get today's date and use it to get the current year to tack
     * onto the end of the holiday start/end dates. Then parse the
     * date to get the number of seconds since midnight 1/1/1970
     */
    var today = new Date();
    var holidayStart = Date.parse("10/31/" + today.getFullYear().toString());
    var holidayEnd = Date.parse("12/30/" + today.getFullYear().toString());

    /*
     * If the page is not loaded in post back from a button click,
     * animate the logo, header, footer, and login box to their 
     * respective positions when the page is first loaded
     */
    if (isPostBack == null)
    {
        jQuery('#divAccountDetails').animate({ top: '25%' }, 1000);
        jQuery('#divAddTicket').animate({ top: '20%' }, 1000);
        jQuery('#divCreateAccount').animate({ top: '20%' }, 1000);
        jQuery('#divFooter').animate({ top: '95%' }, 1000);
        jQuery('#divHeader').animate({ left: '0.5%' }, 1000);
        jQuery('#divLogin').animate({ top: '30%' }, 1000);
        jQuery('#divLogo').animate({ right: '84%' }, 1000);
        jQuery('#divSiteNav').animate({ left: '0.5%' }, 1000);
        jQuery('#divTicketGrid').animate({ top: '17%' }, 1000);
        jQuery('#divUpdateTicket').animate({ top: '25%' }, 1000);

        /*
         * Convert todays date to the number of seconds since midnight
         * 1/1/1970 using getTime() to compare to the holiday start/end
         * times. If today's date is between the holiday dates then animate 
         * an image of santa across the screen
         */
        if ((today.getTime() > holidayStart) && (today.getTime() < holidayEnd))
        {
            jQuery('#divSanta').animate({ right: '-100%' }, 7000);
        }
    }

    /*
     * If the page is loaded in post back from a button click,
     * set the CSS attributes for the logo, header, footer, and
     * login box so they stay where they are rather than animate
     * again
     */
    else
    {
        jQuery('#divAccountDetails').css({ top: '25%' });
        jQuery('#divAddTicket').css({ top: '20%' });
        jQuery('#divCreateAccount').css({ top: '20%' });
        jQuery('#divFooter').css({ top: '95%' });
        jQuery('#divHeader').css({ left: '0.5%' });
        jQuery('#divLogin').css({ top: '30%' });
        jQuery('#divLogo').css({ right: '84%' });
        jQuery('#divSiteNav').css({ left: '0.5%' });
        jQuery('#divTicketGrid').css({ top: '17%' });
        jQuery('#divUpdateTicket').css({ top: '20%' });
        jQuery('.divSanta').css({ right: '-100%' });
    }
});