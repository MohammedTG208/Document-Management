import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { getHeaders, getToken } from '../../Auth/authService';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class FolderDialogService {
  private http = inject(HttpClient);
  header = getHeaders();
  baseUrl = environment.apiUrl;
  constructor() { }

  updateFolder(folderId: number, body: { name: string | null, isPublic: boolean | null }) {
    return this.http.patch(this.baseUrl + "/api/v1/Folder/update/" + folderId, body, {
      responseType:'text',
      headers: this.header
    });
  }
}
