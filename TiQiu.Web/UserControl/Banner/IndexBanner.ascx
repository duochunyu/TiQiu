<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="IndexBanner.ascx.cs" Inherits="TiQiu.Web.UserControl.Banner.IndexBanner" %>
      <div class="camera_wrap camera_black_skin " id="<%=this.SliderID%>">
           <% if (this.files.Count > 0)
              {
                  string virPath = ConfigurationManager.AppSettings["FileVirtualPath"];
                  files.ForEach(f =>
                      Response.Write(string.Format(@"
                    <div data-thumb='{0}' data-src='{1}'>
                        <div class='camera_caption fadeFromBottom'> {2}</div>
                    </div>", virPath + f.PATH, virPath + f.PATH, f.TITLE))
                  );
              } %>
        
        </div>
<script type="text/javascript">
    $(document).ready(function () {
        jQuery('#<%=this.SliderID%>').camera({
            height: '300px',
            thumbnails: false,
            imagePath: "../Images/",
            pagination: false
        });
    });
    </script>