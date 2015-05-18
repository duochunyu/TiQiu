<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UploadImageControl.ascx.cs" Inherits="TiQiu.Web.UserControl.UploadImageControl" %>
<asp:FileUpload ID="fuImage" runat="server" /><asp:Button ID="btnUpload" runat="server" Text="上传" Style="margin-left: 5px;" OnClick="btnUpload_Click" OnClientClick="return Check();" />
<script type="text/javascript">
    function Check() {
        var selectedFile = $("#<%=fuImage.ClientID%>").val();
        if (sele.length == 0) {
            alert("请选择上传的文件");
            return false;
        }
        return true;
    }
</script>
