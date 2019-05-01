<%@ Page Title="Pet A Puppy" Language="C#" MasterPageFile="~/masterPages/MasterPage.Master" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="Assignment9.aspxPages.Main" %>
<%--
    Main page for Assignment 13: The Complete PetAPuppy System
    Author: Jeffrey Trotz
    Date: 12/13/2018
    Class: CS 356
--%>
<asp:Content ID="cntMainPage" ContentPlaceHolderID="cphPageContent" runat="server">
    <!--
       Site navigation displayed on the left side of the page
    -->
    <div id="divSiteNav" class="div">
        <asp:SiteMapDataSource ID="sMapDS" Runat="server" />
        <asp:TreeView ID="tView" runat="server" DataSourceID="sMapDS" ExpandDepth="2"></asp:TreeView>
    </div>  
    <!--
       Grid where ticket information is displayed
    -->
    <div id ="divTicketGrid" class="div">
        <asp:Label ID="lblWelcomeMessage" runat="server" Text=""></asp:Label>
        <br />
        <br />
        <asp:Button ID="btnAll" runat="server" SkinID="submitButton" CssClass="button" OnClick="BtnAll_Click" Text="All Tickets" />
        <asp:Button ID="btnMyTickets" runat="server" SkinID="resetButton" CssClass="button" OnClick="BtnMyTickets_Click" Text="My Tickets" />
        <asp:Button ID="btnMyOpen" runat="server" SkinID="submitButton" CssClass="button" OnClick="BtnMyOpen_Click" Text="My Open Tickets" />
        <asp:Button ID="btnMyClosed" runat="server" SkinID="resetButton" CssClass="button" OnClick="BtnMyClosed_Click" Text="My Closed Tickets" />   
        <!--
            These buttons are only visible to admins (employees)
        -->
        <asp:Button ID="btnHigh" runat="server" SkinID="submitButton" CssClass="button" OnClick="BtnHigh_Click" 
            Visible="false" Text="My High Priority Tickets" />
        <asp:Button ID="btnMedium" runat="server" SkinID="resetButton" CssClass="button" OnClick="BtnMedium_Click" 
            Visible="false" Text="My Medium Priority Tickets" />
        <asp:Button ID="btnLow" runat="server" SkinID="submitButton" CssClass="button" OnClick="BtnLow_Click" 
            Visible="false" Text="My Low Priority Tickets" />
        <asp:Button ID="btnUnassigned" runat="server" SkinID="resetButton" CssClass="button" OnClick="BtnUnassigned_Click" 
            Visible="false" Text="Unassigned Tickets" />
        <br />
        <!--
           Grid where user tickets are displayed. It has limited functionality (no update ticket buttons are shown) compared to the admin ticket grid
        -->
        <asp:GridView ID="gvUserTickets" runat="server" AutoGenerateColumns="false" ItemStyle-Wrap="true" AllowPaging="true" PageSize="7" 
            OnRowDataBound="GvTickets_RowDataBound" OnPageIndexChanging ="GvTickets_PageIndexChanging" Visible="true">
            <Columns>
                <asp:BoundField DataField="TicketNumber" HeaderText="Ticket Number" ItemStyle-Wrap="True" />
                <asp:BoundField DataField="Description" HeaderText="Description" ItemStyle-Wrap="True" />
                <asp:BoundField DataField="StepsToReproduce" HeaderText="Steps to Reproduce" ItemStyle-Wrap="True" />
                <asp:BoundField DataField="TicketDate" HeaderText="Submission Date" ItemStyle-Wrap="True" />
                <asp:BoundField DataField="AssignedTo" HeaderText="Who's Working on it" ItemStyle-Wrap="True" />
                <asp:BoundField DataField="TicketStatus" HeaderText="Status" ItemStyle-Wrap="True" />
                <asp:BoundField DataField="ResolutionDetails" HeaderText="Resolution" ItemStyle-Wrap="True" />
                <asp:BoundField DataField="ResolutionDate" HeaderText="Resolution Date" ItemStyle-Wrap="True" />
                <asp:BoundField DataField="WhoFixedIt" HeaderText="Who Fixed it" ItemStyle-Wrap="True" />
            </Columns>
        </asp:GridView>  
        <!--
           Grid where admin tickets are displayed. It has more functionality than the user ticket grid (update ticket buttons are shown) 
        -->
        <asp:GridView ID="gvAdminTickets" runat="server" AutoGenerateColumns="false" ItemStyle-Wrap="true" AllowPaging="true" PageSize="7" 
            OnRowDataBound="GvTickets_RowDataBound" OnPageIndexChanging ="GvTickets_PageIndexChanging" Visible="false">
            <Columns>
                <asp:BoundField DataField="TicketNumber" HeaderText="Ticket Number" ItemStyle-Wrap="True" />
                <asp:BoundField DataField="Description" HeaderText="Description" ItemStyle-Wrap="True" />
                <asp:BoundField DataField="StepsToReproduce" HeaderText="Steps to Reproduce" ItemStyle-Wrap="True" />
                <asp:BoundField DataField="TicketDate" HeaderText="Submission Date" ItemStyle-Wrap="True" />
                <asp:BoundField DataField="PriorityLevel" HeaderText="Priority Level" ItemStyle-Wrap="True" />
                <asp:BoundField DataField="AssignedTo" HeaderText="Who's Working on it" ItemStyle-Wrap="True" />
                <asp:BoundField DataField="TicketStatus" HeaderText="Status" ItemStyle-Wrap="True" />
                <asp:BoundField DataField="ResolutionDetails" HeaderText="Resolution" ItemStyle-Wrap="True" />
                <asp:BoundField DataField="ResolutionDate" HeaderText="Resolution Date" ItemStyle-Wrap="True" />
                <asp:BoundField DataField="WhoFixedIt" HeaderText="Who Fixed it" ItemStyle-Wrap="True" />
                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:Button ID="btnUpdate" runat="server" CausesValidation="false" UseSubmitBehavior="false" CssClass="button" 
                            CommandName="UpdateTicket" SkinID="submitButton" Text="Update Ticket" CommandArgument='<%# Eval("TicketNumber") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>