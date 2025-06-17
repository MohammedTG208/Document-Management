import { CommonModule } from '@angular/common';
import { Component,Input,output } from '@angular/core';
import { AddMessageComponent } from './add-message/add-message.component';
import { Message } from '../document-info-dialog-component/add-message/document.model';
import { DatePipe } from '@angular/common';
import { DocumentInfoDilogService } from './document-info-dialog.Service';


interface Dummy_Data {
  folderName: string;
  dateAdded: string;
  messages: Message[];
};

@Component({
  selector: 'app-document-dialog-component',
  imports: [AddMessageComponent, CommonModule, DatePipe],
  standalone: true,
  templateUrl: './document-info-dialog-component.component.html',
  styleUrl: './document-info-dialog-component.component.css'
})
export class DocumentInfoDialogComponentComponent {
  @Input() document!: Dummy_Data;
  closed = output();
  tryAdd: boolean = false;

  constructor(private documentService: DocumentInfoDilogService) { }

  onCloseClick() {
    this.closed.emit();
  }

  reseveNewMassage(message: Message) {
    this.documentService.reseveNewMassage(message, this.document);
  }

  AddMessage(): boolean {
    return this.tryAdd= this.documentService.AddMessage(this.tryAdd);
  }
  
}
