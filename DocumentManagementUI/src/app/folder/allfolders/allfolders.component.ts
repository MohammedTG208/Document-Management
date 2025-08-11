import { CommonModule } from '@angular/common';
import { Component, inject, OnInit, signal, Signal } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { FolderService } from '../folder.service';
import { routes } from '../../app.routers';
import { Router } from '@angular/router';

@Component({
  selector: 'app-allfolders',
  imports: [CommonModule,ReactiveFormsModule],
  templateUrl: './allfolders.component.html',
  styleUrl: './allfolders.component.css'
})
export class AllfoldersComponent implements OnInit {
  folderService=inject(FolderService);
  private routes = inject(Router);
  folders = signal<any[]>([]);
  searchForm = new FormGroup({
  txtSearch: new FormControl('', { validators: [Validators.pattern('^[a-zA-Z0-9 ]*$')], updateOn: 'submit' })
  });

  ngOnInit(): void {
    this.folderService.getAllFolders().subscribe({
      next: (data) => {
        this.folders.set(data);
      },
      error: (error) => {
        console.error('Error fetching folders:', error);
      },
    });
  }

  goToDocuments(folderId: number) {
    this.routes.navigate(['/mydoc/', folderId]);
  }

  onSearch() {
    throw new Error('Method not implemented.');
  }

}
