function InitDialogSelector() {

    var isJs = false;
    if (window.dialogArguments != null) {
        if (window.dialogArguments.Paras == "VendorQuery" ||
        window.dialogArguments.Paras == "ThirdpartySiteQuery" ||
        window.dialogArguments.Paras == "BrandQuery" ||
        window.dialogArguments.Paras == "ProductQuery"
        ) {
            isJs = true;
        }
    }

    $(".checkItem").click(function() {
        var returnValues = new Array($(this).attr("returnID"), $(this).attr("returnName"), $(this).attr("args"));
        if (isJs == true) {
            ReturnMessage(returnValues);
        }
        else {
            CloseCallBackDialogWithArgs(returnValues);
        }
    });
}