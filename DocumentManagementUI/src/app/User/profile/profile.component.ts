import { Component, inject, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ProfileService } from '../profile.service';

@Component({
  selector: 'app-profile',
  imports: [],
  standalone: true,
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.css'
})
export class ProfileComponent implements OnInit{
  profile: any[any];
  private router = inject(Router);
  private profileService = inject(ProfileService);
  goToEdit() {
    this.router.navigate(['/profile/edit']);
  }

  ngOnInit(): void {
    this.profileService.getProfile().subscribe({
      next: (data) => {
        this.profile = data;
        if (!this.profile) {
          if(confirm('You have no profile, do you want to create one?')) {
            this.router.navigate(['/profile/edit']);
          }
        }
      },
      error: (error) => {
        console.error('Error fetching profile:', error);
      }
    });
  }

}
