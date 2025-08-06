import { Routes } from "@angular/router";
import { LoginComponent } from "./login/login.component";
import { HomeComponent } from "./home/home.component";
import { RegisterComponent } from "./register/register.component";
import { PageNotFoundngComponent } from "./page-not-foundng/page-not-foundng.component";
import { FolderComponent } from "./folder/folder.component";
import { authGuard } from "./auth.guard";
import { FolderDialogComponent } from "./folder/folder-dialog/folder-dialog.component";
import { DocumentComponent } from "./Document/document/document.component";



export const routes: Routes = [
  { path: '', redirectTo: '/home', pathMatch: 'full' },
  { path: 'home', component: HomeComponent },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'folder', component: FolderComponent, canActivate: [authGuard], children: [{ path: 'folder-dialog', component: FolderDialogComponent, canActivate: [authGuard] }] },
  {path:'mydoc/:folderId',component:DocumentComponent,canActivate:[authGuard]},
  { path: '**', component: PageNotFoundngComponent }
];
