<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="wfRegister.aspx.cs" Inherits="BITCollegeSite.wfRegister" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <asp:Label ID="lblStudentName" runat="server"></asp:Label>
    <br />
    <br />
    <asp:Label ID="lblCourseSelector" runat="server" Text="Course Selector:"></asp:Label>
&nbsp;<asp:DropDownList ID="ddlCourses" runat="server" OnSelectedIndexChanged="ddlCourses_SelectedIndexChanged" AutoPostBack="True">
    </asp:DropDownList>
    <br />
    <br />
    <asp:Label ID="lblNotes" runat="server" Text="Registration Notes:"></asp:Label>
&nbsp;<asp:TextBox ID="tbNotes" runat="server"></asp:TextBox>
&nbsp;<asp:RequiredFieldValidator ID="rfvRegistrationNotes" runat="server" ControlToValidate="tbNotes" ErrorMessage="Notes are Required for Web Registrations"></asp:RequiredFieldValidator>
    <br />
    <br />
    <asp:LinkButton ID="lbRegister" runat="server" OnClick="lbRegister_Click">Register</asp:LinkButton>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:LinkButton ID="lbReturn" runat="server" OnClick="lbReturn_Click" CausesValidation="False">Return to Registration Listing</asp:LinkButton>
    <br />
    <br />
    <asp:Label ID="lblError" runat="server" Visible="False" Font-Bold="True" ForeColor="Red"></asp:Label>
</asp:Content>
