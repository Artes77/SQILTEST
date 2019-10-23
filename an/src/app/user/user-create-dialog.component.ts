import { Component, OnInit, Inject } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Role } from '../shared/model/role.model';
import { IUser, User } from '../shared/model/user.model';
import { ToastrService } from 'ngx-toastr';
import { Store } from '@ngxs/store';
import { AddUser } from './user.action';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'user-create-dialog',
  templateUrl: './user-create-dialog.component.html'
})
export class UserCreateDialogComponent implements OnInit {

  editForm = this.fb.group({
    id: [],
    firstName: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(20)]],
    lastName: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(20)]],
    roleId: [''],
    roles: [ Role , [Validators.required]]
  })

  user: IUser;
  Roles: Role[];
  selectedUserCreateLoaded: boolean = false;

  constructor(
    @Inject(MAT_DIALOG_DATA) public data:any,
    private store: Store,
    public dialog: MatDialogRef<UserCreateDialogComponent>,
    private toastr:ToastrService,
    private fb: FormBuilder
    ) {
      this.editForm
    }
  
  ngOnInit(){
    this.Roles=this.data.role;
  }

  private update(): IUser {
    return{
      ...new User(),
      id: 0,
      firstName: this.editForm.get(['firstName']).value,
      lastName: this.editForm.get(['lastName']).value,
      roleId: this.editForm.get(['roles']).value.id,
      role: null
    }
  }

  save(){
    this.user = this.update();
    this.store.dispatch(new AddUser(this.user)).subscribe(()=>{
      this.closeModal();
      this.toastr.success('Success', 'User is created');
    }, error =>{
      this.toastr.error('Error', 'server error');
    });
  }

  closeModal() {
    this.dialog.close();
  }
}