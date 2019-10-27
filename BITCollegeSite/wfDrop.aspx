<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="wfDrop.aspx.cs" Inherits="BITCollegeSite.wfDrop" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <asp:DetailsView ID="dvCourseInfo" runat="server" AutoGenerateRows="False" Height="126px" Width="343px" AllowPaging="True" OnPageIndexChanging="dvCourseInfo_PageIndexChanging" CellPadding="4" ForeColor="#333333" GridLines="None">
        <AlternatingRowStyle BackColor="White" />
        <CommandRowStyle BackColor="#D1DDF1" Font-Bold="True" />
        <EditRowStyle BackColor="#2461BF" />
        <FieldHeaderStyle BackColor="#DEE8F5" Font-Bold="True" />
        <Fields>
            <asp:BoundField DataField="RegistrationNumber" HeaderText="Registration" ReadOnly="True" />
            <asp:BoundField HeaderText="Student" ReadOnly="True" DataField="student.FullName" />
            <asp:BoundField HeaderText="Course" ReadOnly="True" DataField="course.Title" />
            <asp:BoundField DataField="RegistrationDate" HeaderText="Date" ReadOnly="True" DataFormatString="{0:yyyy-MM-dd}" />
            <asp:BoundField HeaderText="Grade" ReadOnly="True" DataField="Grade" DataFormatString="{0:p}" />
        </Fields>
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <PagerStyle HorizontalAlign="Center" BackColor="#2461BF" ForeColor="White" />
        <RowStyle BackColor="#EFF3FB" />
    </asp:DetailsView>
    <br />
    <asp:LinkButton ID="lbDrop" runat="server" OnClick="lbDrop_Click" Enabled="False">Drop Course</asp:LinkButton>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:LinkButton ID="lbReturn" runat="server" OnClick="lbReturn_Click">Return to Registration Listing</asp:LinkButton>
    <br />
    <br />
    <asp:Label ID="lblError" runat="server" Visible="False" ForeColor="Red" Font-Bold="True"></asp:Label>
</asp:Content>
