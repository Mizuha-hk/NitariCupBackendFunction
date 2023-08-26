namespace NitariCupBackendFunction.Library

open System
open System.Text
open System.Text.Json
open System.Net.Http
open System.Collections.Generic
open NitariCupBackendFunction.Library.Models

module Replayer =
    
    let Replay (channelAccessToken: string ,tasks: ResponseData[]) =
        task {

            let mutable TextMessage = "直近のタスクのお知らせ.\n"
            for task in tasks do
                TextMessage <- TextMessage + task.title + "\n" + task.startDate.ToString() + "~ \n" + task.limitDate.ToString() + "\n"

            let reply = { To = tasks[0].userId; Messages = [|{ Type = "text"; Text = TextMessage}|]}

            let options = new JsonSerializerOptions()
            options.PropertyNamingPolicy <- JsonNamingPolicy.CamelCase
            let json = JsonSerializer.Serialize(reply, options)
            let content = new StringContent(json, Encoding.UTF8, "application/json")

            let client = new HttpClient()
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + channelAccessToken)
            client.PostAsync("https://api.line.me/v2/bot/message/push", content) |> Async.AwaitTask |> ignore
            return ()
        }