using System;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;

/**
 * Data Access Layer for Assignment 13: The Complete PetAPuppy System
 * Class: CS 356 
 * @author Jeffrey Trotz
 * @date 12/13/2018
 */
namespace Assignment9.DAL
{
    public class DataAccessLayer
    {
        // Database connection shared by all methods
        private static SqlConnection databaseConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["PAPHelpDeskSQLServerConnectionString"].ConnectionString);

        /**
         * This function gets tickets of a certian priority assigned to
         * a specific admin
         * @param priorityLevel Priority level of the tickets to get (high/medium/low)
         * @username Username of the admin the ticket was assigned to
         * @return Returns a data table containing tickets of high/medium/low priority 
         * (depends on what priorityLevel was)
         */
        public DataTable GetSpecificPriorityTickets(string priorityLevel, string username)
        {
            // Create SQL command
            using (SqlCommand sqlSpecificPriorityTickets = new SqlCommand("uspGetSpecificPriorityTickets", databaseConnection))
            {
                // Set command type and add parameters
                sqlSpecificPriorityTickets.CommandType = CommandType.StoredProcedure;
                sqlSpecificPriorityTickets.Parameters.AddWithValue("@PriorityLevel", priorityLevel);
                sqlSpecificPriorityTickets.Parameters.AddWithValue("@AssignedTo", username);

                // Create data objects
                SqlDataAdapter specificPriorityTicketsAdapter = new SqlDataAdapter(sqlSpecificPriorityTickets);
                DataTable specificPriorityTickets = new DataTable();

                // Open connection, fill data table, and "clean up"
                databaseConnection.Open();
                specificPriorityTicketsAdapter.Fill(specificPriorityTickets);
                sqlSpecificPriorityTickets.Dispose();
                specificPriorityTicketsAdapter.Dispose();
                databaseConnection.Close();

                // Return results
                return specificPriorityTickets;
            }
        }

        /**
         * This function gets all tickets that are unassigned
         * @return Returns an data table containing tickets that haven't been assigned
         */
        public DataTable GetUnassignedTickets()
        {
            // Create SQL command
            using (SqlCommand sqlUnassignedTickets = new SqlCommand("uspGetUnassignedTickets", databaseConnection))
            {
                // Set command type and add parameters
                sqlUnassignedTickets.CommandType = CommandType.StoredProcedure;

                // Create data objects
                SqlDataAdapter unassignedTicketsAdapter = new SqlDataAdapter(sqlUnassignedTickets);
                DataTable unassignedTickets = new DataTable();

                // Open connection, fill data table, and "clean up"
                databaseConnection.Open();
                unassignedTicketsAdapter.Fill(unassignedTickets);
                sqlUnassignedTickets.Dispose();
                unassignedTicketsAdapter.Dispose();
                databaseConnection.Close();

                // Return results
                return unassignedTickets;
            }
        }

        /**
         * This function gets all open/closed tickets assigned to a specific admin
         * @param ticketStatus Status of the tickets to get (open or closed)
         * @username Username that the tickets are assigned to
         * @return Returns a data table containing open/closed admin tickets
         */
        public DataTable GetOpenOrClosedAdminTickets(string ticketStatus, string username)
        {
            // Create SQL command
            using (SqlCommand sqlOpenOrClosedAdminTickets = new SqlCommand("uspGetOpenOrClosedAdminTickets", databaseConnection))
            {
                // Set command type and add parameters
                sqlOpenOrClosedAdminTickets.CommandType = CommandType.StoredProcedure;
                sqlOpenOrClosedAdminTickets.Parameters.AddWithValue("@TicketStatus", ticketStatus);
                sqlOpenOrClosedAdminTickets.Parameters.AddWithValue("@AssignedTo", username);

                // Create data objects
                SqlDataAdapter openOrCosedAdminTicketsAdapter = new SqlDataAdapter(sqlOpenOrClosedAdminTickets);
                DataTable openOrCosedAdminTickets = new DataTable();

                // Open connection, fill data table, and "clean up"
                databaseConnection.Open();
                openOrCosedAdminTicketsAdapter.Fill(openOrCosedAdminTickets);
                sqlOpenOrClosedAdminTickets.Dispose();
                openOrCosedAdminTicketsAdapter.Dispose();
                databaseConnection.Close();

                // Return results
                return openOrCosedAdminTickets;
            }
        }

