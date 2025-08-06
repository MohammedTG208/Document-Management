import { Component, EventEmitter, inject, Input, Output } from '@angular/core';
import { DocumentService } from '../document/document.service';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import { tap } from 'rxjs';

@Component({
  selector: 'app-document-dialog-add',
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './document-dialog-add.component.html',
  styleUrl: './document-dialog-add.component.css'
})
export class DocumentDialogAddComponent {

  private documentService = inject(DocumentService);
  @Output() close = new EventEmitter();
  private params = inject(ActivatedRoute);
  folderId = +this.params.snapshot.paramMap.get("folderId")!;
  fileSelected:File|null=null;

  formAdd = new FormGroup({
    file: new FormControl(null, { validators: [Validators.required] }),
    name: new FormControl('', { validators: [Validators.required, Validators.minLength(4)] })
  });
  onFileChange(event: any) {
    this.fileSelected=event.target.files[0];
  }
  OnSubmit() {
    const formFile = this.documentService.getDocValue(this.fileSelected);
    this.documentService.AddNewDocument(this.folderId,this.formAdd.controls.name.value,formFile).pipe(tap(()=>{
      this.closeDialog();
      this.documentService.getDocumentbyId(this.folderId).subscribe();
    })).subscribe({
      error:(errormsg)=>{
        console.log(errormsg)
      },
    })
  }



  closeDialog() {
    this.close.emit();
  }

  onBackdropClick(event: MouseEvent) {
    if (event.target === event.currentTarget) {
      this.closeDialog();
    }
  }



}


