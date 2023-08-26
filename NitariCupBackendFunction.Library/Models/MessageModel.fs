namespace NitariCupBackendFunction.Library.Models

type TextMessage = {
    Type: string
    Text: string
}

type ReplyMessage = {
    To: string
    Messages: TextMessage[]
}