        /**
         * This function gets all open/closed tickets submitted by a user
         * @param ticketStatus Status of the tickets to get (open or closed)
         * @username Username of the user that submitted the ticket(s)
         * @return Returns a data table contain open or closed tickets
         */
        public DataTable GetOpenOrClosedUserTickets(string ticketStatus, string username)
        {
            // Create SQL command
            using (SqlCommand sqlOpenOrClosedUserTickets = new SqlCommand("uspGetOpenOrClosedUserTickets", databaseConnection))
            {
                // Set command type and add parameters
                sqlOpenOrClosedUserTickets.CommandType = CommandType.StoredProcedure;
                sqlOpenOrClosedUserTickets.Parameters.AddWithValue("@TicketStatus", ticketStatus);
                sqlOpenOrClosedUserTickets.Parameters.AddWithValue("@Username", username);

                // Create data objects
                SqlDataAdapter openOrCosedUserTicketsAdapter = new SqlDataAdapter(sqlOpenOrClosedUserTickets);
                DataTable openOrCosedUserTickets = new DataTable();

                // Open connection, fill data table, and "clean up"
                databaseConnection.Open();
                openOrCosedUserTicketsAdapter.Fill(openOrCosedUserTickets);
                sqlOpenOrClosedUserTickets.Dispose();
                openOrCosedUserTicketsAdapter.Dispose();
                databaseConnection.Close();

                // Return results
                return openOrCosedUserTickets;
            }
        }

        /**
         * This function gets all tickets assigned to a specific admin
         * @param username Username of the admin. Used to get tickets assigned to them
         * @return Returns a data table containing all admin tickets
         */
        public DataTable GetAdminTickets(string username)
        {
            // Create SQL command
            using (SqlCommand sqlAdminTickets = new SqlCommand("uspGetAdminTickets", databaseConnection))
            {
                // Set command type and add parameters
                sqlAdminTickets.CommandType = CommandType.StoredProcedure;
                sqlAdminTickets.Parameters.AddWithValue("@Username", username);

                // Create data objects
                SqlDataAdapter adminTicketsAdapter = new SqlDataAdapter(sqlAdminTickets);
                DataTable adminTickets = new DataTable();

                // Open connection, fill data table, and "clean up"
                databaseConnection.Open();
                adminTicketsAdapter.Fill(adminTickets);
                sqlAdminTickets.Dispose();
                adminTicketsAdapter.Dispose();
                databaseConnection.Close();

                // Return results
                return adminTickets;
            }
        }

        /**
         * This function gets all tickets from the database that match the user's
         * email address so they can see what the status of their ticket is
         * @param username Username entered by the user
         * @return Returns an data table containing all user tickets
         */
        public DataTable GetUserTickets(string username)
        {
            /*
             * The username is used to get the email address for their account. Then
             * that email address is used to get any and all tickets from the tickets
             * table associated with that email address. This is done in case the user 
             * added a ticket in the past before creating an account to check their 
             * status (an email address is required to add a ticket)
             */
            using (SqlCommand sqlUserTickets = new SqlCommand("uspGetUserTickets", databaseConnection))
            {
                // Set command type and add parameters
                sqlUserTickets.CommandType = CommandType.StoredProcedure;
                sqlUserTickets.Parameters.AddWithValue("@Username", username);
                                
                // Create data objects
                SqlDataAdapter userTicketsAdapter = new SqlDataAdapter(sqlUserTickets);
                DataTable userTickets = new DataTable();

                // Open connection, fill data table, and "clean up"
                databaseConnection.Open();
                userTicketsAdapter.Fill(userTickets);
                sqlUserTickets.Dispose();
                userTicketsAdapter.Dispose();
                databaseConnection.Close();

                // Return results
                return userTickets;
            }
        }

