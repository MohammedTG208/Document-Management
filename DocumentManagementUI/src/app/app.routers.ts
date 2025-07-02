import {  Routes } from "@angular/router";
import { LoginComponent } from "./login/login.component";
import { HomeComponent } from "./home/home.component";
import { RegisterComponent } from "./register/register.component";
import { ProfileComponent } from "./User/profile/profile.component";
import { UpdateProfileComponent } from "./User/profile/update-profile/update-profile.component";
import { DocumentComponent } from "./document/document.component";


export const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'profile', component: UpdateProfileComponent },
  { path: 'document', component: DocumentComponent }
];
