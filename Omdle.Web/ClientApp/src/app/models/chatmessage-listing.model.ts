import { ChatMessage } from "./chatmessage.model";

export class ChatMessageListing {
  constructor(
    public messages: ChatMessage[]) {
  }
}