        /**
         * This function gets all tickets from the database so the admin can view/update them as they wish
         * @return Returns a data table containing all tickets in the database
         */
        public DataTable GetAllTickets()
        {
            // Create SQL command
            using (SqlCommand sqlAllTickets = new SqlCommand("uspGetAllTickets", databaseConnection))
            {
                // Set command type and create objects
                sqlAllTickets.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter allTicketsAdapter = new SqlDataAdapter(sqlAllTickets);
                DataTable allTickets = new DataTable();

                // Open connection, fill datatable, and "clean up"
                databaseConnection.Open();
                allTicketsAdapter.Fill(allTickets);
                sqlAllTickets.Dispose();
                allTicketsAdapter.Dispose();
                databaseConnection.Close();

                // Return results
                return allTickets;
            }
        }

        /**
         * This function checks if the user logging in is an admin or not
         * @param username Username entered by the user
         * @return Returns true if the user is an admin, false if they are not
         */
        public bool IsAdmin(string username)
        {
            // Create SQL command
            using (SqlCommand sqlIsAdmin = new SqlCommand("uspIsAdmin", databaseConnection))
            {
                // Set command type and add parameters
                sqlIsAdmin.CommandType = CommandType.StoredProcedure;
                sqlIsAdmin.Parameters.AddWithValue("@Username", username);

                // Open connection, execute query/store result, and "clean up"
                databaseConnection.Open();
                int result = Convert.ToInt16(sqlIsAdmin.ExecuteScalar());
                sqlIsAdmin.Dispose();
                databaseConnection.Close();

                // We have a problem if there's less than 0 or more than 1 record in the database...
                if ((result < 0) || (result > 1))
                {
                    HttpContext.Current.Response.Redirect("ErrorPage.aspx");
                    return false;
                }

                // Return true if a username exists
                else if (result == 1)
                {
                    return true;
                }

                // Return false if it does not
                else
                {
                    return false;
                }
            }
        }

        /**
         * This function checks if a username is already in use or not by 
         * returning the number of records in the database that match the 
         * entererd username
         * @param username Username to search for
         * @return Returns the number of records that match
         * @return Returns true if a username is taken, false if it is not
         */
        public bool UsernameExists(string username)
        {
            // Create SQL command
            using (SqlCommand sqlUsernameExists = new SqlCommand("uspUsernameExists", databaseConnection))
            {
                // Set command type and add parameters
                sqlUsernameExists.CommandType = CommandType.StoredProcedure;
                sqlUsernameExists.Parameters.AddWithValue("@Username", username);

                // Open connection, execute query/store result, and "clean up"
                databaseConnection.Open();
                int result = Convert.ToInt16(sqlUsernameExists.ExecuteScalar());
                sqlUsernameExists.Dispose();
                databaseConnection.Close();

                // We have a problem if there's less than 0 or more than 1 record in the database...
                if ((result < 0) || (result > 1))
                {
                    HttpContext.Current.Response.Redirect("ErrorPage.aspx");
                    return false;
                }

                // Return true if a username exists
                else if (result == 1)
                {
                    return true;
                }

                // Return false if it does not
                else
                {
                    return false;
                }
            }
        }

        /**
         * This function checks if the entered username/password match what's in the 
         * database by returning the number of records that match the entered information
         * @param username Username to search for
         * @param password Password to search for
         * @return Returns true if the user exists in the database, false if they do not
         */
        public bool UserExists(string username, string password)
        {
            // Create SQL command
            using (SqlCommand sqlUserExists = new SqlCommand("uspUserExists", databaseConnection))
            {
                // Use the user's username to get the salt that was used for them
                // and use it to generate the encrypted password
                string salt = this.GetSalt(username);
                string encryptedPassword = this.EncryptPassword(salt, password);

                // Set command type and add parameters
                sqlUserExists.CommandType = CommandType.StoredProcedure;
                sqlUserExists.Parameters.AddWithValue("@Username", username);
                sqlUserExists.Parameters.AddWithValue("@Password", encryptedPassword);

                // Open connection, execute query/store result, and "clean up"
                databaseConnection.Open();
                int result = Convert.ToInt16(sqlUserExists.ExecuteScalar());
                sqlUserExists.Dispose();
                databaseConnection.Close();

                // We have a problem if there's less than 0 or more than 1 record in the database...
                if ((result < 0) || (result > 1))
                {
                    HttpContext.Current.Response.Redirect("ErrorPage.aspx");
                    return false;
                }

                // If the user does exist, get their old last login date for
                // later use, update their last login date, and retrun true
                else if (result == 1)
                {
                    HttpContext.Current.Session["lastLoginDate"] = this.GetLastLogin(username);
                    this.UpdateLoginDate(username);
                    return true;
                }

                // Return false if the user does not exist
                else
                {
                    return false;
                }
            }
        }

