import { Component, OnInit, ViewChild } from '@angular/core';
import { Permission } from '../shared/model/permission.model';
import { Observable } from 'rxjs';
import { Select, Store } from '@ngxs/store';
import { GetPermissions } from './permission.action';
import { PermissionState } from './permission.state';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { MatDialog } from '@angular/material/dialog';
import { PermissionUpdateDialogComponent } from './permission-update-dialog.component';
import { PermissionDeleteDialogComponent } from './permission-delete-dialog.component';
import { PermissionCreateDialogComponent } from './permission-create-dialog.component';

@Component({
  selector: 'app-permission',
  templateUrl: './permission.component.html',
  styleUrls: ['./permission.component.css']
})
export class PermissionComponent implements OnInit {

  displayedColumns: string[] = ['position', 'Permission', 'Button'];
  Permissions: Permission[];
  Permission: Permission;
  itemsPerPage: number[];
  data: MatTableDataSource<Permission>;
  

  @Select(PermissionState.getPermissionList)
  list$: Observable<Permission[]>;

  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;

  constructor(public dialog: MatDialog, private store: Store) {
    this.Permissions = []
    this.Permission
    this.itemsPerPage = [2, 5, 10, 20]
   }

  ngOnInit() {
    this.store.dispatch(new GetPermissions).subscribe(()=> this.loadAll());
  }

  loadAll(){
    this.list$.subscribe((res)=> {
      this.data = new MatTableDataSource(res);
      this.data.paginator = this.paginator;
    })
  }

  openDeleteDialog(permission: Permission){
    this.dialog.open(PermissionDeleteDialogComponent, {data: {permission}});
  }

  openEditDialog(permission: Permission){
    this.dialog.open(PermissionUpdateDialogComponent, {data: {
      permission
    }});
  }

  openCreateDialog(){
    this.dialog.open(PermissionCreateDialogComponent);
  }
}