<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Employee.aspx.cs" Inherits="taskCrud.Employee" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Employee Management</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Employee Information</h2>
            
            <label for="txtEmpName">Employee Name:</label>
            <asp:TextBox ID="txtEmpName" runat="server"></asp:TextBox><br /><br />
            
            <label for="txtAddress">Address:</label>
            <asp:TextBox ID="txtAddress" runat="server"></asp:TextBox><br /><br />
            
            <label for="ddlReportingManager">Reporting Manager:</label>
            <asp:DropDownList ID="ddlReportingManager" runat="server"></asp:DropDownList><br /><br />
            
            <label for="txtEmpLocation">Employee Location:</label>
            <asp:TextBox ID="txtEmpLocation" runat="server"></asp:TextBox><br /><br />
            
            <label for="chkIsActive">Active:</label>
            <asp:CheckBox ID="chkIsActive" runat="server" /><br /><br />
            <br />
            
            <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
            <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click"  />
            <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click"  /><br /><br />
            
            <h2>Employee List</h2>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                <Columns>
                    <asp:BoundField DataField="EmployeeID" HeaderText="ID" />
                    <asp:BoundField DataField="EmployeeName" HeaderText="Name" />
                    <asp:BoundField DataField="Address" HeaderText="Address" />
                    <asp:BoundField DataField="RepotingManager" HeaderText="Reporting Manager" />
                    <asp:BoundField DataField="Location" HeaderText="Location" />
                    <asp:CheckBoxField DataField="IsActive" HeaderText="Active" />
                    <asp:CommandField ShowSelectButton="True" SelectText="Edit" />
                </Columns>
            </asp:GridView>
           

        </div>
    </form>
</body>
</html>
