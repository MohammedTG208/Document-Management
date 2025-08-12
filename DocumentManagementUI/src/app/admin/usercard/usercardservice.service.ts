import { inject, Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { AuthService, getHeaders } from '../../Auth/authService';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class UsercardserviceService {
  private baseUrl = environment.apiUrl;
  private header = getHeaders();
  private http = inject(HttpClient);

  constructor() { }

  getAllUsers(pageSize: number, pageNumber: number) {
    return this.http.get<any>(`${this.baseUrl}/api/v1/User/get/all?pageSize=${pageSize}&pageNumber=${pageNumber}`, { headers: this.header });
  }


  deleteUser(userId: number) {
    return this.http.delete<any>(`${this.baseUrl}/api/v1/User/delete/${userId}`, { headers: this.header });
  }
}
