import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ProfileService } from '../../profile.service';

@Component({
  selector: 'app-update-profile',
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './update-profile.component.html',
  styleUrl: './update-profile.component.css'
})
export class UpdateProfileComponent{
  profile: any[any];
  private router = inject(Router);
  private profileService = inject(ProfileService);
  haveProfile: boolean = false;
  editForm = new FormGroup({
    firstName: new FormControl('', [Validators.required, Validators.pattern('^[A-Za-z]+$')]),
    lastName: new FormControl('', [Validators.required, Validators.pattern('^[A-Za-z]+$')]),
    email: new FormControl('', [Validators.required, Validators.email]),
    phoneNumber: new FormControl('', [Validators.pattern(/^05\d{8}$/),Validators.required])

  });

  constructor() {
    // Initialize the form with existing profile data if available
    this.profileService.getProfile().subscribe(profile => {
      this.profile = profile;
      if(profile){
        this.editForm.patchValue({
        firstName: profile.firstName,
        lastName: profile.lastName,
        email: profile.email,
        phoneNumber: profile.phoneNumber
      });
        this.haveProfile = true;
      }else{
        this.haveProfile = false;
      }
    });
  }

  saveProfile() {
    if(this.haveProfile){
      this.profileService.updateProfile(this.editForm.value).subscribe({
        next: (response) => {
          alert('Profile updated successfully!');
          this.router.navigate(['/profile']);
        },
        error: (error) => {
          console.error('Error updating profile:', error);
          alert('Failed to update profile. Please try again.');
        }
      });
    }else{
      this.profileService.addNewProfile(this.editForm.value).subscribe({
      next: (response) => {
        alert('Profile added successfully!');
        this.router.navigate(['/profile']);
      },
      error: (error) => {
        console.error('Error added profile:', error);
        alert('Failed to added profile. Please try again.');
      }
    });
    }
    
  }
  
  cancelEdit() {
    if (this.editForm.dirty) {
      if (confirm('You have unsaved changes. Are you sure you want to cancel?')) {
        this.router.navigate(['/profile']);
      }
    }
  }
}
