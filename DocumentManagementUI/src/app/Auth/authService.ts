import { HttpClient, HttpHeaders } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { environment } from "../../environments/environment";
import { AbstractControl, ValidatorFn } from "@angular/forms";
import { jwtDecode, JwtDecodeOptions } from "jwt-decode";
import { BehaviorSubject } from "rxjs";

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private http = inject(HttpClient);
  private apiUrl = environment;

  login(credentials: { userName: string | null; password: string | null }) {

    return this.http.post(this.apiUrl.apiUrl + '/api/v1/Auth/login', credentials, { responseType:'text' })
  }


  register(register: { username: string | null; password: string | null; }) {
    return this.http.post(this.apiUrl.apiUrl + '/api/v1/Auth/register', register, { responseType:'text' });
  }

  // This BehaviorSubject holds the login status of the user
  private loginStatus = new BehaviorSubject<boolean>(!!getToken());
  loginStatus$ = this.loginStatus.asObservable();

  setLoginStatus(status: boolean) {
    this.loginStatus.next(status);
  }

  logout() {
    localStorage.removeItem('token');
    this.setLoginStatus(false);
  }

  getUserInfo() {
    return getToken(); 
  }
  
}

export function ValidationTwoInput(input1: string, input2: string): ValidatorFn {
  return (group: AbstractControl) => {
    const password = group.get(input1)?.value;
    const confirmPassword = group.get(input2)?.value;

    if (password !== confirmPassword) {
      return { passwordMismatch: true };
    }

    return null;
  };
}

export function storeToken(token: string) {
  localStorage.setItem('token', token);
}

export function getToken(): { username: string; id: string; role: string; jwt: string, exp: number } | null {
  const token = localStorage.getItem('token');
  if (token) {
    const decoded: any = jwtDecode(token);
    const username = decoded["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"];
    const id = decoded["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"];
    const role = decoded["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
    const exp=decoded["http://schemas.microsoft.com/ws/2008/06/identity/claims/expired"];
    const jwt = token;

    return { username, id, role, jwt,exp };
  }
  return null;
}

export function getHeaders() {
  const token = getToken();
  if (token) {
    return new HttpHeaders({ Authorization: `Bearer ${token.jwt}` });
  }
  return new HttpHeaders();
}

export function isTokenExpired(token: string): boolean {
  if (!token) return true;

  const payload = JSON.parse(atob(token.split('.')[1]));
  const now = Math.floor(Date.now() / 1000);

  return payload.exp < now;
}

export function deleteToken() {
  localStorage.removeItem('token');
}

