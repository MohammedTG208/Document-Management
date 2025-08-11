import { CommonModule } from '@angular/common';
import { Component, EventEmitter, inject, Input, input, OnInit, Output, signal } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MessageService } from './message.service';
import { tap } from 'rxjs';
export class MessageDto {
  id!: number;
  content!: string;
  user!: {
    id: number;
    userName: string;
    create_at: string;
  };
}

@Component({
  selector: 'app-message',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './message.component.html',
  styleUrl: './message.component.css'
})
export class MessageComponent implements OnInit {
  messages :any[]=[];
  @Input({ required: true }) documentId!: string
  @Output() close = new EventEmitter();

  private messageService = inject(MessageService);

  messageForm = new FormGroup({
    messageInput: new FormControl('', { validators: [Validators.required, Validators.minLength(3), Validators.pattern(/\S+/), Validators.maxLength(255)] })
  })

  OnSubmit() {
    this.messageService.addMessageToDucment(+this.documentId, { content: this.messageForm.controls.messageInput.value!, isPublic: true }).pipe(
      tap(() => {
       this.getMessages();
        this.messageForm.reset();
      })
    ).subscribe({
      next: (response: any[any]) => {
        this.messages=response;
      },
      error: (error) => {
        console.log(error);
      },
    })
  }

  closeDialog() {
    this.close.emit();
  }
  getMessages(){
    if (this.documentId) {
      this.messageService.getDocumentMessage(+this.documentId).subscribe({
        next: (response: any[any]) => {
          this.messages=response;
        },
        error: (error) => {
          console.log(error);
        }
      });
    }
  }
  onBackdropClick(event: MouseEvent) {
    if (event.target === event.currentTarget) {
      this.close.emit();
    }
  }

  sendSighnl() {
    this.close.emit();
  }

  ngOnInit(): void {
    this.getMessages();
  }
}
