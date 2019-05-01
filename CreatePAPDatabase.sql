/*
	Author: Jeffrey Trotz
	Assignment 13: The complete PaP System
	Date: 12/13/2018
	Class: CS 356
*/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO	

-- Create database
CREATE DATABASE PetAPuppyJeffTrotz
GO

use "PetAPuppyJeffTrotz"
GO

-- Create the user table
CREATE TABLE "tblUsers"
(
	"Username" varchar (15) NOT NULL,
	"Password" varchar (135) NOT NULL, -- Field size is 135 due to SHA512 encryption generating 128 character hashes
	"EmailAddress" varchar (55) NOT NULL,
	"CreateDate" DateTime NOT NULL,
	"LastLoginDate" DateTime NOT NULL,
	CONSTRAINT "PK_tblUsers" PRIMARY KEY 
	(
		"Username"
	)
)
GO

-- Table to keep track of who is an admin so only the proper users can update tickets
CREATE TABLE "tblAdmins"
(
	"Username" varchar (15) NOT NULL,
	CONSTRAINT "PK_tblAdmins" PRIMARY KEY 
	(
		"Username"
	),
	CONSTRAINT "FK_tblAdmins" FOREIGN KEY
	(
		"Username"
	)
	REFERENCES "tblUsers"
	(
		"Username"
	)
)
GO

-- Table to store used password salts for later use
CREATE TABLE "tblUsedPasswordSalts"
(
	"PasswordSalt" varchar (15) NOT NULL,
	"Username" varchar (15) NOT NULL,
	CONSTRAINT "PK_tblUsedPasswordSalts" PRIMARY KEY 
	(
		"PasswordSalt"
	),
	CONSTRAINT "FK_tblUsedPasswordSalts" FOREIGN KEY
	(
		"Username"
	)
	REFERENCES "tblUsers"
	(
		"Username"
	)
)
GO

-- Table to store tickets
CREATE TABLE "tblTickets"
(
	"TicketNumber" int IDENTITY (1, 1) NOT NULL,	
	"EmailAddress" varchar (55) NOT NULL,
	"Description" varchar (55) NOT NULL,
	"StepsToReproduce" varchar (505) NOT NULL,
	"TicketDate" DateTime NOT NULL,
	"PriorityLevel" varchar(55) NOT NULL,
	"AssignedTo" varchar(55),
	"TicketStatus" varchar(55) NOT NULL,
	"ResolutionDetails" varchar (505),
	"ResolutionDate" DateTime,
	"WhoFixedIt" varchar (55),
	CONSTRAINT "PK_tblTickets" PRIMARY KEY 
	(
		"TicketNumber"
	)
)
GO

-- Insert users
INSERT INTO tblUsers VALUES ('jeff123', 
'a6b26b196e2b58fa540d9ccf98f23af027e79fe559fa2819c9f0fd6515e4fa1b7ba1b15f0d4a5ed4f15463cf8173cb200e1d24e55d90347abec74772e92686f8',
'jeff@jeff.com', '11/30/2018', '12/12/2018')
GO

INSERT INTO tblUsers VALUES ('admin', 
'a6b26b196e2b58fa540d9ccf98f23af027e79fe559fa2819c9f0fd6515e4fa1b7ba1b15f0d4a5ed4f15463cf8173cb200e1d24e55d90347abec74772e92686f8',
'admin@admin.com', '11/30/2018', '12/1/2018')
GO

INSERT INTO tblUsers VALUES ('intern', 
'a6b26b196e2b58fa540d9ccf98f23af027e79fe559fa2819c9f0fd6515e4fa1b7ba1b15f0d4a5ed4f15463cf8173cb200e1d24e55d90347abec74772e92686f8',
'intern@intern.com', '11/30/2018', '12/11/2018')
GO

INSERT INTO tblUsers VALUES ('jeff1234', 
'9f23e0ea4e3fb9b398e50797248c132ae26ae80f639f45e774c686d88f0f6b28c510c3b09a75932eafa5bfe6f695c8b324c7120de2fa4b69c8d05f930c1b3b67',
'jeff2@jeff2.com', '10/10/2018', '12/13/2018')
GO

