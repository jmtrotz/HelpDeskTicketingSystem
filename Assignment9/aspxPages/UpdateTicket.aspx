<%@ Page Title="" Language="C#" MasterPageFile="~/masterPages/MasterPage.Master" AutoEventWireup="true" CodeBehind="UpdateTicket.aspx.cs" Inherits="Assignment9.aspxPages.UpdateTicket" %>
<%--
    Page to add a ticket for Assignment 13: The Complete PetAPuppy System
    Author: Jeffrey Trotz
    Date: 12/13/2018
    Class: CS 356
--%>
<asp:Content ID="cntUpdateTicketPage" ContentPlaceHolderID="cphPageContent" runat="server">
    <!--
        Site navigation displayed on the left side of the page
    -->
    <div id="divSiteNav" class="div">
        <asp:SiteMapDataSource ID="sMapDS" Runat="server" />
        <asp:TreeView ID="tView" runat="server" DataSourceID="sMapDS" ExpandDepth="2"></asp:TreeView>
    </div> 
    <!--
       Ticket information entry box
    -->
    <div id="divUpdateTicket" class="div">
        <asp:Label ID="lblStar" runat="server" CssClass="errorMessage" Text="* "></asp:Label>
        <asp:Label ID="lblRequiredFieldIndicator" runat="server" Text="Indicates a required field"></asp:Label>
        <br />
        <br />
        <asp:Label ID="lblTicketNumber" runat="server" Text=""></asp:Label>
        <br />
        <br />
        <asp:Label ID="lblStarResolutionDetails" runat="server" CssClass="errorMessage" Text="* "></asp:Label>
        <asp:Label ID="lblResolutionDetails" runat="server" Text="Issue Resolution Details"></asp:Label>
        <br />
        <asp:TextBox ID="txtResolutionDetails" runat="server" TextMode="MultiLine" Rows="10" Columns="50" CssClass="textBox" MaxLength="500" 
            role="Text box to enter issue resolution details"></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="btnUpdateTicket" runat="server" SkinID="submitButton" CssClass="button" OnClick="BtnUpdateTicket_Click" Text="Update Ticket" />
        <asp:Button ID="btnReset" runat="server" SkinID="resetButton" CssClass="button" OnClick="BtnReset_Click" Text="Reset" />
        <asp:Button ID="btnReassign" runat="server" SkinID="submitButton" CssClass="button" OnClick="BtnReassign_Click" Text="Assign to Someone Else" />
    </div>
    <!--
        Box to display an error message if the user forgets to any required information
    -->
    <div id ="divUpdateTicketError" runat="server" visible="false" class="div divUpdateTicketError">        
        <asp:Label ID="lblErrorMessage" runat="server" CssClass="errorMessage" Text=""></asp:Label>
    </div>
    <div id ="divReassignTicket" runat="server" visible="false" class="div divUpdateTicketError">
        <asp:Label ID="lblAssignUsername" runat="server" Text="Enter the username of an employee to assign the ticket to below"></asp:Label>
        <br />
        <asp:TextBox ID="txtUsername" runat="server" CssClass="textBox" MaxLength="15" role="Text box to enter employee name to reassign a ticket to them"></asp:TextBox>
        <asp:Label ID="lblAssignError" runat="server" CssClass="errorMessage" Text=""></asp:Label>
        <br />
        <asp:Button ID="btnAssign" runat="server" SkinID="submitButton" CssClass="button" OnClick="BtnConfirm_Click" Text="Assign" />
        <asp:Button ID="btnCancel" runat="server" SkinID="resetButton" CssClass="button" OnClick="BtnReset_Click" Text="Cancel" />
    </div>
</asp:Content>