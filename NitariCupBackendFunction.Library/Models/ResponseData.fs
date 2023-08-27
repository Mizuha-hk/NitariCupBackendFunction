namespace NitariCupBackendFunction.Library.Models

open System

[<CLIMutable>]
type ResponseData = {
    Id: Guid
    userId: string
    title: string
    description: string
    startDate: DateTime
    limitDate: DateTime
    createdAt: DateTime
    IsDone: Nullable<bool>
    DoneDate: Nullable<DateTime>
    score: Nullable<float>
}
