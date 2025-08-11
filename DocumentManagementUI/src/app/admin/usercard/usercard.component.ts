import { DatePipe } from '@angular/common';
import { Component } from '@angular/core';

@Component({
  selector: 'app-usercard',
  imports: [DatePipe],
  templateUrl: './usercard.component.html',
  styleUrl: './usercard.component.css'
})
export class UsercardComponent {
  users: any;
  viewUser(userId: number) {
    throw new Error('Method not implemented.');
  }
  deleteUser(arg0: any) {
    throw new Error('Method not implemented.');
  }

}