INSERT INTO tblUsers VALUES ('PupLover',
'0f332603492c3b5d9b889eddb6a6ac8fd67f53fce6057b2ab7408a6cdc364f405d553a7793eb71fa57349adb542158d84b797c1c127729fd758c29c1c88ea371', 
'email@email.com', '11/11/2018', '12/11/2018')
GO

INSERT INTO tblUsers VALUES ('Ih8Catz1', 
'ae4c313de40d1d620fc872c93d6a87ca226becba4119cd5e569a3e2d937fe972bed48205dc5a173f77d860372015d073ba9b0c12f051ee87751d4b671bc334c3',
'catsRtehDevilZ@cats.com', '12/12/2012', '12/12/2018')
GO

INSERT INTO tblUsers VALUES ('StanLee15', 
'662c199a2dc0923073b8e55c06e82daf9df835985246fbb108b5db9d2490586caee20dec48aa6df5f1e43a6102ae93174ece6b1638eaf1810921b50117143430', 
'stanlee@gmail.com', '11/09/2009', '12/09/2018')
GO

INSERT INTO tblUsers VALUES ('MonkyMan',
'e82ba2e210e33e17aefe007ac49f4bc57919fe79411afda20cde57079ff754163caf7e1d16de0e53457aa7314e77b1586811c304eb6881be904b0f91eb3411c0',
'spunkyM0nkey@aol.net', '12/12/2018', '12/12/2018')
GO

-- Insert their password salts
INSERT INTO tblUsedPasswordSalts VALUES ('E7BdDQOTbGA=', 'jeff123')
GO

INSERT INTO tblUsedPasswordSalts VALUES ('5b2wBD8wHfQ=', 'admin')
GO

INSERT INTO tblUsedPasswordSalts VALUES ('6G6zg+5wxYA=', 'intern')
GO

INSERT INTO tblUsedPasswordSalts VALUES ('1hUInYq72QM=', 'jeff12345')
GO

INSERT INTO tblUsedPasswordSalts VALUES ('kRLzAdKxDiw=', 'PupLover')
GO

INSERT INTO tblUsedPasswordSalts VALUES ('Z8tnfnzHNPg=', 'Ih8Catz1')
GO

INSERT INTO tblUsedPasswordSalts VALUES ('ooFIcXxaa4g=', 'StanLee15')
GO

INSERT INTO tblUsedPasswordSalts VALUES ('AhqE2XoTQRA=', 'MonkyMan')
GO

-- Set admins
INSERT INTO tblAdmins VALUES ('jeff123')
GO

INSERT INTO tblAdmins VALUES ('admin')
GO

INSERT INTO tblAdmins VALUES ('intern')
GO

-- Insert 25 test tickets (mix of open/closed)
INSERT INTO tblTickets ("EmailAddress", "Description", "StepsToReproduce", "TicketDate", "PriorityLevel", "AssignedTo", "TicketStatus")
	VALUES ('jeff2@jeff2.com', 'Website Crash', 'Enter credentials, click login, and BOOM! Site crashes!!!!', '12/11/2018', 'Medium', 'jeff123', 'Open')
GO

INSERT INTO "tblTickets" ("EmailAddress", "Description", "StepsToReproduce", "TicketDate", "PriorityLevel", "TicketStatus")
	VALUES ('jeff2@jeff2.com', 'Cant pet my puppy :(', 'Cant pet my puppy :(', '12/13/2018', 'Low', 'Open')
GO

INSERT INTO tblTickets ("EmailAddress", "Description", "StepsToReproduce", "TicketDate", "PriorityLevel", "AssignedTo", "TicketStatus")
	VALUES ('catsRtehDevilZ@cats.com', 'Website Crash', 'Clicking buton to pet my puppy makes the site crash', '12/13/2018', 'High', 'jeff123', 'Open')
GO

INSERT INTO tblTickets ("EmailAddress", "Description", "StepsToReproduce", "TicketDate", "PriorityLevel", "AssignedTo", "TicketStatus")
	VALUES ('stanlee@gmail.com', 'Offensive Ads', 'Your website is showing me naughty ads that I do not want to see', '12/12/2018', 'Medium', 'jeff123', 'Open')
GO

INSERT INTO tblTickets ("EmailAddress", "Description", "StepsToReproduce", "TicketDate", "PriorityLevel", "AssignedTo", "TicketStatus")
	VALUES ('stanlee@gmail.com', 'Incorrect Last Login Date', 'When logging in the incorrect last login date is shown', '12/2/2018', 'Low', 'intern', 'Open')
