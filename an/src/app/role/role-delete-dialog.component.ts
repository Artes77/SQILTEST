import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { Role } from '../shared/model/role.model';
import { ToastrService } from 'ngx-toastr';
import {  Store } from '@ngxs/store';
import { DeleteRole } from '../role/role.action';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'role-delete-dialog',
  templateUrl: './role-delete-dialog.component.html'
})
export class RoleDeleteDialogComponent implements OnInit{

  editForm = this.fb.group({
    id: [],
    role: []
  })

  role: Role;
  selectedUserUpdateLoaded: boolean = false;

  constructor(
    @Inject(MAT_DIALOG_DATA) public data:any,
    public dialog: MatDialogRef<RoleDeleteDialogComponent>,
    private store: Store,
    private toastr:ToastrService,
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
    this.store.dispatch(new DeleteRole(this.role.id)).subscribe((res)=>{
      setTimeout(()=>{},300);
      this.closeModal();
    }, error =>{
      this.toastr.error('Error', 'server error');
    }).unsubscribe();
  }

  closeModal() {
    this.dialog.close();
  }
}