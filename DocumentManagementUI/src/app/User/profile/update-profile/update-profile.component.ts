import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Profile } from '../../../../../Models/profile';

@Component({
  selector: 'app-update-profile',
  imports: [FormsModule, CommonModule],
  templateUrl: './update-profile.component.html',
  styleUrl: './update-profile.component.css'
})
export class UpdateProfileComponent {
  showForm = false;

  toggleForm() {
    this.showForm = !this.showForm;
  }

  saveChanges() {
    // Save logic here with endpoint.
  }

  profile?: Profile;
}