        /*
         * This function checks to see if a salt already exists in the database 
         * or not so the same salt isn't used more than once
         * @param salt Password salt to search for
         * @return Returns true if a salt exists in the database, false if it does not
         */
        private bool SaltExists(string salt)
        {
            // Create SQL command
            using (SqlCommand sqlSaltExists = new SqlCommand("uspSaltExists", databaseConnection))
            {
                // Set command type and add parameters
                sqlSaltExists.CommandType = CommandType.StoredProcedure;
                sqlSaltExists.Parameters.AddWithValue("@PassWordSalt", salt);

                // Open connection, execute query/store result, and "clean up"
                databaseConnection.Open();
                int result = Convert.ToInt16(sqlSaltExists.ExecuteScalar());
                sqlSaltExists.Dispose();
                databaseConnection.Close();

                // We have a problem if there's less than 0 or more than 1 record in the database...
                if ((result < 0) || (result > 1))
                {
                    HttpContext.Current.Response.Redirect("ErrorPage.aspx");
                    return false;
                }

                // Return true if the salt exists
                else if (result == 1)
                {
                    return true;
                }

                // Return false if the salt does not exist
                else
                {
                    return false;
                }
            }
        }

        /*
         * This function returns the unique salt that was
         * used for a password by using the username
         * @param username Username to search for to get the password salt
         * @return Returns the password salt from the database as a String
         */
        private string GetSalt(string username)
        {
            // Create SQL command
            using (SqlCommand sqlGetSalt = new SqlCommand("uspGetSalt", databaseConnection))
            {
                // Set command type and add parameters
                sqlGetSalt.CommandType = CommandType.StoredProcedure;
                sqlGetSalt.Parameters.AddWithValue("@Username", username);

                // Open connection, execute query/store result, and "clean up"
                databaseConnection.Open();
                string passwordSalt = sqlGetSalt.ExecuteScalar().ToString();
                sqlGetSalt.Dispose();
                databaseConnection.Close();

                // We have a problem if the result is null
                if (String.IsNullOrEmpty(passwordSalt))
                {
                    HttpContext.Current.Response.Redirect("ErrorPage.aspx");
                    return "";
                }

                // Return result
                else
                {
                    return passwordSalt;
                }
            }
        }

        /**
         * This function gets the last login date of a user
         * @param username Username to get the last login date for
         * @return Returns the last login date as a string
         */
        public string GetLastLogin(string username)
        {
            // Create SQL command
            using (SqlCommand sqlGetLastLogin = new SqlCommand("uspGetLastLogin", databaseConnection))
            {
                // Set command type and add parameters
                sqlGetLastLogin.CommandType = CommandType.StoredProcedure;
                sqlGetLastLogin.Parameters.AddWithValue("@Username", username);

                // Open connection, execute query/store result, and "clean up"
                databaseConnection.Open();
                string lastLoginDate = sqlGetLastLogin.ExecuteScalar().ToString();
                sqlGetLastLogin.Dispose();
                databaseConnection.Close();

                // We have a problem if the result is null
                if (String.IsNullOrEmpty(lastLoginDate))
                {
                    HttpContext.Current.Response.Redirect("ErrorPage.aspx");
                    return "";
                }

                // Return result
                else
                {
                    return lastLoginDate;
                }
            }
        }

