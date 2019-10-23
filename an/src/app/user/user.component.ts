import { Component, OnInit, ViewChild } from '@angular/core';
import { User } from '../shared/model/user.model';
import { Role } from '../shared/model/role.model';
import { Select, Store } from '@ngxs/store';
import { GetUsers } from './user.action';
import { UserState } from './user.state';
import { RoleState } from '../role/role.state';
import { Observable } from 'rxjs';
import { GetRoles } from '../role/role.action';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { MatDialog } from '@angular/material/dialog';
import { UserUpdateDialogComponent } from './user-update-dialog.component';
import { UserDeleteDialogComponent } from './user-delete-dialog.component';
import { UserCreateDialogComponent } from './user-create-dialog.component';


@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {

  @Select(RoleState.getRoleList)
  listRole$: Observable<Role[]>;

  @Select(UserState.getUserList)
  list$: Observable<User[]>;

  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;

  displayedColumns: string[] = ['position', 'FirstName', 'LastName', 'Role', 'Button'];
  User: User;
  Roles: Role[];
  currentPage: number;
  itemsPerPage: number[];
  totalItems: number;
  data: MatTableDataSource<User>;
  
  constructor(private store: Store, public dialog: MatDialog) {
    this.User
    this.currentPage = 1
    this.itemsPerPage = [2, 5, 10, 20]
    this.totalItems = 0

   }

  ngOnInit() {
    this.store.dispatch(new GetUsers).subscribe(() => {
      this.loadAll();
    });
    this.store.dispatch(new GetRoles).subscribe((res) => {this.Roles = res.roles.roles});
  }

  loadAll(){
    this.list$.subscribe((res)=> {
      this.data = new MatTableDataSource(res);
      this.data.paginator = this.paginator;
    })
  }

  openDeleteDialog(user: User){
    this.dialog.open(UserDeleteDialogComponent, {data: {user}});
  }

  openEditDialog(user:User, role:Role[]){
    this.dialog.open(UserUpdateDialogComponent, {data: {
      user,
      role
    }});
  }

  openCreateDialog(role: Role[]){
    this.dialog.open(UserCreateDialogComponent, {data: {role}});
  }
}
