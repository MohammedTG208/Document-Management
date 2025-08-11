import { DatePipe } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { UsercardserviceService } from './usercardservice.service';

@Component({
  selector: 'app-usercard',
  imports: [DatePipe],
  templateUrl: './usercard.component.html',
  styleUrl: './usercard.component.css'
})
export class UsercardComponent implements OnInit {
  users: any;
  private usercardservice = inject(UsercardserviceService);

  deleteUser(userId: number) {
    this.usercardservice.deleteUser(+userId).subscribe(
      {
        next: (response) => {
          console.log('User deleted successfully:', response);
          this.users = this.users.filter((user: any) => user.id !== userId);
        },
        error: (error) => {
          console.error('Error deleting user:', error);
        }
      }
    );
  }

  ngOnInit(): void {
    this.usercardservice.getAllUsers().subscribe(
      (response) => {
        this.users = response.item1;
      },
      (error) => {
        console.error('Error fetching users:', error);
      }
    );
  }

}
