import { CommonModule } from '@angular/common';
import { Component, inject, OnInit, signal, Signal } from '@angular/core';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { FolderService, MyData } from '../folder.service';
import { routes } from '../../app.routers';
import { Router } from '@angular/router';

@Component({
  selector: 'app-allfolders',
  imports: [CommonModule, ReactiveFormsModule,FormsModule],
  templateUrl: './allfolders.component.html',
  styleUrl: './allfolders.component.css'
})
export class AllfoldersComponent implements OnInit {
  folderService = inject(FolderService);
  private routes = inject(Router);
 allFolders: any[] = [];
 filteredFolders: any[] = [];
  searchTerm: string = '';

  ngOnInit(): void {
    this.folderService.getAllFolders().subscribe({
      next: (data) => {
        this.allFolders = data;
        this.filteredFolders = data;
      },
      error: (error) => console.error('Error fetching folders:', error)
    });
  }

  onSearch() {
    const term = this.searchTerm.trim().toLowerCase();

    if (term.length === 0) {
      
      this.filteredFolders = this.allFolders;
    } else {
      
      this.filteredFolders = this.allFolders.filter(folder =>
        folder.name.toLowerCase().includes(term)
      );
    }
  }


  goToDocuments(folderId: number) {
    this.routes.navigate(['/mydoc/', folderId]);
  }

}
