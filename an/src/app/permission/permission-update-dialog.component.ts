import { Component,Inject, OnInit } from '@angular/core';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { Observable } from 'rxjs';
import { Permission } from '../shared/model/permission.model';
import { ToastrService } from 'ngx-toastr';
import { Select, Store } from '@ngxs/store';
import { UpdatePermission } from './permission.action';
import { PermissionState } from './permission.state';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';


@Component({
  selector: 'permission-update-dialog',
  templateUrl: './permission-update-dialog.component.html'
})
export class PermissionUpdateDialogComponent implements OnInit {


  @Select(PermissionState.getPermissionList)
  list$: Observable<Permission[]>;


  editForm = this.fb.group({
    id: [''],
    name: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(20)], this.checkNameValidation.bind(this)]
  })

  permission: Permission;
  permissions: Permission[];
  a: Permission[]

  constructor(
    @Inject(MAT_DIALOG_DATA) public data:any,
    public dialog: MatDialogRef<PermissionUpdateDialogComponent>,
    private store: Store,
    private toastr: ToastrService,
    private fb: FormBuilder
    ) {
      this.permissions=[];
      this.list$.subscribe((res)=>{
        this.permissions = res;
      })
      this.a = this.permissions;

    }

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
    this.store.dispatch(new UpdatePermission(this.permission)).subscribe(()=>{
      this.closeModal();
      this.toastr.success('Success','Permission is updated');
    }, error =>{
      this.toastr.error('Error', 'server error');
    });
  }

  checkNameValidation(control: FormControl): Promise<any>{
    return new Promise(resolve => {
      for(let i=0; i < this.a.length; i++){
        if(control.value === this.a[i].name){
          resolve({
            "nameIsUsed":true
          });
          i=this.a.length;

        }else {
          resolve(null);
        }
      }
    })
  }

  closeModal() {
    this.dialog.close();
  }
}