import { inject, Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class CardService {
  private baseUrl=environment.apiUrl;
  private http=inject(HttpClient);

  getThreeFolders(){
    return this.http.get<any[any]>(`${this.baseUrl}/api/v1/Folder/first-three`);
  }
}
