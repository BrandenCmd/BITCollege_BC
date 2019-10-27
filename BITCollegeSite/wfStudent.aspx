<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="wfStudent.aspx.cs" Inherits="BITCollegeSite.wfStudent" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <asp:Label ID="lblStudentName" runat="server"></asp:Label>
    <br />
    <br />
    <asp:GridView ID="gvCourseData" runat="server" AutoGenerateColumns="False" Height="159px" OnSelectedIndexChanged="gvCourseData_SelectedIndexChanged" Width="768px" AutoGenerateSelectButton="True" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4">
        <Columns>
            <asp:BoundField DataField="CourseNumber" HeaderText="Course" ReadOnly="True" />
            <asp:BoundField DataField="Title" HeaderText="Title" ReadOnly="True" />
            <asp:BoundField DataField="CreditHours" HeaderText="Credit Hours" ReadOnly="True" >
            <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="CourseType" HeaderText="Course Type" ReadOnly="True" />
            <asp:BoundField DataField="TuitionAmount" HeaderText="Tuition" ReadOnly="True" DataFormatString="{0:c}" >
            <ItemStyle HorizontalAlign="Right" />
            </asp:BoundField>
        </Columns>
    </asp:GridView>
    <br />
    <asp:LinkButton ID="LinkButton1" runat="server" TabIndex="1" OnClick="LinkButton1_Click">Click Here To Register For A Course</asp:LinkButton>
    <br />
    <br />
    <asp:Label ID="lblError" runat="server" Visible="False" ForeColor="Red" Font-Bold="True"></asp:Label>
    <br />
    </asp:Content>
