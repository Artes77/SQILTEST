import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { PermissionComponent } from './permission/permission.component';
import { RoleComponent } from './role/role.component';
import { UserComponent } from './user/user.component';


const appRoutes: Routes = [
  { path: 'permission', component: PermissionComponent },
  { path: 'role', component: RoleComponent },
  { path: 'user', component: UserComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(appRoutes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
