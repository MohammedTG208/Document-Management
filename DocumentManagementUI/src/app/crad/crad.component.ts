import { Component, Input,output } from '@angular/core';
import { DocumentInfoDialogComponentComponent } from '../document-info-dialog-component/document-info-dialog-component.component';
import { DUMMY_DATA_DOCUMENT } from '../../../Dummy_Data/DUMMY_DATA_FOLDERS';
import { CommonModule } from '@angular/common';
import { DatePipe } from '@angular/common';

interface Message {
  username: string;
  dateAdded: string;
  message: string;
}
interface Dummy_Data {
  folderName: string;
  dateAdded: string;
  messages: Message[];
};

@Component({
  selector: 'app-crad',
  standalone: true,
  templateUrl: './crad.component.html',
  styleUrl: './crad.component.css',
  imports: [DocumentInfoDialogComponentComponent, CommonModule, DatePipe]
})
export class CradComponent {
  @Input() title!: string;
  @Input() date!: string;
  @Input() uploadedBy!: string;
  //@Output() cardClick = new EventEmitter();
  // here i can add any object or any data type but in this case i am using number type because // i am passing id of the card to the parent component as number type 
  cardClick = output<number>();
  dummy_data_doc = DUMMY_DATA_DOCUMENT;
  @Input() id!: number;
  selectFolderName: Dummy_Data | null = null;

  onDisplayClick() {
    this.selectFolderName = this.dummy_data_doc[this.id - 1];
  }

  onDialogClose() {
    this.selectFolderName = null;
  }
}
