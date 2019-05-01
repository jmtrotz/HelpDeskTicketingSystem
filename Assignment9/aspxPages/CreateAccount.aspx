<%@ Page Title="Create Account" Language="C#" MasterPageFile="~/masterPages/MasterPage.Master" AutoEventWireup="true" CodeBehind="CreateAccount.aspx.cs" Inherits="Assignment9.aspxPages.CreateAccount" %>
<%--
    Create Account page for Assignment 13: The Complete PetAPuppy System
    Author: Jeffrey Trotz
    Date: 12/13/2018
    Class: CS 356
--%>
<asp:Content ID="cntCreateAccount" ContentPlaceHolderID="cphPageContent" runat="server">
    <!--
       Information entry box for creating an account
    -->
    <div id="divCreateAccount" class="div">
        <asp:Label ID="lblStar" runat="server" CssClass="errorMessage" Text="* "></asp:Label>
        <asp:Label ID="lblRequiredFieldIndicator" runat="server" Text="Indicates a required field"></asp:Label>
        <br />
        <br />        
        <asp:Label ID="lblStarEmailAddress" runat="server" CssClass="errorMessage" Text="* "></asp:Label>
        <asp:Label ID="lblEmailAddress" runat="server" Text="Email Address"></asp:Label>
        <br />        
        <asp:TextBox ID="txtEmailAddress" runat="server" CssClass="textBox" MaxLength="50" role="Email address text entry box"></asp:TextBox>
        <br />
        <asp:Label ID="lblStarUsername" runat="server" CssClass="errorMessage" Text="* "></asp:Label>
        <asp:Label ID="lblUsername" runat="server" Text="Username"></asp:Label>
        <br />
        <asp:TextBox ID="txtUsername" runat="server" CssClass="textBox" MaxLength="15" role="Username text entry box"></asp:TextBox>
        <br />
        <asp:Label ID="lblStarPassword" runat="server" CssClass="errorMessage" Text="* "></asp:Label>
        <asp:Label ID="lblPassword" runat="server" Text="Password"></asp:Label>
        <br />
        <asp:TextBox ID="txtPassword" runat="server" CssClass="textBox" TextMode="Password" MaxLength="30" role="Password text entry box"></asp:TextBox>
        <br />
        <asp:Label ID="lblStarConfirmPassword" runat="server" CssClass="errorMessage" Text="* "></asp:Label>
        <asp:Label ID="lblConfirmPassword" runat="server" Text="Confirm Password"></asp:Label>
        <br />
        <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="textBox" TextMode="Password" MaxLength="30" role="Password comfirmation text entry box"></asp:TextBox>
        <br />
        <asp:Button ID="btnCreateAccount" runat="server" SkinID="submitButton" CssClass="button" OnClick="BtnCreateAccount_Click" Text="Create Account" />
        <asp:Button ID="btnReset" runat="server" SkinID="resetButton" CssClass="button" OnClick="BtnReset_Click" Text="Reset" />
    </div>
    <!--
        Box to display an error message if the user forgets to enter a username, password, etc
    -->
    <div id="divCreateAccountError" runat="server" class="div divCreateAccountError" visible="false">
        <asp:Label ID="lblErrorMessage" runat="server" CssClass="errorMessage" Text=""></asp:Label>
    </div>
</asp:Content>