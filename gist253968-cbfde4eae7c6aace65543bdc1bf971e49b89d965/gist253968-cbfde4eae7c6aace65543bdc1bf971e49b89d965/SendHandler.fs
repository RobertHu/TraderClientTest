#light

namespace Comet.Chating

open Comet
open System
open System.Web

type SendHandler() =

    interface IHttpHandler with
        member h.IsReusable = false
        member h.ProcessRequest(context) = 
            let fromName = context.Request.Form.Item("from");
            let toName = context.Request.Form.Item("to")
            let msg = context.Request.Form.Item("msg")
            Chat.send fromName toName msg
            context.Response.Write "sent"