import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { getHeaders } from '../Auth/authService';

@Injectable({
  providedIn: 'root'
})
export class ProfileService {
  private http=inject(HttpClient);
  private baseUrl=environment.apiUrl;
  private header=getHeaders();

  getProfile() {
    return this.http.get<any>(`${this.baseUrl}/api/v1/Profile`, {headers:this.header, responseType:'json'});
  }

  updateProfile(profile: any) {
    return this.http.patch(`${this.baseUrl}/api/v1/Profile/update`, profile, { headers: this.header, responseType: 'text' as 'json' });
  }

  addNewProfile(profile: any) {
    return this.http.post(`${this.baseUrl}/api/v1/Profile/add/new`, profile, { headers: this.header , responseType: 'text' as 'json' });
  }
}
