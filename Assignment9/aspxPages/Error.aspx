<%@ Page Title="Uh Oh!" Language="C#" MasterPageFile="~/masterPages/MasterPage.Master" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="Assignment9.aspxPages.Error" %>
<%--
    Error page for Assignment 13: The Complete PetAPuppy System
    Author: Jeffrey Trotz
    Date: 12/13/2018
    Class: CS 356
--%>
<asp:Content ID="cntError" ContentPlaceHolderID="cphPageContent" runat="server">
    <!--
        Error image
    -->
    <div id="divErrorImage" class="div">
        <asp:Image ID="imgError" runat="server" CssClass="center" alt="Monkey holding a wrench" ImageUrl="/images/errorMessage.png" />
    </div>
</asp:Content>