import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { AuthService, getHeaders } from '../Auth/authService';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class FolderService {
  private http = inject(HttpClient);
  private baseUrl = environment;
  private router=inject(Router);
  header=getHeaders();

  constructor() { }

  addNewFolder(Folder: { name: string | null; isPublic: boolean | null }) {
    const headers = getHeaders();
    return this.http.post(`${this.baseUrl.apiUrl}/api/v1/folder/add`, Folder, { headers, responseType:'text' });
  }

  getMyFolders() {
    const headers = getHeaders();

    // Add cache-busting parameter to prevent browser/proxy caching
    const params = new HttpParams().set('_t', Date.now().toString());

    return this.http.get<any[]>(`${this.baseUrl.apiUrl}/api/v1/folder/by-userId`, {
      headers,
      params
    });
  }

  deleteFolderById(folderId: number | null) {
    const headers = getHeaders();
    return this.http.delete(`${this.baseUrl.apiUrl}/api/v1/folder/delete/${folderId}`, { headers, responseType:'text' });
  }

  goToUserDoc(folderId:number){
    this.router.navigate(['/mydoc/',folderId])
  }

  getAllFolders() {
   return this.http.get<any[]>(`${this.baseUrl.apiUrl}/api/v1/folder/all`);
  }
  
}