GO

INSERT INTO tblTickets ("EmailAddress", "Description", "StepsToReproduce", "TicketDate", "PriorityLevel", "AssignedTo", "TicketStatus")
	VALUES ('spunkyM0nkey@aol.net', 'Your site sucks', 'Your site makes my eyes bleed!', '12/12/2018', 'High', 'jeff123', 'Open')
GO

INSERT INTO "tblTickets" ("EmailAddress", "Description", "StepsToReproduce", "TicketDate", "PriorityLevel", "AssignedTo", "TicketStatus")
	VALUES ('spunkyM0nkey@aol.net', 'Test Data', 'Testing 1234', '12/10/2018', 'Low', 'jeff123', 'Open')
GO

INSERT INTO tblTickets ("EmailAddress", "Description", "StepsToReproduce", "TicketDate", "PriorityLevel", "TicketStatus")
	VALUES ('south@park.com', 'Towelie', 'Dont forget to bring a towel!', '12/7/2018', 'Low', 'Open')
GO

INSERT INTO tblTickets ("EmailAddress", "Description", "StepsToReproduce", "TicketDate", "PriorityLevel", "AssignedTo", "TicketStatus")
	VALUES ('stanlee@gmail.com', 'Offensive Ads', 'Your website is showing me naughty ads that I do not want to see', '12/15/2018', 'Medium', 'intern', 'Open')
GO

INSERT INTO tblTickets ("EmailAddress", "Description", "StepsToReproduce", "TicketDate", "PriorityLevel", "AssignedTo", "TicketStatus")
	VALUES ('catsRtehDevilZ@cats.com', 'Cat picture', 'Site shows cat pictures instead of dog pictures', '12/5/2018', 'High', 'intern', 'Open')
GO

INSERT INTO tblTickets ("EmailAddress", "Description", "StepsToReproduce", "TicketDate", "PriorityLevel", "AssignedTo", "TicketStatus")
	VALUES ('jeff2@jeff2.com', 'Website Crash', 'Website crashes when trying to delete my account', '12/13/2018', 'High', 'jeff123', 'Open')
GO

INSERT INTO "tblTickets" ("EmailAddress", "Description", "StepsToReproduce", "TicketDate", "PriorityLevel", "TicketStatus")
	VALUES ('catsRtehDevilZ@cats.com', 'Dogs', 'I Dont like dogs', '12/11/2018', 'Low', 'Open')
GO

INSERT INTO tblTickets ("EmailAddress", "Description", "StepsToReproduce", "TicketDate", "PriorityLevel", "AssignedTo", "TicketStatus")
	VALUES ('spunkyM0nkey@aol.net', 'Website bugs', 'Bugs, bugs, BUGS everywhere!!!!!!', '12/12/2018', 'High', 'jeff123', 'Open')
GO

INSERT INTO tblTickets ("EmailAddress", "Description", "StepsToReproduce", "TicketDate", "PriorityLevel", "AssignedTo", "TicketStatus")
	VALUES ('stanlee@gmail.com', 'Offensive Ads', 'Your website is showing me naughty ads that I do not want to see', '12/8/2018', 'Medium', 'admin', 'Open')
GO

INSERT INTO tblTickets ("EmailAddress", "Description", "StepsToReproduce", "TicketDate", "PriorityLevel", "AssignedTo", "TicketStatus")
	VALUES ('alGore@gmail.com', 'Global Warming', 'Global warming is coming, Im super cereal!', '12/11/2018', 'Low', 'jeff123', 'Open')
GO

INSERT INTO tblTickets ("EmailAddress", "Description", "StepsToReproduce", "TicketDate", "PriorityLevel", "AssignedTo", "TicketStatus", "ResolutionDate", "ResolutionDetails", "WhoFixedIt")
	VALUES ('catsRtehDevilZ@cats.com', 'MORE PUPPIES!', 'UR SITE NEEDZ MOAR PUPPIEZZZZ!!!!', '12/13/2018', 'High', 'jeff123', 'Closed', '12/3/2018', 'Thank you for your feedback! We have added 100 new puppies!', 'jeff123')
GO

