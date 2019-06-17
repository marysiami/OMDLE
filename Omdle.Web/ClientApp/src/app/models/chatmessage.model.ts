export class ChatMessage {
  constructor(
    public message: string,
    public sendTime: Date,
    public messageOwner: string
    ) {
  }
}
