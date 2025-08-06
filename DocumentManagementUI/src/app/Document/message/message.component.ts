import { CommonModule } from '@angular/common';
import { Component, EventEmitter, inject, Input, input, Output } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MessageService } from './message.service';
import { tap } from 'rxjs';

@Component({
  selector: 'app-message',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './message.component.html',
  styleUrl: './message.component.css'
})
export class MessageComponent {
  messages: any;
 @Input({required:true}) documentId!:string
  @Output() close=new EventEmitter();

  private messageService=inject(MessageService);

  messageForm=new FormGroup({
    messageInput:new FormControl('',{validators:[Validators.required, Validators.minLength(3),Validators.pattern(/\S+/), Validators.maxLength(255)]})
  })

  OnSubmit() {
    this.messageService.addMessageToDucment(+this.documentId,{content:this.messageForm.controls.messageInput.value!,isPublic:true}).pipe(
      tap(()=>{
        this.closeDialog();
      })  
    ).subscribe({
      next:(response)=>{
        this.messages.set(response);
      },
      error:(error)=>{
        console.log(error);
      },
    })
  }  

  closeDialog(){
    this.close.emit();
  }

  onBackdropClick(event:MouseEvent){
    if(event.target===event.currentTarget){

    }
  }

  sendSighnl(){
    this.close.emit();
  }
}
