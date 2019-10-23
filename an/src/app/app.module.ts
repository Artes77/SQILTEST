import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { NgxsModule } from '@ngxs/store'



import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { UserComponent } from './user/user.component';
import { RoleComponent } from './role/role.component';
import { PermissionComponent } from './permission/permission.component';
import { UserService } from './shared/service/user.service';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { PermissionService } from './shared/service/permission.service';
import { RoleService } from './shared/service/role.service';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RoleUpdateDialogComponent } from './role/role-update-dialog.component';
import { RoleDeleteDialogComponent } from './role/role-delete-dialog.component';
import { RoleCreateDialogComponent } from './role/role-create-dialog.component';
import { ModalModule, PaginationModule } from 'ngx-bootstrap';
import { PermissionCreateDialogComponent } from './permission/permission-create-dialog.component';
import { PermissionDeleteDialogComponent } from './permission/permission-delete-dialog.component';
import { PermissionUpdateDialogComponent } from './permission/permission-update-dialog.component';
import { UserCreateDialogComponent } from './user/user-create-dialog.component';
import { UserDeleteDialogComponent } from './user/user-delete-dialog.component';
import { UserUpdateDialogComponent } from './user/user-update-dialog.component';
import { ToastrModule } from 'ngx-toastr';
import { PermissionState } from './permission/permission.state';
import { RoleState } from './role/role.state';
import { UserState } from './user/user.state';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatDialogModule } from '@angular/material/dialog';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';

@NgModule({
  declarations: [
    AppComponent,
    UserComponent,
    RoleComponent,
    PermissionComponent,
    RoleUpdateDialogComponent,
    RoleDeleteDialogComponent,
    RoleCreateDialogComponent,
    PermissionCreateDialogComponent,
    PermissionDeleteDialogComponent,
    PermissionUpdateDialogComponent,
    UserCreateDialogComponent,
    UserDeleteDialogComponent,
    UserUpdateDialogComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    AppRoutingModule,
    HttpClientModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    MatToolbarModule,
    MatButtonModule,
    MatTableModule,
    MatPaginatorModule,
    MatDialogModule,
    MatInputModule,
    MatSelectModule,
    ModalModule.forRoot(),
    PaginationModule.forRoot(),
    NgxsModule.forRoot([PermissionState, RoleState, UserState]),
    ToastrModule.forRoot({
      timeOut: 2000,
      positionClass: 'toast-bottom-right',
      preventDuplicates: false
    }),
  ],
  entryComponents:[
    RoleUpdateDialogComponent,
    RoleDeleteDialogComponent,
    RoleCreateDialogComponent,
    PermissionCreateDialogComponent,
    PermissionDeleteDialogComponent,
    PermissionUpdateDialogComponent,
    UserCreateDialogComponent,
    UserDeleteDialogComponent,
    UserUpdateDialogComponent
  ],
  providers: [
    UserService,
    PermissionService,
    RoleService
  ],
  bootstrap: [AppComponent],
})
export class AppModule { }
