namespace NitariCupBackendFunction.Library

open System
open System.Text.Json
open System.Net.Http
open NitariCupBackendFunction.Library.Models

module TaskGetter =

    let GetTask (uri: string) =
        task {
                let client = new HttpClient()
                let! response = client.GetAsync(uri) |> Async.AwaitTask
                
                let! responseBody = response.Content.ReadAsStringAsync() |> Async.AwaitTask
                if responseBody = "[]" then
                    return List<ResponseData>.Empty
                else
                    let tasks = JsonSerializer.Deserialize<List<ResponseData>>(responseBody)
                    return tasks
            }