import { Component, Inject } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Permission } from '../shared/model/permission.model';
import { ToastrService } from 'ngx-toastr';
import { Store } from '@ngxs/store';
import { AddPermission } from './permission.action';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'permission-create-dialog',
  templateUrl: './permission-create-dialog.component.html'
})
export class PermissionCreateDialogComponent {

  editForm = this.fb.group({
    id: [],
    name: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(20)]]
  })

  permission: Permission;
  selectedUserCreateLoaded: boolean = false;

  constructor(
    @Inject(MAT_DIALOG_DATA) public data:any,
    public dialog: MatDialogRef<PermissionCreateDialogComponent>,
    private store: Store,
    private toastr: ToastrService,
    private fb: FormBuilder
    ) {
      this.editForm
    }

  private create(): Permission {
    return{
      ...new Permission(),
      id: 0,
      name: this.editForm.get(['name']).value
    }
  }

  save(){
    this.permission = this.create();
    this.store.dispatch(new AddPermission(this.permission)).subscribe(()=>{
      this.closeModal();
      this.toastr.success('Success', 'New permission is created')
    }, error =>{
      this.toastr.error('Error', 'server error'+ error);
    });
  }

  closeModal() {
    this.dialog.close();
  }
}