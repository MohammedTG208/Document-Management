import { Routes } from "@angular/router";
import { LoginComponent } from "./login/login.component";
import { HomeComponent } from "./home/home.component";
import { RegisterComponent } from "./register/register.component";
import { PageNotFoundngComponent } from "./page-not-foundng/page-not-foundng.component";
import { FolderComponent } from "./folder/folder.component";
import { authGuard } from "./auth.guard";
import { FolderDialogComponent } from "./folder/folder-dialog/folder-dialog.component";
import { DocumentComponent } from "./Document/document/document.component";
import { AllfoldersComponent } from "./folder/allfolders/allfolders.component";
import { ProfileComponent } from "./User/profile/profile.component";
import { UpdateProfileComponent } from "./User/profile/update-profile/update-profile.component";
import { authadminGuard } from "./authadmin.guard";
import { UsercardComponent } from "./admin/usercard/usercard.component";

export const routes: Routes = [
  // Public Routes
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  { path: 'home', component: HomeComponent },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'folders', component: AllfoldersComponent },
  

  // Protected Routes
  // Redirect to home if no path matches
  {
    path: '',
    canActivateChild: [authGuard],
    children: [
      { path: 'folder', component: FolderComponent },
      { path: 'folder/folder-dialog', component: FolderDialogComponent },
      { path: 'mydoc/:folderId', component: DocumentComponent },
      {path:'profile', component: ProfileComponent},
      {path: 'profile/edit', component: UpdateProfileComponent}
    ]
  },

  // Admin Routes
  {
    path: 'admin',
    canActivateChild: [authadminGuard],
    children: [
      // Add admin-specific routes here
      {path: 'all/user', component: UsercardComponent},
    ]
  },

  // 404 fallback
  { path: '**', component: PageNotFoundngComponent }
];