INSERT INTO "tblTickets" ("EmailAddress", "Description", "StepsToReproduce", "TicketDate", "PriorityLevel", "AssignedTo", "TicketStatus", "ResolutionDate", "ResolutionDetails", "WhoFixedIt")
	VALUES ('jeff2@jeff2.com', 'Cant pet my puppy :(', 'Cant pet my puppy :(', '12/11/2018', 'Low', 'intern', 'Closed', '12/11/2018', 'Find another website then', 'intern')
GO

INSERT INTO tblTickets ("EmailAddress", "Description", "StepsToReproduce", "TicketDate", "PriorityLevel", "AssignedTo", "TicketStatus", "ResolutionDate", "ResolutionDetails", "WhoFixedIt")
	VALUES ('spunkyM0nkey@aol.net', 'Website Crash', 'Crashes while logging out', '12/12/2018', 'High', 'jeff123', 'Closed', '12/12/2018', 'Our appologies. This was caused by our intern, who has since been fired', 'jeff123')
GO

INSERT INTO tblTickets ("EmailAddress", "Description", "StepsToReproduce", "TicketDate", "PriorityLevel", "AssignedTo", "TicketStatus", "ResolutionDate", "ResolutionDetails", "WhoFixedIt")
	VALUES ('stan@gmail.com', 'Offensive Ads', 'Your website is showing me naughty ads', '12/7/2018', 'High', 'admin', 'Closed', '12/13/2018', 'Our appologies, this has been corrected', 'admin')
GO

INSERT INTO tblTickets ("EmailAddress", "Description", "StepsToReproduce", "TicketDate", "PriorityLevel", "AssignedTo", "TicketStatus", "ResolutionDate", "ResolutionDetails", "WhoFixedIt")
	VALUES ('kenny@gmail.com', 'Test Data', 'MOAR TEST DATA', '12/12/2018', 'Low', 'intern', 'Closed', '12/13/2018', 'MOAR TEST RESOLUTIONS', 'intern')
GO

INSERT INTO tblTickets ("EmailAddress", "Description", "StepsToReproduce", "TicketDate", "PriorityLevel", "AssignedTo", "TicketStatus", "ResolutionDate", "ResolutionDetails", "WhoFixedIt")
	VALUES ('jeff2@jeff2.com', 'Ugly Puppies', 'The puppies on your site are very ugly. Find better ones!', '12/10/2018', 'High', 'jeff123', 'Closed', '12/10/2018', 'Deal with it', 'jeff123')
GO

INSERT INTO "tblTickets" ("EmailAddress", "Description", "StepsToReproduce", "TicketDate", "PriorityLevel", "AssignedTo", "TicketStatus", "ResolutionDate", "ResolutionDetails", "WhoFixedIt")
	VALUES ('spunkyM0nkey@aol.net', 'Cant change email', 'Cant update email address', '12/9/2018', 'Low', 'intern', 'Closed', '12/9/2018', 'Too bad', 'intern')
GO

INSERT INTO tblTickets ("EmailAddress", "Description", "StepsToReproduce", "TicketDate", "PriorityLevel", "AssignedTo", "TicketStatus", "ResolutionDate", "ResolutionDetails", "WhoFixedIt")
	VALUES ('catsRtehDevilZ@cats.com', 'Poor Accessability', 'My screenreader doesnt work with your site', '12/12/2018', 'High', 'jeff123', 'Closed', '12/13/2018', 'Too bad', 'jeff123')
GO

INSERT INTO tblTickets ("EmailAddress", "Description", "StepsToReproduce", "TicketDate", "PriorityLevel", "AssignedTo", "TicketStatus", "ResolutionDate", "ResolutionDetails", "WhoFixedIt")
	VALUES ('stanlee@gmail.com', 'TeSt DaTaAaAa', 'asdfghjkl;', '12/1/2018', 'Medium', 'admin', 'Closed', '12/1/2018', 'asdfghjkl;', 'admin')
GO

INSERT INTO tblTickets ("EmailAddress", "Description", "StepsToReproduce", "TicketDate", "PriorityLevel", "AssignedTo", "TicketStatus", "ResolutionDate", "ResolutionDetails", "WhoFixedIt")
	VALUES ('stanlee@gmail.com', 'asdfghjkl;', 'tEsSsTt dDaAaTtTaAaAa', '12/2/2018', 'Low', 'intern', 'Closed', '12/3/2018', 'TeEsStT rReSsOlUtIoOonNnNn', 'intern')
