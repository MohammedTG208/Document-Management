import { DatePipe } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { UsercardserviceService } from './usercardservice.service';
import { tap } from 'rxjs';
import { Router } from '@angular/router';

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
    this.usercardservice.deleteUser(+userId).pipe(tap(this.users = this.users.filter((user: any) => user.id !== userId))).subscribe(
      {
        next: (response) => {
          console.log('User deleted successfully:', response);
        },
        error: (error) => {
          console.error('Error deleting user:', error);
        }
      }
    );
  }

  ngOnInit(): void {
    this.usercardservice.getAllUsers().subscribe({
      next: (response) => {
        this.users = response.item1;
      },
      error: (error) => {
        console.error('Error fetching users:', error);
      }
    });
  }
}
