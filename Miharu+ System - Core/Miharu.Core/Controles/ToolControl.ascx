<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ToolControl.ascx.vb" Inherits="Miharu.Core.ToolControl" %>

<asp:UpdatePanel ID="updTool" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
    <ContentTemplate>
        <table id="tblControl" runat="server">
            <tr id="trControl" runat="server">
                <td id="tdControlNew" runat="server">
                    <div id="divActionNew" class="ToolDiv" runat="server" actionname="New">
                        <asp:ImageButton ID="imgNew" runat="server" ImageUrl="~/_images/tool/new.png" CommandName="New"
                            CommandArgument="New" ValidationGroup="New" />
                        <div id="divDisabledNew" runat="server" class="ToolDivInDisabled">
                        </div>
                    </div>
                </td>
                <td id="tdControlDelete" runat="server">
                    <div id="divActionDelete" class="ToolDiv" runat="server" actionname="Delete">
                        <asp:ImageButton ID="imgDelete" runat="server" ImageUrl="~/_images/tool/delete.png"
                            CommandName="Delete" CommandArgument="Delete" ValidationGroup="Delete" />
                        <div id="divDisabledDelete" runat="server" class="ToolDivInDisabled">
                        </div>
                    </div>
                </td>
                <td id="tdControlSave" runat="server">
                    <div id="divActionSave" class="ToolDiv" runat="server" actionname="Save">
                        <asp:ImageButton ID="imgSave" runat="server" ImageUrl="~/_images/tool/save.png" CommandName="Save"
                            CommandArgument="Save" ValidationGroup="Save" />
                        <div id="divDisabledSave" runat="server" class="ToolDivInDisabled">
                        </div>
                    </div>
                </td>
                <td id="tdControlFind" runat="server">
                    <div id="divActionFind" class="ToolDiv" runat="server" actionname="Find">
                        <asp:ImageButton ID="imgFind" runat="server" ImageUrl="~/_images/tool/find.png" CommandName="Find"
                            CommandArgument="Find" ValidationGroup="NoneFind"/>
                        <div id="divDisabledFind" runat="server" class="ToolDivInDisabled">
                        </div>
                    </div>
                </td>
                <td id="tdControlEdit" runat="server">
                    <div id="divActionEdit" class="ToolDiv" runat="server" actionname="Edit">
                        <asp:ImageButton ID="imgEdit" runat="server" ImageUrl="~/_images/tool/edit.png" CommandName="Edit"
                            CommandArgument="Edit" ValidationGroup="Edit" style="visibility:hidden;"/>
                        <div id="divDisabledEdit" runat="server" class="ToolDivInDisabled">
                        </div>
                    </div>
                </td>
            </tr>
        </table>
        <div style="display: none">
            <asp:LinkButton ID="ImageCommandLinkButton" runat="server"></asp:LinkButton>
            <asp:UpdatePanel ID="EnableItemsUpdatePanel" runat="server" UpdateMode="Always">
                <ContentTemplate>
                    <asp:HiddenField ID="ImageArgumentHiddenField" runat="server" />
                    <asp:HiddenField ID="EnabledCommandsHiddenField" runat="server" />
                    <asp:HiddenField ID="FilterCommandsHiddenField" runat="server" />
                    <asp:HiddenField ID="ListCommandsHiddenField" runat="server" />
                    <asp:HiddenField ID="DetailCommandsHiddenField" runat="server" />
                    <asp:HiddenField ID="UniqueCommandsHiddenField" runat="server" />
                </ContentTemplate>
            </asp:UpdatePanel>

            <script type="text/javascript">

                var ctrId_ToolContainer = '<%= trControl.ClientID %>';
                var ctrId_EnabledCommands = '<%= EnabledCommandsHiddenField.ClientID %>';
                var ctrId_FilterCommands = '<%= FilterCommandsHiddenField.ClientID %>';
                var ctrId_ListCommands = '<%= ListCommandsHiddenField.ClientID %>';
                var ctrId_DetailCommands = '<%= DetailCommandsHiddenField.ClientID %>';
                var ctrId_UniqueCommands = '<%= UniqueCommandsHiddenField.ClientID %>';

                ConfigureTool_TabSelected(ctrId_ToolControl, 0);
            </script>

        </div>
    </ContentTemplate>
</asp:UpdatePanel>