        /**
         * This function gets the user's email address from the database
         * @param username User's username. Used to find their email address
         * @return Returns the email address as a string
         */
        public string GetEmail(string username)
        {
            // Create SQL command
            using (SqlCommand sqlGetEmail = new SqlCommand("uspGetEmail", databaseConnection))
            {
                // Set command type and add parameters
                sqlGetEmail.CommandType = CommandType.StoredProcedure;
                sqlGetEmail.Parameters.AddWithValue("@Username", username);

                // Open connection, execute query/store result, and "clean up"
                databaseConnection.Open();
                string emailAddress = sqlGetEmail.ExecuteScalar().ToString();
                sqlGetEmail.Dispose();
                databaseConnection.Close();

                // We have a problem if the result is null
                if (String.IsNullOrEmpty(emailAddress))
                {
                    HttpContext.Current.Response.Redirect("ErrorPage.aspx");
                    return "";
                }

                // Return result
                else
                {
                    return emailAddress;
                }
            }
        }

        /**
         * This function inserts a new ticket into the database
         * @param emailAddress Email address entered by the user
         * @param description Description of the issue from the user
         * @param stepsToReproduce Steps to reproduce entered by the user
         */
        public void InsertTicket(string emailAddress, string description, string stepsToReproduce, string priorityLevel)
        {
            // Create SQL command
            using (SqlCommand sqlInsertTicket = new SqlCommand("uspInsertTicket", databaseConnection))
            {
                // Get current date, set command type, add parameters
                DateTime ticketDate = DateTime.Now;
                sqlInsertTicket.CommandType = CommandType.StoredProcedure;
                sqlInsertTicket.Parameters.AddWithValue("@EmailAddress", emailAddress);
                sqlInsertTicket.Parameters.AddWithValue("@Description", description);
                sqlInsertTicket.Parameters.AddWithValue("@StepsToReproduce", stepsToReproduce);
                sqlInsertTicket.Parameters.AddWithValue("@TicketDate", ticketDate);
                sqlInsertTicket.Parameters.AddWithValue("@PriorityLevel", priorityLevel);
                sqlInsertTicket.Parameters.AddWithValue("@TicketStatus", "Open");

                // Open connection, execute query, and "clean up"
                databaseConnection.Open();
                sqlInsertTicket.ExecuteNonQuery();
                sqlInsertTicket.Dispose();
                databaseConnection.Close();
            }
        }

        /**
         * This function inserts a new user into the database
         * @param emailAddress Email address entered by the user
         * @param username Username chosen by the user
         * @param password Password chosen by the user
         * @param CreateDate Date the account was created
         * @param LastLoginDate Date the user last logged in
         */
        public void InsertUser(string emailAddress, string username, string password)
        {
            // Create SQL command
            using (SqlCommand sqlInsertUser = new SqlCommand("uspInsertUser", databaseConnection))
            {
                // Create password salt and encrypt the user's password
                string salt = this.CreatePasswordSalt();
                string encryptedPassword = this.EncryptPassword(salt, password);

                // Get current date, set command type, and add parameters
                DateTime currentDate = DateTime.Now;
                sqlInsertUser.CommandType = CommandType.StoredProcedure;
                sqlInsertUser.Parameters.AddWithValue("@Username", username);
                sqlInsertUser.Parameters.AddWithValue("@Password", encryptedPassword);
                sqlInsertUser.Parameters.AddWithValue("@EmailAddress", emailAddress);
                sqlInsertUser.Parameters.AddWithValue("@CreateDate", currentDate);
                sqlInsertUser.Parameters.AddWithValue("@LastLoginDate", currentDate);

                // Open connection, execute query, and "clean up"
                databaseConnection.Open();
                sqlInsertUser.ExecuteNonQuery();
                sqlInsertUser.Dispose();
                databaseConnection.Close();

                // Call function to insert salt/username into another table for later use
                this.InsertSalt(salt, username);
            }
        }

