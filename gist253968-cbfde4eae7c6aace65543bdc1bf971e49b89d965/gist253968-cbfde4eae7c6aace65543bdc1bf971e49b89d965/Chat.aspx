<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Chat.aspx.cs" Inherits="CometServer.Chat" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Chat</title>
</head>
<body>
    <script language="javascript" type="text/javascript" src="Scripts/jquery-1.3.2.min.js"></script>
    <script language="javascript">
        var name = "<%= Request.QueryString["name"] %>";
        
        function sendMsg() {
            var msg = $("#msg").val();
            var toName = $("#toName").val();
            
            record("sending " + msg + "...")
            
            $.ajax({
                type: "POST",
                url: "Send.ashx",
                data: { from: name, to: toName, msg: msg },
                success: function() { $("#msg").val(""); }});
        }
        
        function receive() {
            record("receiving...");
            lastReceiving = new Date().getTime();
            $.getJSON("Receive.ashx", { name: name }, receiveCallback);
        }
        
        function receiveCallback(messages) {
            var receiveTime = new Date().getTime() - lastReceiving;
            
            if (messages.length <= 0) {
                record("received nothing (" + receiveTime + "ms)");
            }
            
            for (var i = messages.length - 1; i >= 0; i--) {
                var m = messages[i];
                record("received: " + m.text + " (" + receiveTime + "ms)");
            }
            
            receive();
        }
        
        var startTime = new Date().getTime();
        
        function record(text) {
            var time = new Date().getTime() - startTime;
            var html = $("<div></div>").text(time + " - " + text);
            $("#data").append(html);
        }
        
        $(document).ready(receive);
    </script>
    
    对方：<input type="text" value="<%= Request.QueryString["name"] %>" id="toName" disabled="disabled" /><br />
    消息：<input type="text" value="" id="msg" />
    <input type="button" value="发送" onclick="sendMsg()" />
    <div id="data"></div>
</body>
</html>