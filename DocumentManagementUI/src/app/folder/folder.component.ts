import { CommonModule } from '@angular/common';
import { Component, inject, signal } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { FolderService } from './folder.service';
import { switchMap } from 'rxjs/operators';
import { FolderDialogComponent } from "./folder-dialog/folder-dialog.component";
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-folder',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, FolderDialogComponent],
  templateUrl: './folder.component.html',
  styleUrl: './folder.component.css'
})
export class FolderComponent {


  private folderService = inject(FolderService);

  folderForm = new FormGroup({
    folderName: new FormControl('', Validators.required),
    isPublic: new FormControl(false)
  });

  folders = signal<any[]>([]);
  showDialog = false;
  isLoading = false;
  updateFolder = false;
  updatedFolderValue!: any;

  constructor() {
    this.getUserFolders();
  }

  getUserFolders() {
    this.folderService.getMyFolders().subscribe({
      next: (data) => {
        console.log('Data fetched:', data);
        this.folders.set(data || []);
      },
      error: (err) => {
        console.error('Error fetching folders:', err);
      }
    });
  }

  createFolder() {
    const name = this.folderForm.controls.folderName.value;
    const isPublic = this.folderForm.controls.isPublic.value;

    if (!name) return;

    this.isLoading = true;

    this.folderService.addNewFolder({ name, isPublic }).pipe(
      switchMap(() => {
        return this.folderService.getMyFolders();
      })
    ).subscribe({
      next: (data) => {
        this.folders.set(data || []);
        this.folderForm.reset();
        this.closeDialog();
        this.isLoading = false;
      },
      error: (err) => {
        this.isLoading = false;
      }
    });
  }

  deleteFolder(id: string) {
    if (!confirm('Are you sure you want to delete this folder?')) return;

    this.isLoading = true;
    console.log('Deleting folder:', id);

    this.folderService.deleteFolderById(+id).pipe(
      switchMap(() => {
        return this.folderService.getMyFolders();
      })
    ).subscribe({
      next: (data) => {
        console.log('Updated folders received:', data);
        this.folders.set(data || []);
        this.isLoading = false;
      },
      error: (err) => {
        console.error('Delete error:', err);
        this.isLoading = false;
        this.getUserFolders();
      }
    });
  }

  openDialog() {
    this.showDialog = true;
  }

  closeDialog() {
    this.showDialog = false;
  }

  onBackdropClick(event: MouseEvent) {
    this.closeDialog();
  }

  eaditFolder() {
    return this.updateFolder = false;
  }

  editFolder(folder: { id: string, status: boolean, name: string }) {
    this.updateFolder = true;
    this.updatedFolderValue = folder;
    console.log("send folder "+typeof folder.status);
  }

  onFolderUpdated() {
    this.getUserFolders();
  }

  goToDocuments(folderId:number){
    this.folderService.goToUserDoc(folderId);
  }

}
