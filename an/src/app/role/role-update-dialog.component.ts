import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Role } from '../shared/model/role.model';
import { ToastrService } from 'ngx-toastr';
import { Store } from '@ngxs/store';
import { UpdateRole } from '../role/role.action';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';


@Component({
  selector: 'role-update-dialog',
  templateUrl: './role-update-dialog.component.html'
})
export class RoleUpdateDialogComponent implements OnInit{

  editForm = this.fb.group({
    id: [''],
    role: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(20)]]
  })

  role: Role;

  constructor(
    @Inject(MAT_DIALOG_DATA) public data:any,
    public dialog: MatDialogRef<RoleUpdateDialogComponent>,
    private store: Store,
    private toastrService: ToastrService,
    private fb: FormBuilder
    ) {}

  ngOnInit(){
    this.updateForm(this.data.role);
  }

  private updateForm(role: Role): void{
    this.editForm.patchValue({
      id: role.id,
      role: role.role
    })
  }

  private updateUser(): Role {
    return{
      ...new Role(),
      id: this.editForm.get(['id']).value,
      role: this.editForm.get(['role']).value
    }
  }

  save(){
    this.role = this.updateUser();
    this.store.dispatch(new UpdateRole(this.role)).subscribe(()=>{
      this.closeModal();
      this.toastrService.success('Success', 'Role is updated');
    }, error =>{
      this.toastrService.error('Error', 'server error');
    });
  }

  closeModal() {
    this.dialog.close();
  }
}