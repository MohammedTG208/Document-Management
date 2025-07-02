import { Component, EventEmitter, Input, output, Output } from '@angular/core';
import { Message } from './document.model';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-add-message',
  imports: [CommonModule, FormsModule],
  templateUrl: './add-message.component.html',
  styleUrl: './add-message.component.css',
  standalone: true
})
export class AddMessageComponent {
  date!: string;
  username!: string;
  message!: string;
  newMessage!: Message;
  @Output() messageFromChildToParent = new EventEmitter<Message>();
  @Input() closeMessage!: boolean;
  wrongInput!: string;

  AddNewMessageFromChild() {
    this.newMessage = {
      dateAdded: this.date,
      username: this.username,
      message: this.message
    };

    if (!this.date || !this.username.trim() || !this.message.trim()) {
      this.wrongInput = "Enter the required fields";
    } else {
      this.wrongInput = '';
      this.messageFromChildToParent.emit(this.newMessage);
      this.closeMessage = false;

     
    }
  }

  onSubmit() {
    
    // Reset fields
    this.date = '';
    this.username = '';
    this.message = '';
  }

}
