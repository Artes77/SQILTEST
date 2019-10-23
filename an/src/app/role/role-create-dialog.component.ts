import { Component, Inject } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Role } from '../shared/model/role.model';
import { ToastrService } from 'ngx-toastr';
import { Store } from '@ngxs/store';
import { AddRole } from '../role/role.action';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'role-create-dialog',
  templateUrl: './role-create-dialog.component.html'
})
export class RoleCreateDialogComponent {

  editForm = this.fb.group({
    id: [],
    role: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(20)]]
  })

  role: Role;

  constructor(
    @Inject(MAT_DIALOG_DATA) public data:any,
    public dialog: MatDialogRef<RoleCreateDialogComponent>,
    private store: Store,
    private toastr:ToastrService,
    private fb: FormBuilder
    ) {
      this.editForm
    }

  private updateUser(): Role {
    return{
      ...new Role(),
      role: this.editForm.get(['role']).value
    }
  }

  save(){
    this.role = this.updateUser();
    this.store.dispatch(new AddRole(this.role)).subscribe(()=>{
      this.closeModal();
      this.toastr.success('Success', 'New role is created')
    }, error =>{
      this.toastr.error('Error', 'server error');
    });
  }

  closeModal() {
    this.dialog.close();
  }
}