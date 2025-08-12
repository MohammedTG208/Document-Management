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
  users: any[]=[];
  private usercardservice = inject(UsercardserviceService);
  pageNumber = 1;
  pageSize = 10;
  totalPages = 1;

  deleteUser(userId: number) {
    const isApproved = confirm('Are you sure you want to delete this user?');
    if (isApproved) {
      this.usercardservice.deleteUser(userId).pipe(
        tap(() => {
          this.loadUsers();
          console.log('User deleted successfully');
        })
      ).subscribe({
        next: (response) => {
          console.log('User deleted successfully:', response);
        },
        error: (error) => {
          console.error('Error deleting user:', error);
        }
      });
    }
  }

  loadUsers() {
    this.usercardservice.getAllUsers(this.pageSize, this.pageNumber).subscribe({
      next: (response) => {
        this.users = response.item1;
        this.totalPages = response.item2.totalPageCount;
      },
      error: (error) => {
        console.error('Error fetching users:', error);
      }
    });
  }

  ngOnInit(): void {
    this.loadUsers();
  }

  nextPage() {
    if (this.pageNumber < this.totalPages) {
      this.pageNumber++;
      this.loadUsers();
    }
  }

  prevPage() {
    if (this.pageNumber > 1) {
      this.pageNumber--;
      this.loadUsers();
    }
  }
}
