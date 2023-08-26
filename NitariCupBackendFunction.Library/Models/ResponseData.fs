namespace NitariCupBackendFunction.Library.Models

open System

type ResponseData = {
    Id: Guid
    userId: string
    title: string
    description: string
    startDate: DateTime
    limitDate: DateTime
    createdAt: DateTime
    IsDone: bool
    DoneDate: DateTime
    score: float
}
