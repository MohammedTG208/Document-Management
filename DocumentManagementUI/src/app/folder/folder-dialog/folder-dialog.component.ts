import { Component, EventEmitter, inject, Input, Output, SimpleChanges } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { FolderDialogService } from './folder-dialog.service';
import { switchMap, tap } from 'rxjs';

@Component({
  selector: 'app-folder-dialog',
  imports: [ReactiveFormsModule],
  templateUrl: './folder-dialog.component.html',
  styleUrl: './folder-dialog.component.css'
})
export class FolderDialogComponent {

  @Output() display = new EventEmitter();
  @Output() onFolderUpdated = new EventEmitter();
  @Input() folder!: { id: string, status: boolean, name: string };
  folderUpDateDialogServise = inject(FolderDialogService);

  FolderUpdateform = new FormGroup({
    folderName: new FormControl('', { validators: [Validators.required] }),
    status: new FormControl(false),
  });

  changeArray() {
    this.onFolderUpdated.emit();
  }


  closeDialog() {
    this.display.emit();
  }

  onBackdropClick(event: MouseEvent) {
    if (event.target === event.currentTarget) {
      this.closeDialog();
    }
  }

  onSubmit() {
    if (this.FolderUpdateform.valid) {
      this.folderUpDateDialogServise.updateFolder(
        +this.folder.id,
        {
          name: this.FolderUpdateform.controls.folderName.value,
          isPublic: this.FolderUpdateform.controls.status.value

        }
      ).pipe(
        tap(() => {
          this.changeArray();
          this.closeDialog();
          console.log("this what i send " + this.FolderUpdateform.controls.folderName.value + " and " + this.FolderUpdateform.controls.status.value)
        })
      ).subscribe({
        next: () => {
          console.log('Folder updated successfully');

        },
        error: (error) => {
          console.error('Error updating folder:', error);
          // Handle error (show message to user, etc.)
        }
      });
    }
  }

  ngOnChanges(simple: SimpleChanges) {
    if (simple['folder'] && this.folder) {
      this.FolderUpdateform.patchValue({
        folderName: this.folder.name,
        status: this.folder.status,
      });
       console.log('Form value after patch:', this.FolderUpdateform.value); // Debug log
    }
  }
}