GO

-- Stored procedure to get all tickets assigned to an admin
-- of a certian priority based on what @PriorityLevel is
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE "uspGetSpecificPriorityTickets"
@PriorityLevel varchar (55),
@AssignedTo varchar (55)

AS
BEGIN
    SET NOCOUNT ON;
	SELECT * FROM tblTickets WHERE PriorityLevel = @PriorityLevel 
	AND AssignedTo = @AssignedTo ORDER BY TicketDate
END
GO

-- Stored procedure to get tickets that haven't been assigned to anyone yet
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE "uspGetUnassignedTickets"

AS
BEGIN
    SET NOCOUNT ON;
	SELECT * FROM tblTickets WHERE AssignedTo IS NULL ORDER BY TicketDate
END
GO

-- Stored procedure to get open or closed tickets 
-- for admins (employees) depending on what @TicketStatus is
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE "uspGetOpenOrClosedAdminTickets"
@TicketStatus varchar (55),
@AssignedTo varchar (55)

AS
BEGIN
    SET NOCOUNT ON;
	SELECT * FROM tblTickets WHERE TicketStatus = @TicketStatus 
	AND AssignedTo = @AssignedTo ORDER BY TicketDate
END
GO

-- Stored procedure to get open or closed tickets 
-- for regular users depending on what @TicketStatus is
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE "uspGetOpenOrClosedUserTickets"
@TicketStatus varchar (55),
@Username varchar (55)

AS
BEGIN
    SET NOCOUNT ON;
	SELECT * FROM tblTickets INNER JOIN tblUsers 
	ON tblTickets.EmailAddress = tblUsers.EmailAddress 
	WHERE Username = @Username AND TicketStatus = @TicketStatus 
	ORDER BY TicketDate
END
GO

-- Stored procedure to get tickets assigned to a specific admin (employee)
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE "uspGetAdminTickets"
@Username varchar(15)

AS
BEGIN
    SET NOCOUNT ON;
	SELECT * FROM tblTickets WHERE AssignedTo = @Username
	ORDER BY TicketDate
END
GO

-- Stored procedure to get tickets submitted by a specific average user
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE "uspGetUserTickets"
@Username varchar(15)

AS
BEGIN
    SET NOCOUNT ON;
	SELECT * FROM tblTickets INNER JOIN tblUsers 
	ON tblTickets.EmailAddress = tblUsers.EmailAddress 
	WHERE Username = @Username ORDER BY TicketDate
END
GO

-- Stored procedure to get ticket details to be displayed
-- to admins (employees) so they can be updated
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE "uspGetAllTickets"

AS
BEGIN
    SET NOCOUNT ON;
	SELECT * FROM tblTickets ORDER BY TicketDate
END
GO

-- Stored procedure to check if a user is an admin (employee) or just an average user
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE "uspIsAdmin"
@Username varchar(15)

AS
BEGIN
    SET NOCOUNT ON;
	SELECT COUNT(*) FROM tblAdmins WHERE Username = @Username
END
GO

-- Stored procedure to check if a username has already been used
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE "uspUsernameExists"
@Username varchar(15)

AS
BEGIN
    SET NOCOUNT ON;
	SELECT COUNT(*) FROM tblUsers WHERE Username = @Username
END
GO

-- Stored procedure to check if a user exists in the database
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE "uspUserExists"
@Username varchar(15),
@Password varchar(135)

AS
BEGIN
    SET NOCOUNT ON;
	SELECT COUNT(*) FROM tblUsers 
	WHERE Username = @Username AND "Password" = @Password
END
GO

-- Stored procedure to check if a generated password salt already exists in the databse
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE "uspSaltExists"
@PasswordSalt varchar(15)

AS
BEGIN
    SET NOCOUNT ON;
	SELECT Count(*) FROM tblUsedPasswordSalts WHERE PasswordSalt = @PasswordSalt
END
GO

-- Stored procedure to get the password salt for a user so it can be
-- encrypted with their password to verify their identity
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE "uspGetSalt"
@Username varchar(15)

AS
BEGIN
    SET NOCOUNT ON;
	SELECT PasswordSalt FROM tblUsedPasswordSalts WHERE Username = @Username
