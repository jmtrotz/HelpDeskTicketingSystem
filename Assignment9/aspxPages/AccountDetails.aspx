<%@ Page Title="Account Details" Language="C#" MasterPageFile="~/masterPages/MasterPage.Master" AutoEventWireup="true" CodeBehind="AccountDetails.aspx.cs" Inherits="Assignment9.aspxPages.AccountDetails" %>
<%--
    Page to view/update account info or delete an account 
    for Assignment 13: The Complete PetAPuppy System
    Author: Jeffrey Trotz
    Date: 12/13/2018
    Class: CS 356
--%>
<asp:Content ID="cntAccountInfo" ContentPlaceHolderID="cphPageContent" runat="server">
    <!--
        Site navigation displayed on the left side of the page
    -->
    <div id="divSiteNav" class="div">
        <asp:SiteMapDataSource ID="sMapDS" Runat="server" />
        <asp:TreeView ID="tView" runat="server" DataSourceID="sMapDS" ExpandDepth="2"></asp:TreeView>
    </div> 
    <!--
       Account info box
    -->
    <div id="divAccountDetails" class="div">
        <asp:Label ID="lblUsername" runat="server" Text=""></asp:Label>
        <br />
        <asp:Label ID="lblEmailAddress" runat="server" Text=""></asp:Label>
        <br />        
        <br />
        <asp:Label ID="lblChangeEmail" runat="server" Text="Update Email Address"></asp:Label>
        <br />
        <asp:Label ID="lblNewEmail" runat="server" Text="New email address: "></asp:Label>
        <asp:TextBox ID="txtEmailAddress" runat="server" role="Text box to enter updated password"></asp:TextBox>
        <br />
        <br />
        <asp:Label ID="lblChangePassword" runat="server" Text="Change Password"></asp:Label>   
        <br />
        <asp:Label ID="lblOldPassword" runat="server" Text="Old Password: "></asp:Label>
        <asp:TextBox ID="txtOldPassword" runat="server" TextMode="Password" role="Text box to enter old password"></asp:TextBox>
        <br />
        <asp:Label ID="lblNewPassword" runat="server" Text="New Password: "></asp:Label>
        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" role="Text box to enter new password"></asp:TextBox>
        <br />
        <asp:Label ID="lblConfirmPassword" runat="server" Text="Confirm Password: "></asp:Label>
        <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" role="Text box to confirm new password"></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="btnDeleteAccount" runat="server" SkinID="reportIssueButton" CssClass="button" OnClick="BtnDeleteAccount_Click" Text="Delete Account" />
        <br />
        <br />
        <asp:Button ID="btnSubmit" runat="server" SkinID="submitButton" CssClass="button" OnClick="BtnSubmit_Click" Text="Update Account" />
        <asp:Button ID="btnReset" runat="server" SkinID="resetButton" CssClass="button" OnClick="BtnReset_Click" Text="Reset" />
    </div>
    <!--
        Box to display an error message if the user forgets to any required information
    -->
    <div id ="divAccountInfoError" runat="server" visible="false" class="div divAccountInfoError">        
        <asp:Label ID="lblErrorMessage" runat="server" CssClass="errorMessage" Text=""></asp:Label>
    </div>
    <!--
        Box to confirm that the user wants to delete their account
    -->
    <div id ="divDeleteConfirm" runat="server" visible="false" class="div divAccountInfoError">        
        <asp:Label ID="lblConfirm" runat="server" CssClass="errorMessage" Text="Are you sure you want to delete your account?"></asp:Label>
        <br />
        <asp:Button ID="btnConfirm" runat="server" SkinID="submitButton" CssClass="button" OnClick="BtnConfirm_Click" Text="Yes" />
        <asp:Button ID="btnCancel" runat="server" SkinID="resetButton" CssClass="button" OnClick="BtnReset_Click" Text="No" />
    </div>
    <!--
        Box to confirm that the user REALLY wants to delete their account
    -->
    <div id ="divDeleteDoubleConfirm" runat="server" visible="false" class="div divAccountInfoError">        
        <asp:Label ID="lblDoubleConfirm" runat="server" CssClass="errorMessage" Text="Are you REALLY sure you want to delete your account?"></asp:Label>
        <br />
        <asp:Button ID="btnDoubleConfirm" runat="server" SkinID="submitButton" CssClass="button" OnClick="BtnDoubleConfirm_Click" Text="Yes, delete me already!" />
        <asp:Button ID="btnDoubleCancel" runat="server" SkinID="resetButton" CssClass="button" OnClick="BtnReset_Click" Text="No, please don't!" />
    </div>
    <!--
        Box where the user enters their credentials to confirm and finally carry out deletion 
    -->
    <div id ="divFinalConfirm" runat="server" visible="false" class="div divAccountInfoError">        
        <asp:Label ID="lblFinalConfirm" runat="server" CssClass="errorMessage" Text="Please enter your username and password below to confirm deletion"></asp:Label>
        <br />
        <asp:Label ID="lblDeleteUsername" runat="server" Text="Username:"></asp:Label>
        <br />
        <asp:TextBox ID="txtDeleteUsername" runat="server" role="Text box to enter username"></asp:TextBox>
        <br />
        <asp:Label ID="lblDeletePassword" runat="server" Text="Password:"></asp:Label>
        <br />
        <asp:TextBox ID="txtDeletePassword" runat="server" TextMode="Password" role="Text box to enter password"></asp:TextBox>
        <br />
        <asp:Label ID="lblDeleteError" runat="server" Visible="false" Text=""></asp:Label>
        <br />
        <asp:Button ID="btnDelete" runat="server" SkinID="submitButton" CssClass="button" OnClick="BtnFinalConfirm_Click" Text="Submit" />
        <asp:Button ID="btnCancelDelete" runat="server" SkinID="resetButton" CssClass="button" OnClick="BtnReset_Click" Text="Cancel" />
    </div>
</asp:Content>