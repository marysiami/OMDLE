import { EventEmitter, Injectable, Inject } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@aspnet/signalr';

import { BaseService } from "./base.service";
import { HttpClient, HttpParams } from "@angular/common/http";

import { ChatMessageListing } from "../models/chatmessage-listing.model";


@Injectable()
export class ChatService extends BaseService {
  messageReceived = new EventEmitter<string>();
  connectionEstablished = new EventEmitter<Boolean>();

  private connectionIsEstablished = false;
  private _hubConnection: HubConnection;

  constructor(http: HttpClient, @Inject("BASE_URL") baseUrl: string) {
    super(http, baseUrl);
    this.createConnection();
    this.registerOnServerEvents();
    this.startConnection();
  }

  getMessages() {
    let x = 0;
    let y = 10;
    const params = new HttpParams()
      .set("skip", x.toString())
      .set("take", y.toString());

    const url = this.baseUrl + "chat/GetMessages";

    return this.http.get<ChatMessageListing>(url, { params: params, headers: this.headers });
  }

  sendChatMessage(message: string, userId: string) {
    this._hubConnection.invoke('SendMessage', message, userId);
  }

  private createConnection() {
    this._hubConnection = new HubConnectionBuilder()
      .withUrl(window.location.href + 'chathub')
      .build();
  }

  private startConnection() {
    this._hubConnection
      .start()
      .then(() => {
        this.connectionIsEstablished = true;
        console.log('Hub connection started');
        this.connectionEstablished.emit(true);
      })
      .catch(err => {
        console.log('Error while establishing connection, retrying...');
        setTimeout(this.startConnection(), 5000);
      });
  }

  private registerOnServerEvents() {
    this._hubConnection.on('ReceiveMessage', (message: any) => {
      this.messageReceived.emit(JSON.parse(message));
    });
  }
}
