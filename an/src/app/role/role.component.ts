import { Component, OnInit, ViewChild } from '@angular/core';
import { Role } from '../shared/model/role.model';
import { Observable } from 'rxjs';
import { Select, Store } from '@ngxs/store';
import { GetRoles } from '../role/role.action';
import { RoleState } from '../role/role.state';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { MatDialog } from '@angular/material/dialog';
import { RoleUpdateDialogComponent } from './role-update-dialog.component';
import { RoleDeleteDialogComponent } from './role-delete-dialog.component';
import { RoleCreateDialogComponent } from './role-create-dialog.component';

@Component({
  selector: 'app-role',
  templateUrl: './role.component.html',
  styleUrls: ['./role.component.css']
})
export class RoleComponent implements OnInit {
  
  @Select(RoleState.getRoleList)
  listRole$: Observable<Role[]>;
  

  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;

  displayedColumns: string[] = ['position', 'Role', 'Button'];
  Roles: Role[];
  role: Role;
  itemsPerPage: number[];
  data: MatTableDataSource<Role>;

  constructor(public dialog: MatDialog, private store: Store) {
    this.Roles = []
    this.role
    this.itemsPerPage = [2, 5, 10, 20]
   }

  ngOnInit() {
    this.store.dispatch(new GetRoles).subscribe(res => this.loadAll());
  }

  loadAll(){
    this.listRole$.subscribe((res)=> {
      this.data = new MatTableDataSource(res);
      this.data.paginator = this.paginator;
    })
  }

  openDeleteDialog(role: Role){
    this.dialog.open(RoleDeleteDialogComponent, {data: {role}});
  }

  openEditDialog(role:Role){
    this.dialog.open(RoleUpdateDialogComponent, {data: {
      role
    }});
    
  }

  openCreateDialog(){
    this.dialog.open(RoleCreateDialogComponent);
  }
}