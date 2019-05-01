<%@ Page Title="Goodbye :(" Language="C#" MasterPageFile="~/masterPages/MasterPage.Master" AutoEventWireup="true" CodeBehind="Goodbye.aspx.cs" Inherits="Assignment9.aspxPages.Goodbye" %>
<%--
    Goodbye page shown when a user deletes their account
    for Assignment 13: The Complete PetAPuppy System
    Author: Jeffrey Trotz
    Date: 12/14/2018
    Class: CS 356
--%>
<asp:Content ID="cntGoodbye" ContentPlaceHolderID="cphPageContent" runat="server">
    <!--
        Goodbye image displayed when a user deletes their account
    -->
    <div id="divErrorImage" class="div">
        <asp:Label ID="lblGoodbye" runat="server" Text="We're sorry to see you go. Please consider coming back soon!"></asp:Label>
        <br />
        <br />
        <asp:Image ID="imgGoodbye" runat="server" CssClass="center" alt="Sad puppy" ImageUrl="/images/sadPuppy.jpg" />
    </div>
</asp:Content>