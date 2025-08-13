import { Component, inject, input, Input, output } from '@angular/core';
import { DocumentInfoDialogComponentComponent } from '../home/document-info-dialog-component/document-info-dialog-component.component';
import { DUMMY_DATA_DOCUMENT } from '../../../Dummy_Data/DUMMY_DATA_FOLDERS';
import { CommonModule } from '@angular/common';
import { DatePipe } from '@angular/common';
import { Router } from '@angular/router';

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
  imports: [ CommonModule, DatePipe]
})
export class CradComponent {
  @Input() folder: any;
  private router = inject(Router);
  onDisplayClick() {
    this.router.navigate(['/mydoc', this.folder.id]);
  }
}