        /*
         * This function inserts the salt that was generated
         * into a seperate table for later use, such as getting
         * the salt when the user wants to login again so we can
         * verify their identity
         * @param username Username entered by the user
         * @param salt Unique salt that was generated for that username
         */
        private void InsertSalt(string salt, string username)
        {
            // Create SQL command
            using (SqlCommand sqlInsertSalt = new SqlCommand("uspInsertSalt", databaseConnection))
            {
                // Set command type and add parameters
                sqlInsertSalt.CommandType = CommandType.StoredProcedure;
                sqlInsertSalt.Parameters.AddWithValue("@PasswordSalt", salt);
                sqlInsertSalt.Parameters.AddWithValue("@Username", username);

                // Open connection, execute query, and "clean up"
                databaseConnection.Open();
                sqlInsertSalt.ExecuteNonQuery();
                sqlInsertSalt.Dispose();
                databaseConnection.Close();
            }
        }

        /**
         * This function updates the LastLoginDate field in the database
         * @param username Username entered by the user
         */
        private void UpdateLoginDate(string username)
        {
            // Create SQL command
            using (SqlCommand sqlUpdateLoginDate = new SqlCommand("uspUpdateLoginDate", databaseConnection))
            {
                // Get the current date, set command type, and add parameters
                DateTime newLoginDate = DateTime.Now;                
                sqlUpdateLoginDate.CommandType = CommandType.StoredProcedure;
                sqlUpdateLoginDate.Parameters.AddWithValue("@LastLoginDate", newLoginDate);
                sqlUpdateLoginDate.Parameters.AddWithValue("@Username", username);

                // Open connection, execute query, and "clean up"
                databaseConnection.Open();
                sqlUpdateLoginDate.ExecuteNonQuery();
                sqlUpdateLoginDate.Dispose();
                databaseConnection.Close();
            }
        }

        /**
         * This function updates a ticket when it has been resolved
         * @param ticketNumber Ticket to update
         * @param resolutionDetails Details of what fixed the issue
         * @param whoFixedIt Employee who fixed the issue
         */
        public void UpdateTicket(int ticketNumber, string resolutionDetails, string whoFixedIt)
        {
            // Create SQL command
            using (SqlCommand sqlUpdateTicket = new SqlCommand("uspUpdateTicket", databaseConnection))
            {
                // Get the current date, set command type, and add parameters
                DateTime resolutionDate = DateTime.Now;
                sqlUpdateTicket.CommandType = CommandType.StoredProcedure;
                sqlUpdateTicket.Parameters.AddWithValue("@TicketStatus", "Closed");
                sqlUpdateTicket.Parameters.AddWithValue("@ResolutionDetails", resolutionDetails);
                sqlUpdateTicket.Parameters.AddWithValue("@ResolutionDate", resolutionDate);
                sqlUpdateTicket.Parameters.AddWithValue("@WhoFixedIt", whoFixedIt);
                sqlUpdateTicket.Parameters.AddWithValue("@TicketNumber", ticketNumber);

                // Open connection, execute query, and "clean up"
                databaseConnection.Open();
                sqlUpdateTicket.ExecuteNonQuery();
                sqlUpdateTicket.Dispose();
                databaseConnection.Close();
            }
        }

        public void ReassignTicket(string username, int ticketNumber)
        {
            // Create SQL command
            using (SqlCommand sqlUpdateTicket = new SqlCommand("uspReassignTicket", databaseConnection))
            {
                // Set command type and add parameters
                sqlUpdateTicket.CommandType = CommandType.StoredProcedure;
                sqlUpdateTicket.Parameters.AddWithValue("@Username", username);
                sqlUpdateTicket.Parameters.AddWithValue("@TicketNumber", ticketNumber);

                // Open connection, execute query, and "clean up"
                databaseConnection.Open();
                sqlUpdateTicket.ExecuteNonQuery();
                sqlUpdateTicket.Dispose();
                databaseConnection.Close();
            }
        }

        public void UpdateEmail(string emailAddress, string username)
        {
            // Create SQL command
            using (SqlCommand sqlUpdateEmail = new SqlCommand("uspUpdateEmail", databaseConnection))
            {
                // Set command type and add parameters
                sqlUpdateEmail.CommandType = CommandType.StoredProcedure;
                sqlUpdateEmail.Parameters.AddWithValue("@EmailAddress", emailAddress);
                sqlUpdateEmail.Parameters.AddWithValue("@Username", username);

                // Open connection, execute query, and "clean up"
                databaseConnection.Open();
                sqlUpdateEmail.ExecuteNonQuery();
                sqlUpdateEmail.Dispose();
                databaseConnection.Close();
            }
        }

