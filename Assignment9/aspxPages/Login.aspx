<%@ Page Title="Login" Language="C#" MasterPageFile="~/masterPages/MasterPage.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Assignment9.aspxPages.Login" %>
<%--
    Login page for Assignment 13: The Complete PetAPuppy System
    Author: Jeffrey Trotz
    Date: 12/13/2018
    Class: CS 356
--%>
<asp:Content ID="cntLoginPage" ContentPlaceHolderID="cphPageContent" runat="server">
    <!--
        Santa image animated across the screen if it's the holidays
    -->
    <div id="divSanta" class="div">
        <asp:Image ID="imgSanta" runat="server" ImageUrl="~/Images/santa.png" />
    </div>
    <!--
       Shows a welcome message if the user creates a new account
    -->
    <div id="divWelcomeNewUser" runat="server" class="div divWelcomeNewUser" visible="false">
        <asp:Label ID="lblWelcome" runat="server" Text=""></asp:Label>
    </div>
    <!--
       Login information entry box
    -->
    <div id="divLogin" class="div">
        <asp:Label ID="lblUsername" runat="server" Text="Username"></asp:Label>
        <br />
        <asp:TextBox ID="txtUsername" runat="server" CssClass="textBox" MaxLength="15" role="Username text entry box"></asp:TextBox>
        <br />
        <asp:Label ID="lblPassword" runat="server" Text="Password"></asp:Label>
        <br />
        <asp:TextBox ID="txtPassword" runat="server" CssClass="textBox" TextMode="Password" MaxLength="30" role="Password text entry box"></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="btnLogin" runat="server" SkinID="submitButton" CssClass="button" OnClick="BtnLogin_Click" Text="Login" />
        <asp:Button ID="btnReset" runat="server" SkinID="resetButton" CssClass="button" OnClick="BtnReset_Click" Text="Reset" />
        <br />
        <br />
        <asp:Button ID="btnReportIssue" runat="server" SkinID="reportIssueButton" CssClass="button" OnClick="BtnReportIssue_Click" Text="Report Issue Without Creating an Account" />
        <br />
        <br />
        <asp:LinkButton ID="lBtnNewUser" runat="server" Text="New user?" OnClick="LBtnNewUser_Click"></asp:LinkButton>
        <br />
        <asp:LinkButton ID="lBtnForgotPassword" runat="server" Text="Forgot password?" OnClick="LBtnForgotPassword_Click"></asp:LinkButton>
    </div>
    <!--
        Box to display an error message if the user forgets to enter a username, password, etc
    -->
    <div id="divLoginError" runat="server" class="div divLoginError" visible="false">
        <asp:Label ID="lblErrorMessage" runat="server" CssClass="errorMessage" Text=""></asp:Label>
    </div>
</asp:Content>