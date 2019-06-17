import { Component, NgZone } from "@angular/core";
import { AuthService } from "../../services/auth.service";

import { ChatService } from "../../services/chat.service";

enum WeekDay {
  Niedziela = 0,
  Poniedzialek,
  Wtorek,
  Sroda,
  Czwartek,
  Piatek,
  Sobota
}


@Component({
  selector: "app-chat",
  templateUrl: "./chat.component.html",
  styleUrls: ["./chat.component.css"]
})
export class ChatComponent {
  chatMessage: string;
  userId: string;
  messageHistory = [];
  canSendMessage: boolean;

  constructor(
    public authService: AuthService,
    private chatService: ChatService,
    private ngZone: NgZone
  ) {
    this.subscribeToEvents();
    this.chatMessage = "";
    this.userId = authService.getUserId();
    this.chatService.getMessages().subscribe(messages => {
      this.messageHistory = messages.messages;
    });
  }

  sendMessage() {
    if (this.canSendMessage) {
      this.chatService.sendChatMessage(this.chatMessage, this.userId);

    }
  }


  private subscribeToEvents() {
    this.chatService.connectionEstablished.subscribe(() => {
      this.canSendMessage = true;
    });

    this.chatService.messageReceived.subscribe((message) => {
      this.ngZone.run(() => {
        this.chatMessage = "";
        this.messageHistory.push(message);
        console.log(this.messageHistory);
      });
    });
  }
}