        public void UpdatePassword(string password, string username)
        {
            // Create SQL command
            using (SqlCommand sqlUpdateEmail = new SqlCommand("uspUpdatePassword", databaseConnection))
            {
                // Create password salt and encrypt the user's password
                string salt = this.GetSalt(username);
                string encryptedPassword = this.EncryptPassword(salt, password);

                // Set command type and add parameters
                sqlUpdateEmail.CommandType = CommandType.StoredProcedure;
                sqlUpdateEmail.Parameters.AddWithValue("@Password", password);
                sqlUpdateEmail.Parameters.AddWithValue("@Username", username);

                // Open connection, execute query, and "clean up"
                databaseConnection.Open();
                sqlUpdateEmail.ExecuteNonQuery();
                sqlUpdateEmail.Dispose();
                databaseConnection.Close();
            }
        }

        /**
         * This function deletes a user's account from the database
         * @param username Username to delete
         */
        public void DeleteAccount(string username)
        {
            // Call function to delete their password salt
            this.DeleteSalt(username);

            // Create SQL command
            using (SqlCommand sqlDeleteAccount = new SqlCommand("uspDeleteAccount", databaseConnection))
            {
                // Set command type and add parameters
                sqlDeleteAccount.CommandType = CommandType.StoredProcedure;
                sqlDeleteAccount.Parameters.AddWithValue("@Username", username);

                // Open connection, execute query, and "clean up"
                databaseConnection.Open();
                sqlDeleteAccount.ExecuteNonQuery();
                sqlDeleteAccount.Dispose();
                databaseConnection.Close();
            }
        }

        /**
         * This function deletes a user's password salt after they
         * have deleted their account
         */
        private void DeleteSalt(string username)
        {
            // Create SQL command
            using (SqlCommand sqlDeleteSalt = new SqlCommand("uspDeleteSalt", databaseConnection))
            {
                // Set command type and add parameters
                sqlDeleteSalt.CommandType = CommandType.StoredProcedure;
                sqlDeleteSalt.Parameters.AddWithValue("@Username", username);

                // Open connection, execute query, and "clean up"
                databaseConnection.Open();
                sqlDeleteSalt.ExecuteNonQuery();
                sqlDeleteSalt.Dispose();
                databaseConnection.Close();
            }
        }

        /*
         * This function generates a salt for the user's password by generating a random number and encrypting it
         * @return Password salt that was created
         */
        private string CreatePasswordSalt()
        {
            //Generate a cryptographic random number.
            RNGCryptoServiceProvider randomNumber = new RNGCryptoServiceProvider();
            byte[] buff = new byte[8];
            randomNumber.GetBytes(buff);

            // Convert byte array to a string
            string passwordSalt = Convert.ToBase64String(buff);

            // Make sure it hasn't been used before
            if (this.SaltExists(passwordSalt))
            {
                this.CreatePasswordSalt();
            }
            
            // Return result
            return passwordSalt;
        }

        /*
         * This function adds the salt to the user's password then encrypts it using SHA512
         * @param salt Salt that was generated
         * @param password Password entered by the user
         * @return 128 character string generated by SHA512
         */
        private string EncryptPassword(string salt, string password)
        {
            // Create StringBuiklder object
            StringBuilder stringBuilder = new StringBuilder();

            // Concatinate the password and the salt
            string saltedPassword = String.Concat(salt, password);

            // Encrypt the password
            using (var hash = SHA512.Create())
            {
                Encoding encoding = Encoding.UTF8;
                Byte[] result = hash.ComputeHash(encoding.GetBytes(saltedPassword));

                foreach (Byte bite in result)
                {
                    stringBuilder.Append(bite.ToString("x2"));
                }
            }

            // Return the result
            return stringBuilder.ToString();
        }
    }
}