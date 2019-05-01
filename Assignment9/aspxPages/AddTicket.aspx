<%@ Page Title="Log an Issue" Language="C#" MasterPageFile="~/masterPages/MasterPage.Master" AutoEventWireup="true" CodeBehind="AddTicket.aspx.cs" Inherits="Assignment9.aspxPages.AddTicket" %>
<%--
    Page to add a ticket for Assignment 13: The Complete PetAPuppy System
    Author: Jeffrey Trotz
    Date: 12/13/2018
    Class: CS 356
--%>
<asp:Content ID="cntAddTicket" ContentPlaceHolderID="cphPageContent" runat="server">
    <!--
       Ticket information entry box
    -->
    <div id="divAddTicket" class="div">
        <asp:Label ID="lblStar" runat="server" CssClass="errorMessage" Text="* "></asp:Label>
        <asp:Label ID="lblRequiredFieldIndicator" runat="server" Text="Indicates a required field"></asp:Label>
        <br />
        <br />        
        <asp:Label ID="lblStarEmailAddress" runat="server" CssClass="errorMessage" Text="* "></asp:Label>
        <asp:Label ID="lblEmailAddress" runat="server" Text="Email Address"></asp:Label>
        <br />        
        <asp:TextBox ID="txtEmailAddress" runat="server" CssClass="textBox" MaxLength="50" role="Email address text entry box"></asp:TextBox>
        <br />
        <asp:Label ID="lblStarDescription" runat="server" CssClass="errorMessage" Text="* "></asp:Label>
        <asp:Label ID="lblDescription" runat="server" Text="Short Issue Description"></asp:Label>
        <br />
        <asp:TextBox ID="txtDescription" runat="server" CssClass="textBox" MaxLength="50" role="Text box to enter a short issue description"></asp:TextBox>
        <br />
        <asp:Label ID="lblStarStepsToReproduce" runat="server" CssClass="errorMessage" Text="* "></asp:Label>
        <asp:Label ID="lblStepsToReproduce" runat="server" Text="Please tell us how to reproduce this issue"></asp:Label>
        <br />        
        <asp:TextBox ID="txtStepsToReproduce" runat="server" TextMode="MultiLine" Rows="10" Columns="50" CssClass="textBox" MaxLength="500" role="Text box to enter steps to reproduce the issue"></asp:TextBox>
        <br />
        <asp:Label ID="lblStarPriorityLevel" runat="server" CssClass="errorMessage" Text="* "></asp:Label>
        <asp:Label ID="lblPriorityLevel" runat="server" Text="Please choose a priority level"></asp:Label>
        <asp:DropDownList ID="ddlPriorityLevel" runat="server">
            <asp:ListItem Enabled="true" Text="Priority Level" Value=""></asp:ListItem>
            <asp:ListItem Text="High" Value="high"></asp:ListItem>
            <asp:ListItem Text="Medium" Value="medium"></asp:ListItem>
            <asp:ListItem Text="Low" Value="low"></asp:ListItem>
        </asp:DropDownList>
        <br />
        <br />
        <asp:Button ID="btnReportIssue" runat="server" SkinID="submitButton" CssClass="button" OnClick="BtnReportIssue_Click" Text="Report Issue" />
        <asp:Button ID="btnReset" runat="server" SkinID="resetButton" CssClass="button" OnClick="BtnReset_Click" Text="Reset" />
    </div>
    <!--
        Box to display an error message if the user forgets to any required information
    -->
    <div id ="divAddTicketError" runat="server" visible="false" class="div divAddTicketError">        
        <asp:Label ID="lblErrorMessage" runat="server" CssClass="errorMessage" Text=""></asp:Label>
    </div>
</asp:Content>