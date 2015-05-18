function ShowModalDialog(SendParas,Url,Width,Height)
{
    var obj = new Object();
    obj.Paras = SendParas;
    DialogResult = window.showModalDialog(Url, obj, 'dialogHeight:' + Height + 'px; dialogWidth:' + Width + 'px;dialogTop: px; dialogLeft: px; edge:Raised;center:Yes;help:No;resizable:No;status:No;');
		 
	if(DialogResult != null)
	{
	    return DialogResult;
	}
} 

function GetMessage()
{
    var obj = window.dialogArguments;
    return obj.Paras;
} 

function ReturnMessage(returnVal)
{
	window.returnValue = returnVal;	
	window.close();
}   
 
function CloseWindow()
{
    window.close();
}