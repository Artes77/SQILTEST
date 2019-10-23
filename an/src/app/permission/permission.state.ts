import { Permission } from '../shared/model/permission.model';
import { Action, Selector, State, StateContext } from '@ngxs/store';
import { PermissionService } from '../shared/service/permission.service'
import { AddPermission, DeletePermission, GetPermissionById, GetPermissions, UpdatePermission } from './permission.action';
import { HttpErrorResponse } from '@angular/common/http';
import { catchError, tap } from 'rxjs/operators'
import { throwError } from 'rxjs';
import { StateContextFactory } from '@ngxs/store/src/internal/state-context-factory';

export class PermissionStateModel {
    permissions: Permission[];
    permission: Permission;
}

@State<PermissionStateModel>({
    name: 'permissions',
    defaults:{
        permissions:[],
        permission: null
    }
})
export class PermissionState {
    constructor(private permissionService: PermissionService) {}

    @Selector()
    static getPermissionList(state: PermissionStateModel){
        return state.permissions;
    }

    @Selector()
    static getSelectedPermission(state: PermissionStateModel){
        console.log('getSelectedPermission!');
        return state.permission;
    }

    @Action(GetPermissions)
    GetPermissions({ getState, setState}: StateContext<PermissionStateModel>){
        return this.permissionService.getAll().pipe(tap(response=>{
            const state = getState();
            setState({
                ...state,
                permissions: response
            });
        }),
        catchError((err: HttpErrorResponse) => {
            alert('Something happened. Please try again.');
            return throwError(err.message);
        })
      );
    }

    @Action(GetPermissionById)
    getPermissionById(
        { getState, setState, patchState }: StateContext<PermissionStateModel>,
        { id }: GetPermissionById
    )  {
        return this.permissionService.get(id).pipe(tap(response => {
            const state=getState();
            patchState({
                ...state,
                permission: response
            });
        }),
        catchError((err: HttpErrorResponse)=>{
            alert('Something happened. Place try again.');
            return throwError(err.message);
        })
      );
    }

    @Action(AddPermission)
    addPermission(
      { getState, patchState }: StateContext<PermissionStateModel>,
      { payload }: AddPermission
    ) {
      return this.permissionService.create(payload).pipe(
        tap(response => {
          const state = getState();
          patchState({
            permissions: [...state.permissions, response]
          });
        }),
        catchError((err: HttpErrorResponse) => {
          alert('Something happened. Please try again.');
          return throwError(err.message);
        })
      );
    }

    @Action(UpdatePermission)
    updatePermission(
      { getState, setState }: StateContext<PermissionStateModel>,
      { payload }: UpdatePermission
    ) {
      // Optimistic update
      const previousState = getState();
      const state = getState();
      const permissions = [...state.permissions];
      const index = permissions.findIndex(item => item.id === payload.id);
      permissions[index] = payload;
      setState({
        ...state,
        permissions
      });
      return this.permissionService.update(payload).pipe(
        catchError((err: HttpErrorResponse) => {
          alert('Something happened. Please try again.');
          setState({
            ...state,
            permissions: previousState.permissions
          });
          return throwError(err.message);
        })
      );
    }

    @Action(DeletePermission)
    deletePermission(
      { getState, setState }: StateContext<PermissionStateModel>,
      { id }: DeletePermission
    ) {
      // Optimistic update
      const previousState = getState();
      const state = getState();
      const filteredArray = state.permissions.filter(h => h.id !== id);
      setState({
        ...state,
        permissions: filteredArray
      });
      return this.permissionService.delete(id).pipe(
        catchError((err: HttpErrorResponse) => {
          alert('Something happened. Please try again.');
          setState({
            ...state,
            permissions: previousState.permissions
          });
          return throwError(err.message);
        })
      );
    }
}
