import { Component, inject, OnInit, signal } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { AuthService, getToken } from '../../Auth/authService';
import { CommonModule, NgIf } from '@angular/common';

@Component({
  selector: 'app-header',
  standalone: true,
  templateUrl: './header.component.html',
  styleUrl: './header.component.css',
  imports: [RouterLink, CommonModule]
})
export class HeaderComponent implements OnInit {
  isLogin: boolean = false;
  private authService = inject(AuthService);
  isAdmin!:boolean;
  username: string='';
  private authservice = inject(AuthService);
  private router = inject(Router);

  ngOnInit(): void {
    
   this.authService.adminLoginStatus$.subscribe(status => {
      this.isAdmin = status;
    });

    // Subscribe to the login status observable to update the isLogin state
    this.authservice.loginStatus$.subscribe(status => {
      this.isLogin = status;
      if (status) {
        const userInfo = this.authservice.getUserInfo();
        this.username = userInfo?.username||'';
      } else {
        this.username = '';
      }
    });
  }

  logout() {
    this.authservice.logout();
    this.authservice.setLoginStatus(false);
    this.authservice.setAdminLoginStatus(false);
    this.router.navigate(['/login']);
  }
}
