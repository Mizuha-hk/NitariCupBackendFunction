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
                
                if response.IsSuccessStatusCode then
                    let! responseBody = response.Content.ReadAsStringAsync() |> Async.AwaitTask
                    let task = JsonSerializer.Deserialize<List<ResponseData>>(responseBody)
                    return task
                else
                    return [{ Id = Guid.Empty; userId = ""; title = ""; description = ""; startDate = DateTime.MinValue; limitDate = DateTime.MinValue; createdAt = DateTime.MinValue; IsDone = false; DoneDate = DateTime.MinValue; score = 0.0 }]
            }