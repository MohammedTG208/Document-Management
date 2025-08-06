import { Component, inject, OnInit } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { Role } from '../../../../Directive/role.drective';
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
  isAdmin: boolean = false;
  username: string='';

  private authservice = inject(AuthService);
  private router = inject(Router);

  ngOnInit(): void {
    this.authservice.loginStatus$.subscribe(status => {
      this.isLogin = status;
      if (status) {
        const userInfo = this.authservice.getUserInfo();
        this.username = userInfo?.username||'';
        this.isAdmin = userInfo?.role.includes("ADMIN") ?? false;
      } else {
        this.username = '';
        this.isAdmin = false;
      }
    });
  }

  logout() {
    this.authservice.logout();
    this.authservice.setLoginStatus(false);
    this.router.navigate(['/login']);
  }
}
