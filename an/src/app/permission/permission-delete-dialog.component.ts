import { Component,Inject, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { Permission } from '../shared/model/permission.model';
import { ToastrService } from 'ngx-toastr';
import { Store } from '@ngxs/store';
import { DeletePermission } from './permission.action';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';


@Component({
  selector: 'permission-delete-dialog',
  templateUrl: './permission-delete-dialog.component.html'
})
export class PermissionDeleteDialogComponent implements OnInit {

  editForm = this.fb.group({
    id: [],
    name: []
  })

  permission: Permission;
  selectedUserUpdateLoaded: boolean = false;

  constructor(
    @Inject(MAT_DIALOG_DATA) public data:any,
    public dialog: MatDialogRef<PermissionDeleteDialogComponent>,
    private store: Store,
    private toastr: ToastrService,
    private fb: FormBuilder
    ) {}
  
  ngOnInit(){
    this.updateForm(this.data.permission);
  }  

  private updateForm(permission: Permission): void{
    this.editForm.patchValue({
      id: permission.id,
      name: permission.name
    })
  }

  private update(): Permission {
    return{
      ...new Permission(),
      id: this.editForm.get(['id']).value,
      name: this.editForm.get(['name']).value
    }
  }

  save(){
    this.permission = this.update();
    this.store.dispatch(new DeletePermission(this.permission.id)).subscribe(()=>{
      this.closeModal();
      this.toastr.success('Success', 'Permission is deleted');
    }, error =>{
      this.toastr.error('Error', 'server error');
    });
  }

  closeModal() {
    this.dialog.close();
  }
}