END
GO

-- Stored procedure to get a user's last login date
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE "uspGetLastLogin"
@Username varchar(15)

AS
BEGIN
    SET NOCOUNT ON;
	SELECT LastLoginDate FROM tblUsers WHERE Username = @Username
END
GO

-- Stored procedure to get a user's email address
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE "uspGetEmail"
@Username varchar(15)

AS
BEGIN
    SET NOCOUNT ON;
	SELECT EmailAddress FROM tblUsers WHERE Username = @Username
END
GO

-- Stored procedure to insert a new ticket
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE "uspInsertTicket"
@EmailAddress varchar (55),
@Description varchar (55),
@StepsToReproduce varchar (505),
@TicketDate DateTime,
@PriorityLevel varchar (55),
@TicketStatus varchar(55)

AS
BEGIN
    SET NOCOUNT ON;
	INSERT INTO tblTickets (EmailAddress,"Description", 
		StepsToReproduce, TicketDate, PriorityLevel, TicketStatus) 
	VALUES (@EmailAddress, @Description, @StepsToReproduce, 
		@TicketDate, @PriorityLevel, @TicketStatus)
END
GO

-- Stored procedure to insert a new user
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE "uspInsertUser"
@Username varchar (15),
@Password varchar (135),
@EmailAddress varchar (55),
@CreateDate DateTime,
@LastLoginDate DateTime

AS
BEGIN
    SET NOCOUNT ON;
	INSERT INTO tblUsers VALUES (@Username, @Password, @EmailAddress, @CreateDate, @LastLoginDate)
END
GO

-- Stored procedure to insert a password salt for later use
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE "uspInsertSalt"
@PasswordSalt varchar(15),
@Username varchar(15)

AS
BEGIN
    SET NOCOUNT ON;
	INSERT INTO tblUsedPasswordSalts VALUES (@PasswordSalt, @Username)
END
GO

-- Stored procedure to update a user's last login date
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE "uspUpdateLoginDate"
@LastLoginDate DateTime,
@Username varchar(15)

AS
BEGIN
    SET NOCOUNT ON;
	UPDATE tblUsers SET LastLoginDate = @LastLoginDate WHERE Username = @Username
END
GO

-- Stored procedure that will update (close) an open ticket
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE "uspUpdateTicket"
@TicketStatus varchar(55),
@ResolutionDetails varchar (505),
@ResolutionDate DateTime,
@WhoFixedIt varchar (55),
@TicketNumber varchar (55)

AS
BEGIN
    SET NOCOUNT ON;
	UPDATE tblTickets SET TicketStatus = @TicketStatus, resolutionDetails = @resolutionDetails, 
		ResolutionDate = @ResolutionDate, WhoFixedIt = @WhoFixedIt 
	WHERE TicketNumber = @TicketNumber
END
GO

-- Stored procedure that reassign a ticket to another employee
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE "uspReassignTicket"
@Username varchar (15),
@TicketNumber varchar (55)

AS
BEGIN
    SET NOCOUNT ON;
	UPDATE tblTickets SET AssignedTo = @Username WHERE TicketNumber = @TicketNumber
END
GO

-- Stored procedure to update a user's email address
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE "uspUpdateEmail"
@EmailAddress varchar(55),
@Username varchar (15)

AS
BEGIN
    SET NOCOUNT ON;
	UPDATE tblUsers SET EmailAddress = @EmailAddress WHERE Username = @Username
END
GO

-- Stored procedure to update a user's  password
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE "uspUpdatePassword"
@Password varchar(135),
@Username varchar (15)

AS
BEGIN
    SET NOCOUNT ON;
	UPDATE tblUsers SET Password = @Password WHERE Username = @Username
END
GO

-- Stored procedure that will delete a user's account
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE "uspDeleteAccount"
@Username varchar (15)

AS
BEGIN
    SET NOCOUNT ON;
	DELETE FROM tblUsers WHERE Username = @Username
END
GO

-- Stored procedure that will delete a user's password salt
CREATE PROCEDURE "uspDeleteSalt"
@Username varchar (15)

AS
BEGIN
    SET NOCOUNT ON;
	DELETE FROM tblUsedPasswordSalts WHERE Username = @Username
END
GO