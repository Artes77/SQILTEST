import { Component, OnInit, Inject } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { User } from '../shared/model/user.model';
import { ToastrService } from 'ngx-toastr';
import { Store } from '@ngxs/store';
import { DeleteUser } from './user.action';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'user-delete-dialog',
  templateUrl: './user-delete-dialog.component.html'
})
export class UserDeleteDialogComponent implements OnInit{

  editForm = this.fb.group({
    id: [],
    firstName: [],
    lastName: [],
    role: []
  })

  user: User;

  constructor(
    @Inject(MAT_DIALOG_DATA) public data:any,
    private store: Store,
    private toastr: ToastrService,
    private fb: FormBuilder,
    public dialog: MatDialogRef<UserDeleteDialogComponent>
    ) {}
  
  ngOnInit(){
    this.updateForm(this.data.user);
  }

  private updateForm(user: User): void{
    this.editForm.patchValue({
      id: user.id,
      firstName: user.firstName,
      lastName: user.lastName,
      role: user.role.role
    })
  }

  private update(): User {
    return{
      ...new User(),
      id: this.editForm.get(['id']).value,
      firstName: this.editForm.get(['firstName']).value,
      lastName: this.editForm.get(['lastName']).value,
      role: this.editForm.get(['role']).value
    }
  }

  save(){
    this.user = this.update();
    this.store.dispatch(new DeleteUser(this.user.id)).subscribe(()=>{
      this.closeModal();
      this.toastr.success('Success', 'User is deleted');
    }, error =>{
      this.toastr.error('Error', 'server error');
    });
  }

  closeModal() {
    this.dialog.close();
  }
}