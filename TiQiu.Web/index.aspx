<%@ Page Language="C#" MasterPageFile="MasterPages/Content.master" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="TiQiu.Web.index" %>

<%@ Register Src="UserControl/Banner/IndexBanner.ascx" TagName="indexbanner" TagPrefix="uc1" %>
<%@ Register Src="UserControl/BookingInfosIndexControl.ascx" TagName="bookingIndex" TagPrefix="uc2" %>
<%@ Register Src="UserControl/FieldInfosIndexControl.ascx" TagName="fieldinfo" TagPrefix="uc3" %>
<%@ Register Src="UserControl/MatchInfosindexControl.ascx" TagName="matchinfo" TagPrefix="uc4" %>
<%@ Register Src="UserControl/ResearchIndexUserControl.ascx" TagName="findfieds" TagPrefix="uc5" %>
<asp:Content ID="Contetnt1" ContentPlaceHolderID="CPHead" runat="server">

</asp:Content> 
<asp:Content ID="Content2" ContentPlaceHolderID="cphmain" runat="server">
  
    
   
        <form id="form1" runat="server">
            <div id="form">
                
                <div style="width: 650px; height: 300px; float: left;margin-top:15px;margin-left:20px;">
                    <uc1:indexbanner SliderID="cameraSlider" ID="indexbanner2" runat="server" />
                </div>

                <div style="width: 290px; height: 300px; float: left;margin-top:15px;margin-right:20px;">
                    <uc5:findfieds ID="findfields" runat="server" />
                </div>
                    
                   
                <div id="content" >
                    <div id="fieldinfo" class="info-panel">
                        <uc3:fieldinfo ID="fieldsinfos" runat="server" />
                    </div>
                    <div id="match" class="info-panel">
                        <uc4:matchinfo ID="matchinfos" runat="server" />
                    </div>
                    <div id="booking" class="info-panel">
                        <uc2:bookingIndex ID="bookinginfos" runat="server" />
                    </div>
                </div>
            </div>
        </form>

</asp:Content>



