import { CommonModule } from '@angular/common';
import { Component, inject, OnInit, signal } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { DocumentDialogAddComponent } from "../document-dialog-add/document-dialog-add.component";
import { DocumentService } from './document.service';
import { ActivatedRoute } from '@angular/router';
import { tap } from 'rxjs';
import { MessageComponent } from "../message/message.component";

@Component({
  selector: 'app-document',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, DocumentDialogAddComponent, MessageComponent],
  templateUrl: './document.component.html',
  styleUrl: './document.component.css'
})
export class DocumentComponent implements OnInit {

  msgDialog!: boolean;
  open!: boolean;
  documents = signal<any[]>([]);
  private documentService = inject(DocumentService);
  private params = inject(ActivatedRoute);
  folderId = +this.params.snapshot.paramMap.get("folderId")!;



  openDialog() {
    this.open = true;
    console.log("clicked and value " + this.open)
  }


  downloadDocument(docId: number, docName: string) {
    this.documentService.downloadDocumentById(docId).subscribe({
      next: (blob: Blob) => {
        const url = window.URL.createObjectURL(blob);

        // Create a temporary <a> element to trigger download
        const a = document.createElement('a');
        a.href = url;

        // Optional: Set a file name
        a.download = `document-${docName}`; // Or use a real name from the response headers if available

        // Trigger the download
        a.click();

        // Cleanup
        window.URL.revokeObjectURL(url);
      },

      error: (error) => {
        console.log(error);
      },
    })
  }

  closeDialog() {
    this.getAllDocumentByFolderId();
    this.open = false;
  }


  getAllDocumentByFolderId() {
    this.documentService.getDocumentbyId(this.folderId).subscribe({
      next: (respons: any) => {
        this.documents.set(respons)
      },
      error: (errorMSG) => {
        console.log(errorMSG);
      },
    })
  }


  ngOnInit(): void {
    this.getAllDocumentByFolderId();
  }



  deleteDocument(docId: number) {
    if (confirm("Do you want to delete this Document from this file?")) {
      this.documentService
        .deleteDocument(docId)
        .pipe(tap(() => this.getAllDocumentByFolderId()))
        .subscribe();
    }
  }
  displayMessageDialog(){
    this.msgDialog=!this.msgDialog;
  }

}
