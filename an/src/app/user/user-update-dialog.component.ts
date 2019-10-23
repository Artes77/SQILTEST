import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Role } from '../shared/model/role.model';
import { User } from '../shared/model/user.model';
import { Observable } from 'rxjs';
import { ToastrService } from 'ngx-toastr';
import { Select, Store } from '@ngxs/store';
import { UpdateUser } from './user.action';
import { UserState } from './user.state';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'user-update-dialog',
  templateUrl: './user-update-dialog.component.html'
})
export class UserUpdateDialogComponent implements OnInit{

  editForm = this.fb.group({
    id: [],
    firstName: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(20)]],
    lastName: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(20)]],
    roleId: [],
    roles: ['', [Validators.required]]
  })

  @Select(UserState.getUserList)
  list$: Observable<User[]>;

  user: User;
  Roles: Role[];
  selectedUserCreateLoaded: boolean = false;

  constructor(
    @Inject(MAT_DIALOG_DATA) public data:any,
    public dialog: MatDialogRef<UserUpdateDialogComponent>,
    private store: Store,
    private toastr: ToastrService,
    private fb: FormBuilder
    ) {
      this.editForm
      this.Roles=[];
    }
  
  ngOnInit(){
    this.updateForm(this.data.user);
    this.Roles=this.data.role;
  }

  private updateForm(user: User): void{
    this.editForm.patchValue({
      id: user.id,
      firstName: user.firstName,
      lastName: user.lastName,
      roleId: user.roleId,
      roles: user.role
    })
  }

  private update(): User {
    return{
      ...new User(),
      id: this.editForm.get(['id']).value,
      firstName: this.editForm.get(['firstName']).value,
      lastName: this.editForm.get(['lastName']).value,
      roleId: this.editForm.get(['roles']).value.id,
      role: new Role (this.editForm.get(['roles']).value.id, this.editForm.get(['roles']).value.role)
    }
  }

  save(){
    this.user = this.update();
    this.store.dispatch(new UpdateUser(this.user)).subscribe(()=>{
      this.closeModal();
      this.toastr.success('Success', 'User is updated');
    }, error =>{
      this.toastr.error('Error', 'server error');
    });
  }

  closeModal() {
    this.dialog.close();
  }
}